using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainTicketMachine.Domain.Contracts;
using TrainTicketMachine.Domain.Model;

namespace TrainTicketMachine.EntityFrameworkCore
{
    public class StationRepository : IStationRepository
    {
        private readonly Context context;

        public StationRepository(Context context)
        {
            this.context = context;
        }

        public async Task<IReadOnlyCollection<Station>> GetAllStations()
        {
            var stations = await this.context.Stations
                .ToListAsync()
                .ConfigureAwait(false);

            return stations;
        }
    }
}
