using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TechTalk.SpecFlow;
using TrainTicketMachine.Caching.InMemory;
using TrainTicketMachine.Decorators;
using TrainTicketMachine.Domain.Contracts;
using TrainTicketMachine.Domain.Model;
using TrainTicketMachine.EntityFrameworkCore;

namespace TrainTicketMachine.AcceptanceTests
{
    [Binding]
    public class StationScenariosSteps : IDisposable
    {
        private readonly Context stationContext;
        private readonly IStationService stationService;

        private string currentQuery;

        public StationScenariosSteps()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            this.stationContext = new Context(options);

            var cacheProvider = new InMemoryCachingProvider();
            
            var repository = new StationRepository(this.stationContext);
            var cachingRepository = new StationRepositoryCachingDecorator(repository, cacheProvider);
            var service = new StationService.StationService(cachingRepository);
            var cachingService = new StationServiceCachingDecorator(service, cacheProvider);

            this.stationService = cachingService;
        }

        [Given(@"a list of four stations ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)""")]
        public async Task GivenAListOfFourStations(string p0, string p1, string p2, string p3)
        {
            this.stationContext.Stations.Add(new Station
            {
                Id = 1,
                Name = p0
            });

            this.stationContext.Stations.Add(new Station
            {
                Id = 2,
                Name = p1
            });

            this.stationContext.Stations.Add(new Station
            {
                Id = 3,
                Name = p2
            });

            this.stationContext.Stations.Add(new Station
            {
                Id = 4,
                Name = p3
            });

            await this.stationContext.SaveChangesAsync()
                .ConfigureAwait(true);
        }
        
        [Given(@"a list of three stations ""(.*)"", ""(.*)"", ""(.*)""")]
        public async Task GivenAListOfThreeStations(string p0, string p1, string p2)
        {
            this.stationContext.Stations.Add(new Station
            {
                Id = 1,
                Name = p0
            });

            this.stationContext.Stations.Add(new Station
            {
                Id = 2,
                Name = p1
            });

            this.stationContext.Stations.Add(new Station
            {
                Id = 3,
                Name = p2
            });

            await this.stationContext.SaveChangesAsync()
                .ConfigureAwait(true);
        }
        
        [When(@"input ""(.*)""")]
        public void WhenInput(string p0)
        {
            this.currentQuery = p0;
        }
        
        [Then(@"should return characters: '(.*)', '(.*)' and stations: ""(.*)"", ""(.*)""")]
        public async Task ThenShouldReturnCharactersAndStations(char p0, char p1, string p2, string p3)
        {
            var (stations, nextCharacters) = await this.stationService.Search(this.currentQuery)
                .ConfigureAwait(true);

            var expectedChars = new List<char> {p0, p1};
            var expectedStations = new List<string> {p2, p3};

            nextCharacters.Should().BeEquivalentTo(expectedChars);
            stations.Select(s => s.Name).Should().BeEquivalentTo(expectedStations);

        }
        
        [Then(@"should return character: '(.*)' and stations: ""(.*)"", ""(.*)""")]
        public async Task ThenShouldReturnCharacterAndStations(char p0, string p1, string p2)
        {
            var (stations, nextCharacters) = await this.stationService.Search(this.currentQuery)
                .ConfigureAwait(true);

            var expectedChars = new List<char> {p0};
            var expectedStations = new List<string> {p1, p2};

            nextCharacters.Should().BeEquivalentTo(expectedChars);
            stations.Select(s => s.Name).Should().BeEquivalentTo(expectedStations);
        }
        
        [Then(@"should return no stations and no characters")]
        public async Task ThenShouldReturnNoStationsAndNoCharacters()
        {
            var (stations, nextCharacters) = await this.stationService.Search(this.currentQuery)
                .ConfigureAwait(true);

            stations.Should().BeEmpty();
            nextCharacters.Should().BeEmpty();
        }

        public void Dispose()
        {
            this.stationContext?.Dispose();
        }
    }
}
