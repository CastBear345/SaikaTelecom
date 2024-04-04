using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaikaTelecom.Domain.Entities;
using SaikaTelecom.Domain.Enum;

namespace SaikaTelecom.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(u => u.PasswordHash)
            .IsRequired();

        builder
            .Property(u => u.Role)
            .IsRequired();

        builder.HasData(new List<User>()
        {
            new User()
            {
                Id = 1,
                FullName = "ChangeTracker",
                Email = "castbear@email.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("a123456a"),
                Role = Roles.Owner
            }
        });
    }
}
