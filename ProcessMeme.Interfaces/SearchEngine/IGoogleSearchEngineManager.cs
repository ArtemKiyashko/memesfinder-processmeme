using System;
namespace ProcessMeme.Interfaces.SearchEngine
{
	public interface IGoogleSearchEngineManager
	{
		public ValueTask<string?> GetMemeLinkAsync(string query);
	}
}

