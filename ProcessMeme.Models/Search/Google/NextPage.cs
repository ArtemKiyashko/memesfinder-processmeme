using System;
using System.Text.Json.Serialization;

namespace ProcessMeme.Models.Search.Google
{
    public record NextPage(
        [property: JsonPropertyName("title")] string Title,
        [property: JsonPropertyName("totalResults")] string TotalResults,
        [property: JsonPropertyName("searchTerms")] string SearchTerms,
        [property: JsonPropertyName("count")] int Count,
        [property: JsonPropertyName("startIndex")] int StartIndex,
        [property: JsonPropertyName("inputEncoding")] string InputEncoding,
        [property: JsonPropertyName("outputEncoding")] string OutputEncoding,
        [property: JsonPropertyName("safe")] string Safe,
        [property: JsonPropertyName("cx")] string Cx,
        [property: JsonPropertyName("searchType")] string SearchType);
}

