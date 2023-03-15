using Domain.Aggregates.OrderAggregate;
using MediatR;
using System;

namespace API.Application.Commands
{
    public record CreateOrderCommand(Guid CustomerId, Guid FlightId, decimal Price) : IRequest<Order>
    {
    }
}
