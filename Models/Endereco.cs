using System.Text.Json.Serialization;

namespace BlazorUsersJsonPlaceHolderApi.Models;

public class Endereco
{
    [JsonPropertyName("city")]
    public string? Cidade { get; set; }
}
