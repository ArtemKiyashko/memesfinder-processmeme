using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ProcessMeme.Interfaces.SearchEngine;

namespace ProcessMeme
{
    public class ProcessMeme
    {
        private readonly ILogger<ProcessMeme> _logger;
        private readonly IGoogleSearchEngineManager _googleSearchEngineManager;

        public ProcessMeme(ILogger<ProcessMeme> log, IGoogleSearchEngineManager googleSearchEngineManager)
        {
            _logger = log;
            _googleSearchEngineManager = googleSearchEngineManager;
        }

        [FunctionName("ProcessMeme")]
        public void Run([ServiceBusTrigger("keywordmessages", "memeprocessor", Connection = "ServiceBusOptions")]string mySbMsg)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}

