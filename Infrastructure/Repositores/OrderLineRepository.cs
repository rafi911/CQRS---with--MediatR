using Domain.Aggregates.OrderAggregate;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositores
{
    public class OrderLineRepository : IOrderLineRepository
    {
        private readonly FlightsContext _context;

        public OrderLineRepository(FlightsContext flightsContext)
        {
            _context = flightsContext;
        }

        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public OrderLine Add(OrderLine orderLine)
        {
            return _context.OrderLines.Add(orderLine).Entity;
        }

        public async Task<OrderLine> GetAsync(Guid orderLineId)
        {
            return await _context.OrderLines.FirstOrDefaultAsync(o => o.Id == orderLineId);
        }

        public void Update(OrderLine orderLine)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderLine.OrderId);
            if (order != null && order.State == OrderState.DRAFT)
            {
                _context.OrderLines.Update(orderLine);
            }            
        }
    }
}
