using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RobotTR.Core.DomainObjects;

namespace RobotTR.User.API.Data.Mappings
{
    public class UsersMapping : IEntityTypeConfiguration<RobotTR.Core.Models.User>
    {
        public void Configure(EntityTypeBuilder<RobotTR.Core.Models.User> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Username)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.Address)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.EmailMaxLength})");
            });

            builder.Property(c => c.Empresa)
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Nome)
                .HasColumnType("varchar(200)");

            builder.ToTable("Users");
        }
    }
}