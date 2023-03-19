using System;

namespace API.ApiResponses;

public record FlightResponse(Guid FlightId, Guid FlightRateId, string DepartureAirportCode, string ArrivalAirportCode, DateTimeOffset Departure, DateTimeOffset Arrival, decimal PriceFrom);