using System;
using Google.Apis.Customsearch.v1.Data;

namespace ProcessMeme.Managers.SearchEngine
{
	public static class SearchResultExtensions
	{
		public static Result? GetRandomResult(this IList<Result> results)
			=> results is null ? default : results[Random.Shared.Next(0, results.Count - 1)];
	}
}

