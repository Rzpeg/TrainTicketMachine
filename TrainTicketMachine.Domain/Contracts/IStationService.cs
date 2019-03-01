using System.Collections.Generic;
using System.Threading.Tasks;
using TrainTicketMachine.Domain.Model;

namespace TrainTicketMachine.Domain.Contracts
{
    public interface IStationService
    {
        Task<(IReadOnlyCollection<Station> stations, IReadOnlyCollection<char> nextCharacters)> Search(string searchString);
    }
}