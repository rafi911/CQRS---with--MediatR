using Domain.Common;
using System;

namespace API.ApiResponses
{
    public record FlightRateResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Available { get; set; }
        public string Currency { get; set; }
    }
}
