using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProcessMeme.Infrastructure.SearchEngine;
using ProcessMeme.Options;
using Telegram.Bot;

[assembly: FunctionsStartup(typeof(ProcessMeme.Startup))]
namespace ProcessMeme
{
    public class Startup : FunctionsStartup
    {
        private IConfigurationRoot _functionConfig;

        public override void Configure(IFunctionsHostBuilder builder)
        {
            _functionConfig = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddGoogleSearch(_functionConfig);
            builder.Services.AddLogging();

            builder.Services.Configure<TelegramBotOptions>(_functionConfig.GetSection("TelegramBotOptions"));
            builder.Services.AddSingleton<ITelegramBotClient>(factory => new TelegramBotClient(factory.GetService<IOptions<TelegramBotOptions>>().Value.Token));
        }
    }
}

