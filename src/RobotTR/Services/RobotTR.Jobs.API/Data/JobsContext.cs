using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RobotTR.Core.Data;
using RobotTR.Core.DomainObjects;
using RobotTR.Core.Mediator;
using RobotTR.Core.Messages;
using RobotTR.Jobs.API.Models;
using RobotTR.Jobs.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotTR.Jobs.API.Data
{
    public class JobsContext : DbContext, IUnitOfWork
    {
        public DbSet<Job> Jobs { get; set; }
        private readonly IMediatorHandler _mediatorHandler;

        public JobsContext(DbContextOptions<JobsContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            var languagesConverter = new EnumCollectionConverter<LanguagesEnum>();
            var frameworksConverter = new EnumCollectionConverter<FrameworksEnum>();

            modelBuilder
              .Entity<Job>()
              .Property(e => e.Languages)
              .HasConversion(languagesConverter);

            modelBuilder
              .Entity<Job>()
              .Property(e => e.Frameworks)
              .HasConversion(frameworksConverter);

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobsContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            var success = await base.SaveChangesAsync() > 0;

            if (success) await _mediatorHandler.PublishEvent(this);

            return success;
        }
    }

    public static class MediatorExtension
    {
        public static async Task PublishEvent<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notifications)
                .ToList();

            domainEntities.ToList().ForEach(entity => entity.Entity.ClearEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}