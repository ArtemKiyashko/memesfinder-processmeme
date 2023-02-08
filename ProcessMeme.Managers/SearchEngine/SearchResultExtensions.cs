using System;
using Google.Apis.Customsearch.v1.Data;

namespace ProcessMeme.Managers.SearchEngine
{
	public static class SearchResultExtensions
	{
		public static Result? GetRandomResult(this IList<Result> results)
			=> results is null ? default : results[Random.Shared.Next(0, Math.Min(3, results.Count))];
	}
}

