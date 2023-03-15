using Domain.Aggregates.FlightAggregate;
using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.OrderAggregate
{
    public class OrderLine : Entity, IAggregateRoot
    {
        public Guid OrderId { get; private set; }
        public Guid FlightRateId { get; private set; }
        public decimal Price { get; private set; }
        public OrderLine(Guid orderId, Guid flightRateId, decimal price)
        {
            OrderId = orderId;
            FlightRateId = flightRateId;
            Price = price;
        }

        public virtual Order Order { get; set; }
        public virtual FlightRate FlightRate { get; set; }
    }
}
