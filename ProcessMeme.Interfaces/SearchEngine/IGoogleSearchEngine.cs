using System;
using Google.Apis.Customsearch.v1.Data;

namespace ProcessMeme.Interfaces.SearchEngine
{
	public interface IGoogleSearchEngine
	{
		public ValueTask<IList<Result>> GetMemesAsync(string keyword);
	}
}

