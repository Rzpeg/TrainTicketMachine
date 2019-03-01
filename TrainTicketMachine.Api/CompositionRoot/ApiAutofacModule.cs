using Autofac;
using TrainTicketMachine.Caching.InMemory;
using TrainTicketMachine.Common.Caching;
using TrainTicketMachine.Decorators;
using TrainTicketMachine.Domain.Contracts;
using TrainTicketMachine.EntityFrameworkCore;

namespace TrainTicketMachine.Api.CompositionRoot
{
    public class ApiAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<StationRepository>()
                .As<IStationRepository>();

            builder
                .RegisterDecorator<StationRepositoryCachingDecorator, IStationRepository>();

            builder
                .RegisterType<StationService.StationService>()
                .As<IStationService>();

            builder
                .RegisterDecorator<StationServiceCachingDecorator, IStationService>();

            builder
                .RegisterType<InMemoryCachingProvider>()
                .As<ICacheProvider>()
                .SingleInstance();
        }
    }
}
