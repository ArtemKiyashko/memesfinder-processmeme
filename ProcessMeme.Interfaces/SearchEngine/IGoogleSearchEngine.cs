using System;
using Google.Apis.Customsearch.v1.Data;

namespace ProcessMeme.Interfaces.SearchEngine
{
	public interface IGoogleSearchEngine
	{
		public IList<Result> GetMemes(string query);
	}
}

