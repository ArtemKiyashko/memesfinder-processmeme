using Google.Apis.Customsearch.v1.Data;
using Microsoft.Extensions.Logging;
using ProcessMeme.Interfaces.SearchEngine;

namespace ProcessMeme.Managers.SearchEngine
{
	public class GoogleSearchEngineManager : IGoogleSearchEngineManager
	{
        private const int MAX_RESULT_COUNT = 3;
        private readonly IGoogleSearchEngine _googleSearchEngine;
        private readonly ILogger<GoogleSearchEngineManager> _logger;
        private IList<Result> _results = new List<Result>();
        private ISet<int> _availableLinkIndexes = new HashSet<int>();

		public GoogleSearchEngineManager(IGoogleSearchEngine googleSearchEngine, ILogger<GoogleSearchEngineManager> logger)
		{
            _googleSearchEngine = googleSearchEngine;
			_logger = logger;
        }

        public async ValueTask SearchMemesAsync(string keyword)
        {
            var results = await _googleSearchEngine.GetMemesAsync(keyword);
            _results = results?
                .Where(result => result is not null && !string.IsNullOrWhiteSpace(result.Link))
                .ToList() ?? new List<Result>();

            //allocate indexes sequence to store taken and not taken values from the search result
            _availableLinkIndexes = Enumerable.Range(0, Math.Min(MAX_RESULT_COUNT, _results?.Count ?? 0)).ToHashSet();

            if (_availableLinkIndexes.Count == 0)
                _logger.LogWarning("Image search returned no usable results for query {Query}.", keyword);
            else
                _logger.LogInformation("Image search returned {ResultCount} usable results for query {Query}.", _availableLinkIndexes.Count, keyword);
        }

        public string? GetNextRandomMemeUrl()
        {
            if (_availableLinkIndexes.Count == 0)
                return null;

            //get random index
            var taken = _availableLinkIndexes.ElementAt(Random.Shared.Next(0, _availableLinkIndexes.Count));

            //remove taken index from sequence to prevent re-using
            _availableLinkIndexes.Remove(taken);

            //return random link
            return _results[taken].Link;
        }
    }
}

