using System;
using AutoFixture;
using FakeItEasy;
using Xunit;

namespace TrainTicketMachine.Logging.Serilog.UnitTests
{
    public class SerilogLoggerTests
    {
        [Fact]
        public void Info_WritesMessage_IntoSerilog()
        {
            // arrange 
            var fixture = new Fixture();
            var message = fixture.Create<string>();
            var serilog = A.Fake<global::Serilog.ILogger>();

            // act
            var sut = new SerilogLogger(serilog);
            sut.Info(message);

            // assert
            A.CallTo(() => serilog.Information(message))
                .MustHaveHappened();
        }

        [Fact]
        public void Error_WritesException_IntoSerilog()
        {
            // arrange 
            var fixture = new Fixture();
            var exception = fixture.Create<Exception>();
            var serilog = A.Fake<global::Serilog.ILogger>();

            // act
            var sut = new SerilogLogger(serilog);
            sut.Error(exception);

            // assert
            A.CallTo(() => serilog.Error(exception, exception.Message))
                .MustHaveHappened();
        }
    }
}
