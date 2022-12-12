using System;
using System.Text.Json.Serialization;

namespace ProcessMeme.Models.Search.Google
{
    public record SearchInformation(
        [property: JsonPropertyName("searchTime")] double SearchTime,
        [property: JsonPropertyName("formattedSearchTime")] string FormattedSearchTime,
        [property: JsonPropertyName("totalResults")] string TotalResults,
        [property: JsonPropertyName("formattedTotalResults")] string FormattedTotalResults);
}

