using Alefba.Api.BackgroundTasks;
using Alefba.Core.Abstracts;
using Alefba.Infrastructure.Repository;
using Alefba.Infrastructure.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLocalServices(this IServiceCollection services)
        {
             services.AddHostedService<PriceUpdaterBackgroundTask>();
            services.AddSingleton<ICurrencyRateTrackerService, CurrencyRateTrackerService>();
            services.AddSingleton<IPriceTrackerRepository, PriceTrackerRepository>();
            services.AddSingleton<IScraperService, ScraperService>();
            return services;
        }
    }
}
