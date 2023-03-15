using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Commands
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IFlightRateRepository _flightRateRepository;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IFlightRateRepository flightRateRepository, IOrderLineRepository orderLineRepository)
        {
            _orderRepository = orderRepository;
            _flightRateRepository = flightRateRepository;
        }

        public async Task<Order> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(request.OrderId);
            if (order.State == OrderState.CONFIRMED) 
            {
                return order;
            }

            var updatedOrder = new Order(order.CustomerId, OrderState.CONFIRMED, order.OrderDate);
            _orderRepository.Update(updatedOrder);


            var flightRates = await _flightRateRepository.GetFlightRatesAsync(order.OrderLines.Select(o => o.FlightRateId).ToList());

            await _orderRepository.UnitOfWork.SaveChangesAsync();
            return updatedOrder;
        }
    }
}
