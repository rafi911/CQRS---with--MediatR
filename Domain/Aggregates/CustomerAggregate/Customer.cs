using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.CustomerAggregate
{
    public class Customer:  Entity, IAggregateRoot
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTimeOffset DateOfBirth { get; private set; }

        public Customer() { }
        public Customer(string firstName, string lastName, DateTimeOffset dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }




    }
}
