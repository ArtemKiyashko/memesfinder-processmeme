using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace ProcessMeme
{
    public class ProcessMeme
    {
        private readonly ILogger<ProcessMeme> _logger;

        public ProcessMeme(ILogger<ProcessMeme> log)
        {
            _logger = log;
        }

        [FunctionName("ProcessMeme")]
        public void Run([ServiceBusTrigger("keywordmessages", "memeprocessor", Connection = "ServiceBusOptions")]string mySbMsg)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}

