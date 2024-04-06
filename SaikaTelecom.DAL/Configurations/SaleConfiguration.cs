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
    }
}
