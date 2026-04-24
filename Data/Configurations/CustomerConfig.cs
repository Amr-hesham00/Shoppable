using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shoppable.Data.Configurations;

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(x => x.Id);
        //builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasOne(x => x.AppUser)
            .WithOne(x => x.customer)
            .HasForeignKey<Customer>(x => x.UserID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.orders)
            .WithOne(x => x.customer)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
