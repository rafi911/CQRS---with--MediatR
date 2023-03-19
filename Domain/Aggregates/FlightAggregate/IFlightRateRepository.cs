using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.FlightAggregate
{
    public interface IFlightRateRepository: IRepository<FlightRate>
    {
        void Update(FlightRate flightRate);
        IQueryable<FlightRate> Get(Guid id);
        Task<List<FlightRate>> GetFlightRatesAsync(List<Guid> ids);
        Task<List<FlightRate>> GetFlightRatesAsync();
    }
}
