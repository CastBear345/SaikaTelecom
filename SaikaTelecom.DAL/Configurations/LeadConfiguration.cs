namespace SaikaTelecom.DAL.Configurations;

public class LeadConfiguration : IEntityTypeConfiguration<Lead>
{
    public void Configure(EntityTypeBuilder<Lead> builder)
    {
        builder
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(u => u.SellerId)
            .IsRequired();

        builder
            .Property(u => u.ContactId)
            .IsRequired();

        builder
            .HasOne(l => l.Contact)
            .WithOne()
            .HasForeignKey<Lead>(l => l.ContactId)
            .IsRequired();

        builder
            .HasOne(l => l.Seller)
            .WithMany()
            .HasForeignKey(l => l.SellerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
