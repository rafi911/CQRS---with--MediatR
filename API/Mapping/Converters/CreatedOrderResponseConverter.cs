using API.ApiResponses;
using AutoMapper;
using Domain.Aggregates.OrderAggregate;
using System.Linq;

namespace API.Mapping.Converters
{
    public class CreatedOrderResponseConverter : ITypeConverter<OrderLine, OrderResponse>
    {
        public OrderResponse Convert(OrderLine source, OrderResponse destination, ResolutionContext context)
        {
            if (source is null)
            {
                return default;
            }

            return new OrderResponse
            (
                OrderId: source.OrderId,
                CustomerId: source.Order.CustomerId,
                State: source.Order.State.ToString(),
                Created: source.Order.OrderDate,
                TotalPrice: source.Price * source.Slots,
                FlightRateId: source.FlightRateId
            );
        }
    }
}
