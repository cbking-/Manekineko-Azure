namespace AzureManekineko.Data;

using Newtonsoft.Json;

public class FortuneDoll
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty(PropertyName = "found")]
    public bool Found { get; set; }

    [JsonProperty(PropertyName = "gold")]
    public bool Gold { get; set; }
}
