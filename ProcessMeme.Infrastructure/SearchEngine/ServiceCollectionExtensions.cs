using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProcessMeme.Interfaces.Infrastructure;
using ProcessMeme.Interfaces.SearchEngine;
using ProcessMeme.Managers.Decorators;
using ProcessMeme.Managers.Infrastructure;
using ProcessMeme.Managers.SearchEngine;
using static Google.Apis.Customsearch.v1.CseResource;

namespace ProcessMeme.Infrastructure.SearchEngine
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGoogleSearch(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SearchEngineOptions>(configuration.GetSection("SearchEngineOptions"));

            services.AddTransient<IGoogleSearchEngine, GoogleSearchEngine>();
            services.AddTransient<GoogleSearchEngineManager>();
            services.AddScoped<IGoogleSearchEngineManager, LoggedGoogleSearchEngineManager>(
                fabric => new LoggedGoogleSearchEngineManager(fabric.GetRequiredService<GoogleSearchEngineManager>(),
                fabric.GetRequiredService<ILogger<LoggedGoogleSearchEngineManager>>()));
            services.AddSingleton<CustomsearchService>(provider =>
            {
                var options = provider.GetRequiredService<IOptions<SearchEngineOptions>>().Value;
                return new CustomsearchService(new BaseClientService.Initializer { ApiKey = options.ApiKey });
            });
            services.AddTransient<ISearchTextProvider, SearchTextProvider>();
            services.AddTransient<ListRequest>(provider =>
            {
                var client = provider.GetRequiredService<CustomsearchService>();
                var options = provider.GetRequiredService<IOptions<SearchEngineOptions>>().Value;

                var listRequest = client.Cse.List();
                listRequest.Cx = options.EngineId;
                listRequest.SearchType = ListRequest.SearchTypeEnum.Image;
                listRequest.Cr = options.CountryRestriction;
                return listRequest;
            });
            return services;
        }
    }
}

