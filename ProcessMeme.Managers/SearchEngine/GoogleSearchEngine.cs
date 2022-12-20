using System;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using ProcessMeme.Interfaces.Infrastructure;
using ProcessMeme.Interfaces.SearchEngine;
using static Google.Apis.Customsearch.v1.CseResource;

namespace ProcessMeme.Managers.SearchEngine
{
	public class GoogleSearchEngine : IGoogleSearchEngine
	{
        private readonly ISearchTextProvider _searchTextProvider;
        private readonly ListRequest _listRequest;

        public GoogleSearchEngine(
            ISearchTextProvider searchTextProvider,
            ListRequest listRequest)
		{
            _searchTextProvider = searchTextProvider;
            _listRequest = listRequest;
        }

        public async ValueTask<IList<Result>> GetMemesAsync(string query)
        {
            var searchQuery = _searchTextProvider.GetSearchText(query);
            _listRequest.Q = searchQuery;

            var searchResults = await _listRequest.ExecuteAsync();

            return searchResults.Items;
        }
    }
}

