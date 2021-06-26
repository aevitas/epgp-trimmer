using System.Text.Json.Serialization;

namespace EPGP
{
    [JsonConverter(typeof(RaiderJsonConverter))]
    public record Raider
    {
        public string Name { get; set; }

        public string Class { get; set; }

        public string Rank { get; set; }

        public int Ep { get; set; }

        public int Gp { get; set; }

        public double Ratio { get; set; }
    }
}
