namespace SaikaTelecom.DAL.Configurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(u => u.LastName)
            .HasMaxLength(50);

        builder
            .Property(u => u.SurName)
            .HasMaxLength(50);

        builder
            .Property(u => u.PhoneNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(u => u.Email)
            .HasMaxLength(100);

        builder
            .Property(u => u.Status)
            .IsRequired();

        builder
            .Property(u => u.LastChanged)
            .IsRequired();

        builder
            .HasOne(c => c.Marketer)
            .WithMany()
            .HasForeignKey(c => c.MarketerId);
    }
}
