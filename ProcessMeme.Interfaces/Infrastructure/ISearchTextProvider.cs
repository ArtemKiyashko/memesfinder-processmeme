using System;
namespace ProcessMeme.Interfaces.Infrastructure
{
	public interface ISearchTextProvider
	{
		public string GetSearchText(string message);
	}
}

