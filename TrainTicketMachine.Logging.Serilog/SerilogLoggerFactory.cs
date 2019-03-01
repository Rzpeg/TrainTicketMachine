using TrainTicketMachine.Common.Logging;

namespace TrainTicketMachine.Logging.Serilog
{
    public class SerilogLoggerFactory : ILoggerFactory
    {
        private readonly global::Serilog.ILogger serilogLogger;

        public SerilogLoggerFactory(global::Serilog.ILogger serilogLogger)
        {
            this.serilogLogger = serilogLogger;
        }

        public ILogger For<TType>() 
            => new SerilogLogger(this.serilogLogger.ForContext<TType>());
    }
}
