using System.Collections.Generic;
using System.Threading.Tasks;
using TrainTicketMachine.Domain.Model;

namespace TrainTicketMachine.Domain.Contracts
{
    public interface IStationRepository
    {
        Task<IReadOnlyCollection<Station>> GetAllStations();
    }
}