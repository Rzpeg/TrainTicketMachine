using System;
using System.Threading.Tasks;
using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace TrainTicketMachine.Caching.InMemory.UnitTests
{
    public class InMemoryCachingProviderTests
    {
        [Fact]
        public async Task GetOrSet_CorrectlyCachesData()
        {
            // arrange
            var fixture = new Fixture();

            var cacheKey = fixture.Create<string>();
            var expected = fixture.Create<SomeData>();

            var setter = A.Fake<Func<Task<SomeData>>>();
            A.CallTo(() => setter()).Returns(Task.FromResult(expected));

            // act
            var sut = new InMemoryCachingProvider();

            var actual = await sut.GetOrSet(cacheKey, setter)
                .ConfigureAwait(true);

            // assert
            actual.Should().BeEquivalentTo(expected);
            A.CallTo(() => setter()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetOrSet_CorrectlyRetrievesDataWithoutReCaching()
        {
            // arrange
            var fixture = new Fixture();

            var cacheKey = fixture.Create<string>();
            var expected = fixture.Create<SomeData>();
            
            var setter = A.Fake<Func<Task<SomeData>>>();
            A.CallTo(() => setter()).Returns(Task.FromResult(expected));

            var sut = new InMemoryCachingProvider();

            await sut.GetOrSet(cacheKey, () => Task.FromResult(expected))
                .ConfigureAwait(true);

            // act
            
            var actual = await sut.GetOrSet(cacheKey, setter)
                .ConfigureAwait(true);

            // assert
            actual.Should().BeEquivalentTo(expected);
            A.CallTo(() => setter()).MustNotHaveHappened();
        }
    }
}
