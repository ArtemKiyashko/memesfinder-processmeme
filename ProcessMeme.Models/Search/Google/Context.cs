using System;
using System.Text.Json.Serialization;

namespace ProcessMeme.Models.Search.Google
{
    public record Context(
        [property: JsonPropertyName("title")] string Title);
}

