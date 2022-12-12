using System;
using System.Text.Json.Serialization;

namespace ProcessMeme.Models.Search.Google
{
    public record Url(
        [property: JsonPropertyName("type")] string Type,
        [property: JsonPropertyName("template")] string Template);

}

