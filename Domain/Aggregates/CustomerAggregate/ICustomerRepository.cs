using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.CustomerAggregate
{
    public interface ICustomerRepository: IRepository<Customer>
    {
        Customer Add(Customer customer);

        Task<Customer> GetAsync(Guid customerId);

        Task<List<Customer>> GetCustomersAsync();

        Task<List<Customer>> GetCustomersAsync(List<Guid> customerIds);
    }
}
