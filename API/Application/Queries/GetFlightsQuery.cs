using Domain.Aggregates.FlightAggregate;
using MediatR;
using System.Collections.Generic;

namespace API.Application.Queries
{
    public record GetFlightsQuery(string Destination) : IRequest<List<FlightDetail>> 
    {
    }
}
