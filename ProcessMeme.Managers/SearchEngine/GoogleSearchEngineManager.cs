using System;
using ProcessMeme.Interfaces.SearchEngine;

namespace ProcessMeme.Managers.SearchEngine
{
	public class GoogleSearchEngineManager : IGoogleSearchEngineManager
	{
        private readonly IGoogleSearchEngine _googleSearchEngine;

        public GoogleSearchEngineManager(IGoogleSearchEngine googleSearchEngine)
		{
            _googleSearchEngine = googleSearchEngine;
        }

        public async ValueTask<string?> GetMemeLinkAsync(string query)
        {
            var memesResult = await _googleSearchEngine.GetMemesAsync(query);
            return memesResult.GetRandomResult()?.Link;
        }
    }
}

