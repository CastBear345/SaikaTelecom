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
    }
}
