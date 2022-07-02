using global::Alefba.Core.Abstracts;

namespace Alefba.Api.BackgroundTasks
{
    public class PriceUpdaterBackgroundTask : IHostedService, IDisposable
    {
        private readonly ILogger<PriceUpdaterBackgroundTask> _logger;
        private Timer? _timer;
        private readonly ICurrencyRateTrackerService _currencyRateTrackerService;

        public PriceUpdaterBackgroundTask(ILogger<PriceUpdaterBackgroundTask> logger, ICurrencyRateTrackerService currencyRateTrackerService)
        {
            _logger = logger;
            _currencyRateTrackerService = currencyRateTrackerService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(GetPriceUpdateAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
            return Task.CompletedTask;
        }

        public async void GetPriceUpdateAsync(object? state)
        {
            try
            {
                var model = await _currencyRateTrackerService.UpdateLastestDollarRateAsync();
                _logger.LogInformation(model.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error! Description => {ex.Message})");
                return;
            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

