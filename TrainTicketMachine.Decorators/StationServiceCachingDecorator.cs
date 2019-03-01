using System.Collections.Generic;
using System.Threading.Tasks;
using TrainTicketMachine.Common.Caching;
using TrainTicketMachine.Domain.Contracts;
using TrainTicketMachine.Domain.Model;

namespace TrainTicketMachine.Decorators
{
    public class StationServiceCachingDecorator : IStationService
    {
        private readonly IStationService decorated;
        private readonly ICacheProvider cacheProvider;

        public StationServiceCachingDecorator(IStationService decorated, ICacheProvider cacheProvider)
        {
            this.decorated = decorated;
            this.cacheProvider = cacheProvider;
        }

        public async Task<(IReadOnlyCollection<Station> stations, IReadOnlyCollection<char> nextCharacters)> Search(string searchString)
        {
            return await this.cacheProvider.GetOrSet(
                    $"{nameof(StationServiceCachingDecorator)}|{nameof(Search)}|{searchString}",
                    () => this.decorated.Search(searchString))
                .ConfigureAwait(false);
        }
    }
}
