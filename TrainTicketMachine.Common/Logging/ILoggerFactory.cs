namespace TrainTicketMachine.Common.Logging
{
    public interface ILoggerFactory
    {
        ILogger For<TType>();
    }
}