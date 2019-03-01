using System.Collections.Generic;
using TrainTicketMachine.Domain.Model;

namespace TrainTicketMachine.Api.Models.Stations
{
    public class StationsResponse
    {
        public IReadOnlyCollection<Station> Stations { get; set; }
        public IReadOnlyCollection<char> NextCharacters { get; set; }
    }
}
