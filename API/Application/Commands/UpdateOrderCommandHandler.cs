using API.ApiResponses;
using AutoMapper;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Commands
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderLine>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IFlightRateRepository _flightRateRepository;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IFlightRateRepository flightRateRepository)
        {
            _orderRepository = orderRepository;
            _flightRateRepository = flightRateRepository;
        }

        public async Task<OrderLine> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            // Get the order associated with the command.OrderId
            var order = await _orderRepository.GetAsync(command.OrderId);

            // If the order state is already confirmed, return the default value
            if (order.State == OrderState.CONFIRMED) 
            {
                return default;
            }

            // Change the order state to confirmed
            order.ChangeState(OrderState.CONFIRMED);

            _orderRepository.Update(order);

            //for the movement, consider relationshop between oder & orderline is 1:1 
            var orderLine = order.OrderLines.FirstOrDefault();

            var flightRate = await _flightRateRepository.Get(orderLine.FlightRateId).FirstOrDefaultAsync();

            // Reduce the number of available slots for the flight rate
            flightRate.ReduceAvailability(orderLine.Slots);

            _flightRateRepository.Update(flightRate);

            await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return orderLine;
        }
    }
}
