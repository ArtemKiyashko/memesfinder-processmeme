using Microsoft.Extensions.Logging;
using ProcessMeme.Interfaces.SearchEngine;

namespace ProcessMeme.Managers.Decorators
{
    public class LoggedGoogleSearchEngineManager : IGoogleSearchEngineManager
    {
        private IGoogleSearchEngineManager _decorotee;
        private ILogger _logger;

        public LoggedGoogleSearchEngineManager(IGoogleSearchEngineManager decorotee, ILogger logger)
        {
            _decorotee = decorotee;
            _logger = logger;
        }

        public async ValueTask<string?> GetMemeLinkAsync(string keyword)
        {
            _logger.LogInformation("Keyword is: {}", keyword);
            var memeLink = await _decorotee.GetMemeLinkAsync(keyword);
            _logger.LogInformation("Meme link is: {}", memeLink);
            return memeLink;
        }
    }
}
