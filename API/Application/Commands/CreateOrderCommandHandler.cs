using API.ApiResponses;
using AutoMapper;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderLineRepository _orderLineRepository;
        private readonly IFlightRateRepository _flightRateRepository;
        private readonly IMapper _mapper;
        public CreateOrderCommandHandler
            (
                IOrderRepository orderRepository,
                IOrderLineRepository orderLineRepository,
                IFlightRateRepository flightRateRepository,
                IMapper mapper
            )
        {
            _orderRepository = orderRepository;
            _orderLineRepository = orderLineRepository;
            _flightRateRepository = flightRateRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var flightRate = await _flightRateRepository.Get(request.FlightRateId).FirstOrDefaultAsync();
            if(flightRate== null)
            {
                return default;
            }

            if (request.Slots > flightRate.Available)
            {
                return default;
            }

            var order = _orderRepository.Add(new Order(request.CustomerId, OrderState.DRAFT, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)));
            var orderLine = _orderLineRepository.Add(new OrderLine(order.Id, request.FlightRateId, flightRate.Price.Value, request.Slots));

            orderLine.Order = order;

            await _orderLineRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderResponse>(orderLine);
        }
    }
}
