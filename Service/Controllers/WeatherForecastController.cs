using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.IBusiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer.ViewModel;
using Service.Utility;

namespace Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [TypeFilter(typeof(CustomExceptionFilter))]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITestOpertaions ItestOpertaions;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,ITestOpertaions testOpertaions)
        {
            _logger = logger;
            this.ItestOpertaions = testOpertaions;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("api/test")]
        public IActionResult Test()
        {
            
            return Ok(ItestOpertaions.getTestList());
        }

        public class t 
        {
            public int a { get; set; } 
        }
        [HttpPost]
        [Route("api/exceptiontest")]
        [AllowAnonymous]
   //     [CustomExceptionFilter]
        public IActionResult ExceptionTest([FromBody]t t)
        {
            int b = 10;
            int c = 0;
            int a = b/c;
            // throw new Exception("Testing custom exception filter.");
            return Ok("Complete");
        }

    }
}
