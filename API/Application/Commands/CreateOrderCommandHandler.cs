using Domain.Aggregates.OrderAggregate;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderLineRepository _orderLineRepository;
        public CreateOrderCommandHandler(IOrderRepository orderRepository, IOrderLineRepository orderLineRepository)
        {
            _orderRepository = orderRepository;
            _orderLineRepository = orderLineRepository;
        }

        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _orderRepository.Add(new Order(request.CustomerId, OrderState.DRAFT, DateTime.Now));
            var orderLine = _orderLineRepository.Add(new OrderLine(order.Id, request.FlightId, request.Price));

            order.OrderLines.Add(orderLine);

            await _orderLineRepository.UnitOfWork.SaveChangesAsync();

            return await Task.FromResult(order);
        }
    }
}
