using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Castle.Core.Logging;
using FakeItEasy;
using FluentAssertions;
using TrainTicketMachine.Domain.Contracts;
using TrainTicketMachine.Domain.Model;
using Xunit;

namespace TrainTicketMachine.StationService.UnitTests
{
    public class StationServiceTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task Search_ReturnsListOfAllStations_AndNextLetters_WhenInputIsNullOrEmpty(string search)
        {
            // arrange
            var stations = new List<Station>
            {
                new Station { Id = 7, Name = "PADDINGTON"},
                new Station { Id = 8, Name = "EUSTON"},
                new Station { Id = 9, Name = "LONDON BRIDGE"},
                new Station { Id = 10, Name = "VICTORIA"}
            };
            
            var repo = A.Fake<IStationRepository>();

            A.CallTo(() => repo.GetAllStations())
                .Returns(stations);

            var expectedNextChars = new List<char>
            {
                'P',
                'E',
                'L',
                'V'
            };

            var expected = (stations, expectedNextChars);

            // act
            var sut = new StationService(repo);
            var actual = await sut.Search(search)
                .ConfigureAwait(true);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("D", new [] { "DARTFORD","DARTMOUTH" }, new[] { 'A' })]
        [InlineData("DA", new [] { "DARTFORD","DARTMOUTH" }, new[] { 'R' })]
        [InlineData("DAR", new [] { "DARTFORD","DARTMOUTH" }, new[] { 'T' })]
        [InlineData("DART", new [] { "DARTFORD","DARTMOUTH" }, new[] { 'F', 'M' })]
        [InlineData("DARTF", new [] { "DARTFORD" }, new[] { 'O' })]
        [InlineData("DARTFO", new [] { "DARTFORD" }, new[] { 'R' })]
        [InlineData("DARTFOR", new [] { "DARTFORD" }, new[] { 'D' })]
        [InlineData("DARTFORD", new [] { "DARTFORD" }, new char[] {  })]
        [InlineData("DARTM", new [] { "DARTMOUTH" }, new[] { 'O' })]
        [InlineData("DARTMO", new [] { "DARTMOUTH" }, new[] { 'U' })]
        [InlineData("DARTMOU", new [] { "DARTMOUTH" }, new[] { 'T' })]
        [InlineData("DARTMOUT", new [] { "DARTMOUTH" }, new[] { 'H' })]
        [InlineData("DARTMOUTH", new [] { "DARTMOUTH" }, new char[] { })]
        public async Task Search_ReturnsListOfMatchingStations_AndNextLetters_WhenInputIsPartial(string search, string[] outputStations, char[] outputNextChars)
        {
            // arrange
            var stations = new List<Station>
            {
                new Station { Id = 1, Name = "DARTFORD"},
                new Station { Id = 2, Name = "DARTMOUTH"},
                new Station { Id = 7, Name = "PADDINGTON"},
                new Station { Id = 8, Name = "EUSTON"},
                new Station { Id = 9, Name = "LONDON BRIDGE"},
                new Station { Id = 10, Name = "VICTORIA"}
            };
            
            var repo = A.Fake<IStationRepository>();

            A.CallTo(() => repo.GetAllStations())
                .Returns(stations);
            
            var expected = (stations.Where(s => outputStations.Any(os => os == s.Name)).ToList(), outputNextChars.ToList());

            // act
            var sut = new StationService(repo);
            var actual = await sut.Search(search)
                .ConfigureAwait(true);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
