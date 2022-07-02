using Alefba.Core.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Alefba.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AverageRate : ControllerBase
    {
        private readonly ILogger<AverageRate> _logger;
        private readonly ICurrencyRateTrackerService _currencyRateTrackerService;

        public AverageRate(ILogger<AverageRate> logger,
               ICurrencyRateTrackerService currencyRateTrackerService)
        {
            _logger = logger;
            _currencyRateTrackerService = currencyRateTrackerService;
        }

        [HttpGet(Name = "GetAverage")]
        public async Task<string> GetAverage([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            try
            {
                return await _currencyRateTrackerService.GetAverageAsync(fromDate, toDate);
            }
            catch (Exception ex)
            {
                ControllerContext.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                _logger.LogError(ex.Message);
                return "Please check your parameters (from date, and 'to date') and try again later\n " +
                    "Also you can inspect error details in console window!";
            }
        }
    }
}