namespace TrainTicketMachine.Common.Logging
{
    public class Logger
    {
        public static ILoggerFactory LoggerFactory { get; set; } = null;

        public static ILogger For<TType>() 
            => LoggerFactory?.For<TType>() ?? throw new MissingLoggerFactoryException();
    }
}
