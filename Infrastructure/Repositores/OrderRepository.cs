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
    public class OrderRepository : IOrderRepository
    {
        private readonly FlightsContext _context;

        public OrderRepository(FlightsContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public Order Add(Order order)
        {
            return _context.Orders.Add(order).Entity;
        }

        public async Task<Order> GetAsync(Guid orderId)
        {
            return await _context.Orders.Where(o => o.Id == orderId).Include(o => o.OrderLines).FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetDraftOrdersAsync(Guid FlightRateId)
        {
            var orderIds = _context.OrderLines.Where(ol => ol.FlightRateId == FlightRateId).Select(ol => ol.OrderId);
            if (orderIds is null)
            {
                return new List<Order>();
            }
            var orders = await _context.Orders.Where(o => orderIds.Contains(o.Id) && o.State == OrderState.DRAFT)
                    .Include(o => o.OrderLines).ToListAsync();

            return orders;
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }
    }
}
