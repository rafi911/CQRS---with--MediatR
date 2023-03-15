using API.Application.Commands;
using AutoMapper;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightRateController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public FlightRateController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("/ChangeFlightRate")]
        public async Task<IActionResult> ChangeFlightRateAsync([FromBody] UpdateFlightRateCommand command)
        {
            var flightRateViewModel = await _mediator.Send(command);
            var flightRatePriceChangedEvent = new FlightRatePriceChangedEvent(flightRateViewModel.Flight, flightRateViewModel.FlightRate);
            await _mediator.Publish(flightRatePriceChangedEvent);

            return Ok(flightRateViewModel.FlightRate);
        }

    }
}
