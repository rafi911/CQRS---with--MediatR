using API.Application.ViewModels;
using Domain.Aggregates.FlightAggregate;
using Domain.Common;
using Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Commands
{
    public class UpdateFlightRateCommandHandler : IRequestHandler<UpdateFlightRateCommand, FlightRateViewModel>
    {
        private readonly IFlightRateRepository _flightRateRepository;
        private readonly IFlightRepository _flightRepository;

        public UpdateFlightRateCommandHandler(IFlightRateRepository flightRateRepository, IFlightRepository flightRepository)
        {
            _flightRateRepository = flightRateRepository;
            _flightRepository = flightRepository;
        }

        public async Task<FlightRateViewModel> Handle(UpdateFlightRateCommand request, CancellationToken cancellationToken)
        {
            var flightRate = await _flightRateRepository.GetAsync(request.FlightId);
            var flight = await _flightRepository.GetAsync(request.FlightId);

            var price = new Price(request.Price, request.Currency);
            flightRate.ChangePrice(price);

            flightRate.AddDomainEvent(new FlightRatePriceChangedEvent(flight, flightRate));

            _flightRateRepository.Update(flightRate);

            await _flightRateRepository.UnitOfWork.SaveChangesAsync();
            return new FlightRateViewModel(flight, flightRate);
        }
    }
}
