namespace SaikaTelecom.DAL.Configurations;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(s => s.LeadId)
            .IsRequired();

        builder
            .Property(s => s.SellerId)
            .IsRequired();

        builder
            .Property(s => s.DateOfSale)
            .IsRequired();

        builder
            .HasOne(s => s.Lead)
            .WithMany()
            .HasForeignKey(s => s.LeadId);

        builder
            .HasOne(s => s.Seller)
            .WithMany()
            .HasForeignKey(s => s.SellerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
