using Domain.Aggregates.AirportAggregate;
using Domain.Aggregates.FlightAggregate;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Queries
{
    public class GetFlightsQueryHandler : IRequestHandler<GetFlightsQuery, List<FlightDetail>>
    {
        private readonly IFlightRepository _flightRepository;

        public GetFlightsQueryHandler(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        async Task<List<FlightDetail>> IRequestHandler<GetFlightsQuery, List<FlightDetail>>.Handle(GetFlightsQuery request, CancellationToken cancellationToken)
        {
            return await _flightRepository.GetFlights(request.Destination);
        }
    }
}
