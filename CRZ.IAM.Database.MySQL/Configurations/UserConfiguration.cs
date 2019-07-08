using CRZ.IAM.Platform.Domain.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRZ.IAM.Database.MySQL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users").HasIndex(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.Name).HasColumnName("name").IsRequired();
            builder.Property(u => u.Email).HasColumnName("email").IsRequired();
            builder.Property(u => u.Username).HasColumnName("username").IsRequired();
            builder.Property(u => u.Password).HasColumnName("password").IsRequired().HasMaxLength(255);
            builder.Property(u => u.LastLogin).HasColumnName("last_login").HasColumnType("datetime");
            builder.Property(u => u.Active).HasColumnName("active").IsRequired().HasDefaultValue(1);
            builder.Property(u => u.Token).HasColumnName("token").HasMaxLength(500);
            builder.Property(u => u.CreatedAt).HasColumnName("created_at")
                .IsRequired().HasColumnType("datetime").HasDefaultValueSql("now()");
            builder.Property(u => u.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime");

            // indexes
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.Username).IsUnique();
        }
    }
}