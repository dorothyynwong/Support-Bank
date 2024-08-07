using Newtonsoft.Json;

namespace SupportBank;

public class JsonInterface {
    // [JsonProperty("Date")]
    public string Date { get; set; }

    // [JsonProperty("FromAccount")]
    public string FromAccount { get; set; }

    // [JsonProperty("ToAccount")]
    public string ToAccount { get; set; }

    // [JsonProperty("Narrative")]
    public string Narrative { get; set; }

    // [JsonProperty("Amount")]
    public string Amount { get; set; }
}