using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.FlightAggregate
{
    public class FlightDetail
    {
        public string DepartureAirportCode { get; set; }
        public string ArrivalAirportCode { get; set; }
        public DateTimeOffset Departure { get; set; }
        public DateTimeOffset Arrival { get; set; }
        public decimal PriceFrom { get; set; }
        public Guid FlightRateId { get; set; }
        public Guid FlightId { get; set; }
    }
}
