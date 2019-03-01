using System.Collections.Generic;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrainTicketMachine.Api.CompositionRoot;
using TrainTicketMachine.Domain.Model;
using TrainTicketMachine.EntityFrameworkCore;

namespace TrainTicketMachine.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<Context>(ob => ob.UseInMemoryDatabase("TrainTicketMachine"));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<ApiAutofacModule>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var scope = app.ApplicationServices.CreateScope())
            using (var context = scope.ServiceProvider.GetRequiredService<Context>())
            {
                var stations = new List<Station>
                {
                    new Station { Id = 1, Name = "DARTFORD"},
                    new Station { Id = 2, Name = "DARTMOUTH"},
                    new Station { Id = 3, Name = "TOWER HIL"},
                    new Station { Id = 4, Name = "DERBY"},
                    new Station { Id = 5, Name = "LIVERPOOL"},
                    new Station { Id = 6, Name = "LIVERPOOL LIME STREET"},
                    new Station { Id = 7, Name = "PADDINGTON"},
                    new Station { Id = 8, Name = "EUSTON"},
                    new Station { Id = 9, Name = "LONDON BRIDGE"},
                    new Station { Id = 10, Name = "VICTORIA"}
                };

                context.Stations.AddRange(stations);
                context.SaveChanges();
            }

            app.UseMvc();
        }
    }
}
