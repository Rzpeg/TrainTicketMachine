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
    public class StationServiceCachingDecoratorTests
    {
        [Fact]
        public async Task GetAllStations_SetupsCache()
        {
            // arrange
            var fixture = new Fixture();

            var service = A.Fake<IStationService>();
            var cache = A.Fake<ICacheProvider>();

            var query = fixture.Create<string>();

            var expected = fixture.Create<(IReadOnlyCollection<Station>, IReadOnlyCollection<char>)>();

            A.CallTo(() =>
                    cache.GetOrSet(
                        $"{nameof(StationServiceCachingDecorator)}|{nameof(StationServiceCachingDecorator.Search)}|{query}",
                        A<Func<Task<(IReadOnlyCollection<Station>, IReadOnlyCollection<char>)>>>.Ignored))
                .Returns(expected);

            // act
            var sut = new StationServiceCachingDecorator(service, cache);

            var actual = await sut.Search(query)
                .ConfigureAwait(true);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
