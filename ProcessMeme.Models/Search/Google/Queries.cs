using System;
using System.Text.Json.Serialization;

namespace ProcessMeme.Models.Search.Google
{
    public record Queries(
        [property: JsonPropertyName("request")] List<Request> Request,
        [property: JsonPropertyName("nextPage")] List<NextPage> NextPage);
}

