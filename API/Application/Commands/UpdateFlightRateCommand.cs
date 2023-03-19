using API.Application.ViewModels;
using Domain.Aggregates.FlightAggregate;
using Domain.Common;
using MediatR;
using System;

namespace API.Application.Commands
{
    public record UpdateFlightRateCommand(Guid FlightRateId, decimal Price) : IRequest<FlightRateViewModel> { }
}
