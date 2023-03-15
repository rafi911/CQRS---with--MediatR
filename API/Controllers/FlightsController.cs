using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.ApiResponses;
using API.Application.Queries;
using API.Application.ViewModels;
using AutoMapper;
using Domain.Aggregates.AirportAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightsController : ControllerBase
{
    private readonly ILogger<FlightsController> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public FlightsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("Search")]
    public async Task<List<FlightResponse>> GetAvailableFlights(string destination)
    {
        GetFlightsQuery searchFlightCommand = new(destination);
        var flights = await _mediator.Send(searchFlightCommand);

        return _mapper.Map<List<FlightResponse>>(flights);
    }
}
