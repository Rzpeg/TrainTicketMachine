using System;

namespace TrainTicketMachine.Common.Logging
{
    public class MissingLoggerFactoryException : Exception
    {
        public MissingLoggerFactoryException() : base("Please check logging configuration")
        {
            
        }
    }
}