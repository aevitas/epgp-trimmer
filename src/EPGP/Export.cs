using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EPGP
{
    public record Export
    {
        [JsonPropertyName("roster")]
        public List<Raider> Roster { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }
}
