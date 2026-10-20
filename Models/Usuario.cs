using System.Text.Json.Serialization;

namespace BlazorUsersJsonPlaceHolderApi.Models;

public class Usuario
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Nome { get; set; }

    [JsonPropertyName("address")]
    public Endereco? Endereco { get; set; }

    [JsonPropertyName("company")]
    public Empresa? Empresa { get; set; }
}
