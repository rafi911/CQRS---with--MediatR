using API.Application.ViewModels;
using Domain.Aggregates.FlightAggregate;
using Domain.Common;
using MediatR;
using System;

namespace API.Application.Commands
{
    public record UpdateFlightRateCommand(Guid FlightId, decimal Price, Currency Currency) : IRequest<FlightRateViewModel> { }
}
