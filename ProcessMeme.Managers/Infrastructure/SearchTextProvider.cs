using ProcessMeme.Interfaces.Infrastructure;

namespace ProcessMeme.Managers.Infrastructure
{
    public class SearchTextProvider : ISearchTextProvider
    {
        public string GetSearchText(string message) => $"site:twitter.com {message} мем";
    }
}

