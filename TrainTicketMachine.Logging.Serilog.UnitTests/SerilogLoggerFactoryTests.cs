using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace TrainTicketMachine.Logging.Serilog.UnitTests
{
    public class SerilogLoggerFactoryTests
    {
        [Fact]
        public void For_ReturnsNewLogger()
        {
            // arrange
            var serilog = A.Fake<global::Serilog.ILogger>();

            // act
            var sut = new SerilogLoggerFactory(serilog);
            var logger = sut.For<SerilogLoggerFactoryTests>();

            // assert
            logger.Should().BeOfType<SerilogLogger>();

            A.CallTo(() => serilog.ForContext<SerilogLoggerFactoryTests>())
                .MustHaveHappened();
        }
    }
}
