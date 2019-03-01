using System;
using FakeItEasy;
using FluentAssertions;
using TrainTicketMachine.Common.Logging;
using Xunit;

namespace TrainTicketMachine.Common.UnitTests
{
    public class LoggerTests
    {
        [Fact]
        public void For_ShouldThrow_WhenNoFactorySet()
        {
            // arrange
            Logger.LoggerFactory = null;

            // act
            Action act = () => Logger.For<LoggerTests>();

            // assert
            act.Should().ThrowExactly<MissingLoggerFactoryException>();
        }

        [Fact]
        public void For_ShouldReturnLogger_WhenFactoryIsSet()
        {
            // arrange
            var factory = A.Fake<ILoggerFactory>();
            var expected = A.Fake<ILogger>();

            A.CallTo(() => factory.For<LoggerTests>())
                .Returns(expected);

            Logger.LoggerFactory = factory;

            // act
            var actual = Logger.For<LoggerTests>();

            // assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
