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

        public async ValueTask<string?> GetMemeLinkAsync(string keyword)
        {
            var memesResult = await _googleSearchEngine.GetMemesAsync(keyword);
            return memesResult.GetRandomResult()?.Link;
        }
    }
}

