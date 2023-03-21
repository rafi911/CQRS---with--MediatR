using Domain.Aggregates.AirportAggregate;
using Domain.Aggregates.FlightAggregate;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositores
{
    public class FlightRepository : IFlightRepository
    {
        private readonly FlightsContext _context;

        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public FlightRepository(FlightsContext context)
        {
            _context = context;
        }

        public Flight Add(Flight flight)
        {
            return _context.Flights.Add(flight).Entity;
        }

        public async Task<Flight> GetAsync(Guid flightId)
        {
            return await _context.Flights.FindAsync(flightId);
        }

        public async Task<List<FlightDetail>> GetFlights(string destination)
        {
            return await (from flight in _context.Flights
                          join destinationAirport in _context.Airports on flight.DestinationAirportId equals destinationAirport.Id
                          join originAirport in _context.Airports on flight.OriginAirportId equals originAirport.Id
                          join flightRate in _context.FlightRates on flight.Id equals flightRate.FlightId
                          where destinationAirport.Name.Contains(destination) || destinationAirport.Code.Contains(destination)
                          group new { flight , destinationAirport , originAirport , flightRate } by new 
                            { 
                              flight.Id,                            
                              departureAirportCode = originAirport.Code,
                              ArrivaleAirportCode = destinationAirport.Code,
                          } into g
                          let cheapestFlightRate = g.Select(aggegate => aggegate.flightRate).OrderBy(rate => rate.Price.Value).FirstOrDefault()
                          select new FlightDetail
                          {
                              FlightId = g.Key.Id,
                              DepartureAirportCode = g.Key.departureAirportCode,
                              ArrivalAirportCode = g.Key.ArrivaleAirportCode,
                              Arrival = g.Select(aggegate => aggegate.flight.Arrival).FirstOrDefault(),
                              Departure = g.Select(aggegate => aggegate.flight.Departure).FirstOrDefault(),
                              PriceFrom = cheapestFlightRate.Price.Value,
                              FlightRateId = cheapestFlightRate.Id
                          }).ToListAsync();
        }

        public void Update(Flight flight)
        {
            throw new NotImplementedException();
        }
    }
}
