using API.ApiResponses;
using Domain.Aggregates.FlightAggregate;

namespace API.Application.ViewModels
{
    public record FlightRateViewModel(Flight Flight, FlightRate FlightRate, FlightRateResponse RateResponse) { }
}
