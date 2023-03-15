using Domain.Aggregates.OrderAggregate;
using MediatR;
using System;

namespace API.Application.Commands
{
    public record UpdateOrderCommand(Guid OrderId): IRequest<Order>{}
}
