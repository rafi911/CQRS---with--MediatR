using Domain.Aggregates.OrderAggregate;
using System.Collections.Generic;
using System;

namespace API.ApiResponses
{
    public record OrderResponse (Guid CustomerId, string State, DateTimeOffset Created, Guid OrderId, decimal TotalPrice, Guid FlightRateId)
    {
    }
}
