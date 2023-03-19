using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.OrderAggregate
{
    public interface IOrderLineRepository: IRepository<OrderLine>
    {
        OrderLine Add(OrderLine orderLine);

        void Update(OrderLine orderLine);

        Task<OrderLine> GetAsync(Guid orderLineId);
        void BulkUpdate(IEnumerable<OrderLine> orderLines);
    }
}
