using System;
namespace ProcessMeme.Interfaces.SearchEngine
{
	public interface IGoogleSearchEngineManager
	{
		public ValueTask SearchMemesAsync(string keyword);
		public string GetNextRandomMemeUrl();
	}
}

