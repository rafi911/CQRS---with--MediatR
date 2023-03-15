using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.OrderAggregate
{
    public  class Order : Entity, IAggregateRoot
    {
        public OrderState State { get; private set; }
        public Guid CustomerId { get; private set; }
        public DateTimeOffset OrderDate { get; private set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
        public Order(Guid customerId, OrderState state, DateTimeOffset orderDate)
        {
            CustomerId = customerId;
            State = state;
            OrderDate = orderDate;
        }

    }
}
