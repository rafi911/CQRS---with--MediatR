using API.ApiResponses;
using API.Application.ViewModels;
using API.Mapping.Converters;
using AutoMapper;
using Domain.Aggregates.AirportAggregate;
using Domain.Aggregates.CustomerAggregate;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;

namespace API.Mapping
{
    public class AirportProfile : Profile
    {
        public AirportProfile()
        {
            CreateMap<Airport, AirportViewModel>();
            CreateMap<FlightDetail, FlightResponse>();
            CreateMap<Customer, CustomerReponse>();
            CreateMap<FlightRate, FlightRateResponse>()
                .ConvertUsing(new FlightRateResponseConverter());
            CreateMap<OrderLine, OrderResponse>()
                .ConvertUsing(new CreatedOrderResponseConverter());
        }
    }
}