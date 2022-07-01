using Alefba.Core.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Alefba.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyRateTrackerController : ControllerBase
    {
        private readonly ILogger<CurrencyRateTrackerController> _logger;
        private readonly ICurrencyRateTrackerService _currencyRateTrackerService;

        public CurrencyRateTrackerController(ILogger<CurrencyRateTrackerController> logger,
               ICurrencyRateTrackerService currencyRateTrackerService)
        {
            _logger = logger;
            _currencyRateTrackerService = currencyRateTrackerService;
        }

        [HttpGet(Name = "GetAverage")]
        public async Task<double> GetAverage([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            double average = await _currencyRateTrackerService.GetAverageAsync(fromDate, toDate);
            return average;
        }
    }
}