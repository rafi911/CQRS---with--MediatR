using API.ApiResponses;
using AutoMapper;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Commands
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IFlightRateRepository _flightRateRepository;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IFlightRateRepository flightRateRepository, IOrderLineRepository orderLineRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _flightRateRepository = flightRateRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(request.OrderId);
            if (order.State == OrderState.CONFIRMED) 
            {
                return default;
            }
            order.ChangeState(OrderState.CONFIRMED);

            _orderRepository.Update(order);

            //for the movement, consider relationshop between oder & orderline is 1:1 
            var oderLine = order.OrderLines.FirstOrDefault();

            var flightRate = await _flightRateRepository.Get(oderLine.FlightRateId).FirstOrDefaultAsync();

            // Reduce available slots from source
            flightRate.ReduceAvailability(oderLine.Slots);

            _flightRateRepository.Update(flightRate);

            await _orderRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderResponse>(oderLine);
        }
    }
}
