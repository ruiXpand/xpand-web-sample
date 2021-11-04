using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XpandDEVWebCourse.Business;

namespace XpandDEVWebCouse.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICarsService _carsService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICarsService carsService)
        {
            _logger = logger;
            _carsService = carsService;
        }

        [HttpGet]
        [Route("cars")]
        public async Task<IActionResult> Get()
        {
            var cars = _carsService.GetAllCarsAsync();
            return Ok(cars);
        }
    }
}
