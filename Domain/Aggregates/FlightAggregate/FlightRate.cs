using Domain.Common;
using Domain.SeedWork;
using System;

namespace Domain.Aggregates.FlightAggregate
{
    public class FlightRate : Entity, IAggregateRoot
    {
        public Guid FlightId { get; set; }
        public string Name { get; private set; }
        public Price Price { get; private set; }
        public int Available { get; private set; }

        protected FlightRate()
        {

        }

        public FlightRate(string name, Price price, int available)
        {
            Name = name;
            Price = price;
            Available = available;
        }

        public void ChangePrice(Price price)
        {
            Price = price;
        }

        public void MutateAvailability(int quantity)
        {
            Available += quantity;
        }
    }
}