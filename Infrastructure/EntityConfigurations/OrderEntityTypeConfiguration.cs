using Domain.Aggregates.AirportAggregate;
using Domain.Aggregates.CustomerAggregate;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : BaseEntityTypeConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Order.OrderLines));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(p => p.State).IsRequired();

            builder.Property(p => p.OrderDate).IsRequired();

            builder.HasOne<Customer>()
                .WithMany()
                .IsRequired()
                .HasForeignKey("CustomerId");

        }
    }
}
