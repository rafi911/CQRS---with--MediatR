using Domain.Aggregates.CustomerAggregate;
using Domain.Aggregates.OrderAggregate;
using Domain.Events;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Commands
{
    public class UpdatedFlightRateEventHandler : INotificationHandler<FlightRatePriceChangedEvent>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        public UpdatedFlightRateEventHandler(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public async Task Handle(FlightRatePriceChangedEvent notification, CancellationToken cancellationToken)
        {
            var flightRate = notification.FlightRate;
            var orders = await _orderRepository.GetOrdersAsync(flightRate.Id);

            var customers = await _customerRepository.GetCustomersAsync(orders.Select(o => o.CustomerId).ToList());

            foreach (var customer in customers)
            {
                // just draft messages
                System.Console.WriteLine($"Hi {customer.FirstName} {customer.FirstName}, Price of the order has been changed");
            }


        }
    }
}
