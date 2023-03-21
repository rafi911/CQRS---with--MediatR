using API.ApiResponses;
using API.Application.Commands;
using API.Application.Queries;
using AutoMapper;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightRatesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public FlightRatesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPut("/ChangeFlightRate")]
        public async Task<FlightRateResponse> ChangeFlightRateAsync([FromBody] UpdateFlightRateCommand command)
        {
            var flightRateViewModel = await _mediator.Send(command);
            var flightRatePriceChangedEvent = new FlightRatePriceChangedEvent(flightRateViewModel.Flight, flightRateViewModel.FlightRate);
            await _mediator.Publish(flightRatePriceChangedEvent);

            return flightRateViewModel.RateResponse;
        }

        [HttpGet]
        public async Task<List<FlightRateResponse>> Get()
        {
            GetFlightRatesQuery query = new();
            var flightRates = await _mediator.Send(query);

            return _mapper.Map<List<FlightRateResponse>>(flightRates);
        }
    }
}
