using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TrainTicketMachine.Domain.Model;
using Xunit;

namespace TrainTicketMachine.EntityFrameworkCore.UnitTests
{
    public class StationRepositoryTests
    {
        [Fact]
        public async Task GetAllStations_ReturnsAllStations()
        {
            // arrange 
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var fixture = new Fixture();

            var expected = fixture
                .CreateMany<Station>()
                .ToList();

            using (var context = new Context(options))
            {
                await context.Database.EnsureDeletedAsync()
                    .ConfigureAwait(false);

                await context.Stations.AddRangeAsync(expected)
                    .ConfigureAwait(false);

                await context.SaveChangesAsync()
                    .ConfigureAwait(false);
            }
            
            // act
            using (var context = new Context(options))
            {
                var sut = new StationRepository(context);

                var actual = await sut.GetAllStations()
                    .ConfigureAwait(false);

                // assert
                actual.Should().BeEquivalentTo(expected);
            }
        }
    }
}
