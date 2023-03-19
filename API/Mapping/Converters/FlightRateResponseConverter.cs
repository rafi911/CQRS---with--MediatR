using API.ApiResponses;
using AutoMapper;
using Domain.Aggregates.FlightAggregate;

namespace API.Mapping.Converters
{
    public class FlightRateResponseConverter : ITypeConverter<FlightRate, FlightRateResponse>
    {
        public FlightRateResponse Convert(FlightRate source, FlightRateResponse destination, ResolutionContext context)
        {
            if (source is null)
            {
                return default;
            }

            return new FlightRateResponse
            {
                Id = source.Id,
                Name = source.Name,
                Available = source.Available,
                Price = source.Price.Value,
                Currency = source.Price.Currency.ToString(),
            };
        }
    }
}
