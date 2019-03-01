using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainTicketMachine.Domain.Contracts;
using TrainTicketMachine.Domain.Model;

namespace TrainTicketMachine.StationService
{
    public class StationService : IStationService
    {
        private readonly IStationRepository stationRepository;

        public StationService(IStationRepository stationRepository)
        {
            this.stationRepository = stationRepository;
        }

        public async Task<(IReadOnlyCollection<Station> stations, IReadOnlyCollection<char> nextCharacters)> Search(string searchString)
        {
            searchString = searchString ?? string.Empty;

            var allStations = await this.stationRepository.GetAllStations()
                .ConfigureAwait(false);

            var nextStations = allStations
                .Where(s => s.Name.StartsWith(searchString, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var nextChars = nextStations
                .Where(p => p.Name.Length > searchString.Length)
                .Select(p => p.Name[searchString.Length])
                .Distinct()
                .ToList();

            return (nextStations, nextChars);
        }
    }
}
