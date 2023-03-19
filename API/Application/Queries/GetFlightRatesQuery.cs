using Domain.Aggregates.FlightAggregate;
using MediatR;
using System.Collections.Generic;

namespace API.Application.Queries
{
    public record GetFlightRatesQuery: IRequest<List<FlightRate>> { }
}
