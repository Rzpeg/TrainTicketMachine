using System;
using FluentAssertions;
using TrainTicketMachine.Common.Logging;
using Xunit;

namespace TrainTicketMachine.Common.UnitTests
{
    public class MissingLoggerFactoryExceptionTests
    {
        [Fact]
        public void Ctor_HasDefaultMessage()
        {
            // arrange
            // act
            var sut = new MissingLoggerFactoryException();

            // assert
            sut.Message.Should().BeEquivalentTo("Please check logging configuration");
        }
    }
}
