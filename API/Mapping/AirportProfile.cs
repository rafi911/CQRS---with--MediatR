using API.ApiResponses;
using API.Application.ViewModels;
using AutoMapper;
using Domain.Aggregates.AirportAggregate;
using Domain.Aggregates.CustomerAggregate;
using Domain.Aggregates.FlightAggregate;

namespace API.Mapping
{
    public class AirportProfile : Profile
    {
        public AirportProfile()
        {
            CreateMap<Airport, AirportViewModel>();
            CreateMap<FlightDetail, FlightResponse>();
            CreateMap<Customer, CustomerReponse>();
        }
    }
}