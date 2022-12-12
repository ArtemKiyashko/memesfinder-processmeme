using System;
using Azure.Identity;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessMeme.Extensions;

[assembly: FunctionsStartup(typeof(ProcessMeme.Startup))]
namespace ProcessMeme
{
	public class Startup : FunctionsStartup
	{
        private IConfigurationRoot _functionConfig;

        public override void Configure(IFunctionsHostBuilder builder)
        {

            var configBuilder = new ConfigurationBuilder()
                .AddEnvironmentVariables();

            if (builder.IsProduction())
                configBuilder.AddAzureKeyVault(
                    new Uri($"https://{builder.GetContext().Configuration["KeyVaultName"]}.vault.azure.net/"),
                    new DefaultAzureCredential());

            _functionConfig = configBuilder.Build();
            builder.Services.AddLogging();
        }
    }
}

