using System;
using ProcessMeme.Interfaces.Infrastructure;

namespace ProcessMeme.Managers.Infrastructure
{
	public class SearchTextProvider : ISearchTextProvider
	{
        public string GetSearchText(string message) => $"мем про {message}";
    }
}

