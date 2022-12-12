using System;
using System.Text.Json.Serialization;

namespace ProcessMeme.Models.Search
{
	public record SearchResults(
		IEnumerable<SearchResultItem> Items);
}

