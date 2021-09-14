using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RobotTR.Jobs.API.Models;
using System;

namespace RobotTR.Jobs.API.Data.Mapping
{
    public class JobsMapping : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .IsRequired()
                .HasColumnType("varchar(MAX)");

            builder.Property(c => c.Level)
                .HasMaxLength(20)
                .HasConversion
                (
                    v => v.ToString(),
                    v => (LevelEnum)Enum.Parse(typeof(LevelEnum), v)
                )
                .IsUnicode(false);

            builder.Property(c => c.Languages)
                .HasMaxLength(20)
                .HasConversion
                (
                    v => v.ToString(),
                    v => (LanguagesEnum)Enum.Parse(typeof(LanguagesEnum), v)
                )
                .IsUnicode(false);

            builder.Property(c => c.Frameworks)
                .HasMaxLength(20)
                .HasConversion
                (
                    v => v.ToString(),
                    v => (FrameworksEnum)Enum.Parse(typeof(FrameworksEnum), v)
                )
                .IsUnicode(false);

            builder.Property(c => c.Owner.Id)
                .IsRequired();

            builder.ToTable("Jobs");
        }
    }
}