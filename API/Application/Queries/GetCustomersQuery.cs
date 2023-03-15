using Domain.Aggregates.CustomerAggregate;
using MediatR;
using System.Collections.Generic;

namespace API.Application.Queries
{
    public record GetCustomersQuery: IRequest<List<Customer>>
    {
    }
}
