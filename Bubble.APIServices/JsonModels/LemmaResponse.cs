using System.Text.Json.Serialization;

namespace Bubble.Service.JsonModels;

public class Lemma
{
    [JsonPropertyName("start")]
    public int Start { get; set; }

    [JsonPropertyName("end")]
    public int End { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class Annotations
{
    [JsonPropertyName("lemma")]
    public List<Lemma> Lemma { get; set; }
}

public class Root
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("annotations")]
    public Annotations Annotations { get; set; }
}

