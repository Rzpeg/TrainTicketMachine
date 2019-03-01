using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainTicketMachine.Api.Models.Stations;
using TrainTicketMachine.Common.Logging;
using TrainTicketMachine.Domain.Contracts;
using TrainTicketMachine.Domain.Model;

namespace TrainTicketMachine.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private readonly ILogger logger = Logger.For<StationsController>();

        private readonly IStationService stationService;

        public StationsController(IStationService stationService)
        {
            this.stationService = stationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string query)
        {
            try
            {
                var (stations, nextCharacters) = await this.stationService.Search(query)
                    .ConfigureAwait(false);

                var responseModel = new StationsResponse
                {
                    Stations = stations,
                    NextCharacters = nextCharacters
                };

                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}
