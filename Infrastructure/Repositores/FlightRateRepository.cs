﻿using Domain.Aggregates.FlightAggregate;
using Domain.Common;
using Domain.Events;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositores
{
    public class FlightRateRepository : IFlightRateRepository
    {
        private readonly FlightsContext _context;

        public FlightRateRepository(FlightsContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public IQueryable<FlightRate> Get(Guid id)
        {
            return _context.FlightRates.Where(fr => fr.Id== id);
        }

        public async Task<List<FlightRate>> GetFlightRatesAsync(List<Guid> ids)
        {
            return await _context.FlightRates.Where(f => ids.Contains(f.Id)).ToListAsync();
        }
        public async Task<List<FlightRate>> GetFlightRatesAsync()
        {
            return await _context.FlightRates.AsNoTracking().ToListAsync();
        }

        public void Update(FlightRate flightRate)
        {
            _context.Update(flightRate);
        }
    }
}
