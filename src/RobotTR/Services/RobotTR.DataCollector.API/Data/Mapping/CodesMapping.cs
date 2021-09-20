using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RobotTR.DataCollector.API.Models;

namespace RobotTR.DataCollector.API.Data.Mapping
{
    public class CodesMapping : IEntityTypeConfiguration<Codes>
    {
        public void Configure(EntityTypeBuilder<Codes> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Content)
                .IsRequired()
                .HasColumnType("varchar(MAX)");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(MAX)");

            builder.Property(c => c.Project)
                .IsRequired()
                .HasColumnType("varchar(MAX)");

            builder.ToTable("Jobs");
        }
    }
}