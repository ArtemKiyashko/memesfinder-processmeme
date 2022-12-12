using System;
using ProcessMeme.Models.Search;

namespace ProcessMeme.Interfaces
{
	public interface ISearchEngine
	{
		public SearchResults GetResults();
	}
}

