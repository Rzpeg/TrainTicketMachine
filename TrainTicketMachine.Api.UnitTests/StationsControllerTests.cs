using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using TrainTicketMachine.Api.Controllers;
using TrainTicketMachine.Api.Models.Stations;
using TrainTicketMachine.Common.Logging;
using TrainTicketMachine.Domain.Contracts;
using TrainTicketMachine.Domain.Model;
using Xunit;

namespace TrainTicketMachine.Api.UnitTests
{
    public class StationsControllerTests
    {
        [Fact]
        public async Task Stations_Get_ReturnsUnprocessableEntity_AndLogs_WhenServiceThrows()
        {
            // arrange
            var fixture = new Fixture();
            
            var loggerFactory = A.Fake<ILoggerFactory>();
            var logger = A.Fake<ILogger>();

            A.CallTo(() => loggerFactory.For<StationsController>())
                .Returns(logger);

            Logger.LoggerFactory = loggerFactory;

            var service = A.Fake<IStationService>();
            var query = fixture.Create<string>();
            var error = fixture.Create<Exception>();

            A.CallTo(() => service.Search(query))
                .Throws(error);

            // act
            var sut = new StationsController(service);

            var result = await sut.Get(query)
                .ConfigureAwait(true);
            
            // assert
            result.Should().BeOfType<UnprocessableEntityObjectResult>()
                .Which.Value.Should().BeOfType<string>()
                .Which.Should().BeEquivalentTo(error.Message);

            A.CallTo(() => logger.Error(error))
                .MustHaveHappened();
        }

        [Fact]
        public async Task Stations_Get_ReturnsOk_AndStations()
        {
            // arrange
            var fixture = new Fixture();
            
            var loggerFactory = A.Fake<ILoggerFactory>();
            var logger = A.Fake<ILogger>();

            A.CallTo(() => loggerFactory.For<StationsController>())
                .Returns(logger);

            Logger.LoggerFactory = loggerFactory;

            var service = A.Fake<IStationService>();
            var query = fixture.Create<string>();

            var stations = fixture.Create<(List<Station> stations, List<char> nextCharacters)>();

            A.CallTo(() => service.Search(query))
                .Returns(stations);

            // act
            var sut = new StationsController(service);

            var result = await sut.Get(query)
                .ConfigureAwait(true);

            // assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeOfType<StationsResponse>()
                .Which.Should().BeEquivalentTo(new StationsResponse { Stations = stations.stations, NextCharacters = stations.nextCharacters});
        }
    }
}
