using System.Collections.Generic;
using System.Threading.Tasks;
using TrainTicketMachine.Common.Caching;
using TrainTicketMachine.Domain.Contracts;
using TrainTicketMachine.Domain.Model;

namespace TrainTicketMachine.Decorators
{
    public class StationRepositoryCachingDecorator : IStationRepository
    {
        private readonly IStationRepository decorated;
        private readonly ICacheProvider cacheProvider;

        public StationRepositoryCachingDecorator(IStationRepository decorated, ICacheProvider cacheProvider)
        {
            this.decorated = decorated;
            this.cacheProvider = cacheProvider;
        }

        public Task<IReadOnlyCollection<Station>> GetAllStations() 
            => this.cacheProvider.GetOrSet($"{nameof(StationRepositoryCachingDecorator)}|{nameof(GetAllStations)}", this.decorated.GetAllStations);
    }
}
