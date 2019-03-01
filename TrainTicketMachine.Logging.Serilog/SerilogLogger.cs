using System;
using TrainTicketMachine.Common.Logging;

namespace TrainTicketMachine.Logging.Serilog
{
    public class SerilogLogger : ILogger
    {
        private readonly global::Serilog.ILogger serilogLogger;

        public SerilogLogger(global::Serilog.ILogger serilogLogger)
        {
            this.serilogLogger = serilogLogger;
        }

        public void Info(string message)
        {
            this.serilogLogger.Information(message);
        }

        public void Error(Exception ex)
        {
            this.serilogLogger.Error(ex, ex.Message);
        }
    }
}
