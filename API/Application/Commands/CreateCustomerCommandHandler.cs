using Domain.Aggregates.CustomerAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Commands
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _customerRepository.Add(new Customer(request.FirstName, request.LastName, request.DateOfBirth));

            await _customerRepository.UnitOfWork.SaveChangesAsync();
            return customer;
        }
    }
}
