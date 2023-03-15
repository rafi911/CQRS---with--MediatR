using Domain.Aggregates.CustomerAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Queries
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetAsync(request.CustomerId);
        }
    }
}
