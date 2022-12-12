using System;
using System.Text.Json.Serialization;

namespace ProcessMeme.Models.Search.Google
{
    public record Item(
        [property: JsonPropertyName("kind")] string Kind,
        [property: JsonPropertyName("title")] string Title,
        [property: JsonPropertyName("htmlTitle")] string HtmlTitle,
        [property: JsonPropertyName("link")] string Link,
        [property: JsonPropertyName("displayLink")] string DisplayLink,
        [property: JsonPropertyName("snippet")] string Snippet,
        [property: JsonPropertyName("htmlSnippet")] string HtmlSnippet,
        [property: JsonPropertyName("mime")] string Mime,
        [property: JsonPropertyName("fileFormat")] string FileFormat,
        [property: JsonPropertyName("image")] Image Image);
}

