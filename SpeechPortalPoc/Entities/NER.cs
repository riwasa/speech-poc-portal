using System.Text.Json.Serialization;

public class NER
{
    [JsonPropertyName("Companies")]
    public string[]? Companies { get; set; }

    [JsonPropertyName("People")]
    public string[]? People { get; set; }
}
