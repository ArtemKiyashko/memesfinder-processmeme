using System;
namespace ProcessMeme.Managers.SearchEngine
{
	public class SearchEngineOptions
	{
		public string ApiKey { get; set; }
		public string EngineId { get; set; }
		public string CountryRestriction { get; set; }
		public string SitesToExclude { get; set; }
	}
}

