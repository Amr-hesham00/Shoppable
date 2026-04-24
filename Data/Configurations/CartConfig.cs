using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shoppable.Data.Configurations;

public class CartConfig : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Carts");

        builder.HasKey(x => x.Id);
        //builder.Property(x => x.Id).ValueGeneratedNever();


        builder.HasOne(c => c.customer)
               .WithOne(cu => cu.cart)
               .HasForeignKey<Cart>(c => c.CustomerId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.CreatedDate)
            .IsRequired();
    }
}
