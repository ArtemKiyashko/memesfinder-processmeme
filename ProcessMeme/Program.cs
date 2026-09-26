using Azure.Monitor.OpenTelemetry.Exporter;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Azure.Functions.Worker.OpenTelemetry;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenTelemetry.Trace;
using Azure.Core.Serialization;
using ProcessMeme.Infrastructure.SearchEngine;
using ProcessMeme.Options;
using Telegram.Bot;

var builder = FunctionsApplication.CreateBuilder(args);

AppContext.SetSwitch("Azure.Experimental.EnableActivitySource", true);

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource("Azure.Messaging.ServiceBus.*"))
    .UseFunctionsWorkerDefaults()
    .UseAzureMonitorExporter();

builder.Services.Configure<WorkerOptions>(options =>
    options.Serializer = new NewtonsoftJsonObjectSerializer());

builder.Services.AddGoogleSearch(builder.Configuration);
builder.Services.Configure<TelegramBotOptions>(builder.Configuration.GetSection("TelegramBotOptions"));
builder.Services.AddSingleton<ITelegramBotClient>(provider =>
    new TelegramBotClient(provider.GetRequiredService<IOptions<TelegramBotOptions>>().Value.Token));

builder.Build().Run();
