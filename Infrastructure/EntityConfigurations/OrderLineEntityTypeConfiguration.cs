using Domain.Aggregates.CustomerAggregate;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityConfigurations
{
    public class OrderLineEntityTypeConfiguration: BaseEntityTypeConfiguration<OrderLine>
    {
        public override void Configure(EntityTypeBuilder<OrderLine> builder)
        {

            builder.Property(p => p.Price).IsRequired();

            //builder.HasOne<Order>()
            //   .WithMany()
            //   .HasForeignKey(o => o.OrderId)
            //   .IsRequired();

            //builder.HasOne<FlightRate>()
            //   .WithMany()
            //   .IsRequired()
            //   .HasForeignKey(o => o.FlightRateId);



        }
    }
}
