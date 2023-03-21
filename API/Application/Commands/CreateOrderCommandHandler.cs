using API.ApiResponses;
using AutoMapper;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderLine>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderLineRepository _orderLineRepository;
        private readonly IFlightRateRepository _flightRateRepository;
        public CreateOrderCommandHandler
            (
                IOrderRepository orderRepository,
                IOrderLineRepository orderLineRepository,
                IFlightRateRepository flightRateRepository
            )
        {
            _orderRepository = orderRepository;
            _orderLineRepository = orderLineRepository;
            _flightRateRepository = flightRateRepository;
        }

        public async Task<OrderLine> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            // Get the flight rate associated with the command.flightRateId
            var flightRate = await _flightRateRepository.Get(command.FlightRateId).SingleOrDefaultAsync();

            if (flightRate == null)
            {
                throw new ArgumentException($"Flight rate with ID {command.FlightRateId} not found");
            }

            // If there are not enough slots available, throw an exception
            if (command.Slots > flightRate.Available)
            {
                throw new ArgumentException($"Not enough slots available for flight rate with ID {command.FlightRateId}");
            }

            // Create a new order & order line
            var order = _orderRepository.Add(new Order(command.CustomerId, OrderState.DRAFT, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)));
            var orderLine = _orderLineRepository.Add(new OrderLine(order.Id, command.FlightRateId, flightRate.Price.Value, command.Slots));

            orderLine.Order = order;

            await _orderLineRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return orderLine;
        }
    }
}
