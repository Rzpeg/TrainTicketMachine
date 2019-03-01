using System;

namespace TrainTicketMachine.Common.Logging
{
    public interface ILogger
    {
        void Info(string message);
        void Error(Exception ex);
    }
}