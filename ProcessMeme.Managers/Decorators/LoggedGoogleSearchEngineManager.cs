using Microsoft.Extensions.Logging;
using ProcessMeme.Interfaces.SearchEngine;

namespace ProcessMeme.Managers.Decorators
{
    public class LoggedGoogleSearchEngineManager : IGoogleSearchEngineManager
    {
        private IGoogleSearchEngineManager _decorotee;
        private ILogger<LoggedGoogleSearchEngineManager> _logger;

        public LoggedGoogleSearchEngineManager(IGoogleSearchEngineManager decorotee, ILogger<LoggedGoogleSearchEngineManager> logger)
        {
            _decorotee = decorotee;
            _logger = logger;
        }

        public async ValueTask SearchMemesAsync(string keyword)
        {
            _logger.LogInformation("Keyword is: {}", keyword);
            await _decorotee.SearchMemesAsync(keyword);
        }

        public string GetNextRandomMemeUrl()
        {
            _logger.LogInformation("Getting next meme URL");
            var memeUrl = _decorotee.GetNextRandomMemeUrl();
            _logger.LogInformation($"Meme link is: {memeUrl}");
            return memeUrl;
        }
    }
}
