using Google.Apis.Customsearch.v1.Data;
using ProcessMeme.Interfaces.SearchEngine;

namespace ProcessMeme.Managers.SearchEngine
{
	public class GoogleSearchEngineManager : IGoogleSearchEngineManager
	{
        private const int MAX_RESULT_COUNT = 3;
        private readonly IGoogleSearchEngine _googleSearchEngine;
        private IList<Result> _results = new List<Result>();
        private ISet<int> _availableLinkIndexes = new HashSet<int>();

        public GoogleSearchEngineManager(IGoogleSearchEngine googleSearchEngine)
		{
            _googleSearchEngine = googleSearchEngine;
        }

        public async ValueTask SearchMemesAsync(string keyword)
        {
            _results = await _googleSearchEngine.GetMemesAsync(keyword);

            //allocate indexes sequence to store taken and not taken values from the search result
            _availableLinkIndexes = Enumerable.Range(0, Math.Min(MAX_RESULT_COUNT, _results?.Count ?? 0)).ToHashSet();
        }

        public string GetNextRandomMemeUrl()
        {
            if (_availableLinkIndexes.Count == 0)
                throw new ArgumentException("All memes consumed. Perform new search or increase MAX_RESULT_COUNT if you want more");

            //get random index
            var taken = _availableLinkIndexes.ElementAt(Random.Shared.Next(0, _availableLinkIndexes.Count));

            //remove taken index from sequence to prevent re-using
            _availableLinkIndexes.Remove(taken);

            //return random link
            return _results[taken].Link;
        }
    }
}

