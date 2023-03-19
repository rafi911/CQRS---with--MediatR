using API.ApiResponses;
using API.Application.ViewModels;
using AutoMapper;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using Domain.Common;
using Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Commands
{
    public class UpdateFlightRateCommandHandler : IRequestHandler<UpdateFlightRateCommand, FlightRateViewModel>
    {
        private readonly IFlightRateRepository _flightRateRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderLineRepository _orderLineRepository;
        private readonly IMapper _mapper;

        public UpdateFlightRateCommandHandler(IFlightRateRepository flightRateRepository, IFlightRepository flightRepository, IMapper mapper, IOrderRepository orderRepository, IOrderLineRepository orderLineRepository)
        {
            _flightRateRepository = flightRateRepository;
            _flightRepository = flightRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _orderLineRepository = orderLineRepository;
        }

        public async Task<FlightRateViewModel> Handle(UpdateFlightRateCommand request, CancellationToken cancellationToken)
        {
            var flightRate = await _flightRateRepository.Get(request.FlightRateId).Include(fr => fr.Flight).FirstOrDefaultAsync();

            if (flightRate is null)
            {
                return default;
            }

            var price = new Price(request.Price, flightRate.Price.Currency);
            flightRate.ChangePrice(price);

            flightRate.AddDomainEvent(new FlightRatePriceChangedEvent(flightRate.Flight, flightRate));

            _flightRateRepository.Update(flightRate);

            // Update the price of drafts orders if flight rate gets changed
            var orderLines = (await _orderRepository.GetDraftOrdersAsync(flightRate.Id)).SelectMany(o => o.OrderLines);

            foreach (var orderLine in orderLines)
            {
                orderLine.ChangePrice(flightRate.Price.Value);
            }

            _orderLineRepository.BulkUpdate(orderLines);

            await _flightRateRepository.UnitOfWork.SaveChangesAsync();

            var rateResponse = _mapper.Map<FlightRateResponse>(flightRate);
            return new FlightRateViewModel(flightRate.Flight, flightRate, rateResponse);
        }
    }
}
