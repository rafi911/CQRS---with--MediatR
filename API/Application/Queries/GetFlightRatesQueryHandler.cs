using Domain.Aggregates.FlightAggregate;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Queries
{
    public class GetFlightRatesQueryHandler : IRequestHandler<GetFlightRatesQuery, List<FlightRate>>
    {
        private readonly IFlightRateRepository _flightRateRepository;

        public GetFlightRatesQueryHandler(IFlightRateRepository flightRateRepository)
        {
            _flightRateRepository = flightRateRepository;
        }

        public async Task<List<FlightRate>> Handle(GetFlightRatesQuery request, CancellationToken cancellationToken)
        {
            return await _flightRateRepository.GetFlightRatesAsync();
        }
    }
}
