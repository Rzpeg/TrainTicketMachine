using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using TrainTicketMachine.Common.Caching;
using TrainTicketMachine.Domain.Contracts;
using TrainTicketMachine.Domain.Model;
using Xunit;

namespace TrainTicketMachine.Decorators.UnitTests
{
    public class StationRepositoryCachingDecoratorTests
    {
        [Fact]
        public async Task GetAllStations_SetupsCache()
        {
            // arrange
            var fixture = new Fixture();

            var repo = A.Fake<IStationRepository>();
            var cache = A.Fake<ICacheProvider>();

            var expected = fixture
                .CreateMany<Station>()
                .ToList();

            A.CallTo(() =>
                    cache.GetOrSet(
                        $"{nameof(StationRepositoryCachingDecorator)}|{nameof(StationRepositoryCachingDecorator.GetAllStations)}",
                        A<Func<Task<IReadOnlyCollection<Station>>>>.Ignored))
                .Returns(expected);

            // act
            var sut = new StationRepositoryCachingDecorator(repo, cache);

            var actual = await sut.GetAllStations()
                .ConfigureAwait(true);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
