using System;
using System.Text.Json.Serialization;

namespace ProcessMeme.Models.Search.Google
{
    public record Image(
        [property: JsonPropertyName("contextLink")] string ContextLink,
        [property: JsonPropertyName("height")] int Height,
        [property: JsonPropertyName("width")] int Width,
        [property: JsonPropertyName("byteSize")] int ByteSize,
        [property: JsonPropertyName("thumbnailLink")] string ThumbnailLink,
        [property: JsonPropertyName("thumbnailHeight")] int ThumbnailHeight,
        [property: JsonPropertyName("thumbnailWidth")] int ThumbnailWidth);
}

