using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shoppable.Data.Configurations;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(x => x.Id);
        //builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasMany(x => x.orderItems)
            .WithOne(x => x.order)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.payment)
            .WithOne(x => x.order)
            .HasForeignKey<Payment>(x => x.OrderId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}
