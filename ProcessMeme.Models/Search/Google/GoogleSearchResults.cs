using System;
using System.Text.Json.Serialization;

namespace ProcessMeme.Models.Search.Google
{
	public record GoogleSearchResults(
        [property: JsonPropertyName("kind")] string Kind,
        [property: JsonPropertyName("url")] Url Url,
        [property: JsonPropertyName("queries")] Queries Queries,
        [property: JsonPropertyName("context")] Context Context,
        [property: JsonPropertyName("searchInformation")] SearchInformation SearchInformation,
        [property: JsonPropertyName("items")] List<Item> Items);
}

