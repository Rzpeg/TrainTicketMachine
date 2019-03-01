using Microsoft.EntityFrameworkCore;
using TrainTicketMachine.Domain.Model;

namespace TrainTicketMachine.EntityFrameworkCore
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options): base(options)
        {
        }

        public DbSet<Station> Stations { get; set; }
    }
}
