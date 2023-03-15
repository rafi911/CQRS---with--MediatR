using Domain.Aggregates.CustomerAggregate;
using MediatR;
using System;

namespace API.Application.Queries
{
    public record GetCustomerQuery(Guid CustomerId): IRequest<Customer> { }
}
