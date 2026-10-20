using System.Text.Json.Serialization;

namespace BlazorUsersJsonPlaceHolderApi.Models;

public class Empresa
{
    [JsonPropertyName("name")]
    public string? Nome { get; set; }
}
