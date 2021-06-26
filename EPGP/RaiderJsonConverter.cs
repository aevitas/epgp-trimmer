using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EPGP
{
    public class RaiderJsonConverter : JsonConverter<Raider>
    {
        public override Raider Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartArray)
                reader.Read();
            
            var r = new Raider();

            // There has to be a better way to handle this, but I can't think of one off the top of my head..
            r.Name = reader.GetString();
            reader.Read();
            r.Class = reader.GetString();
            reader.Read();
            r.Rank = reader.GetString();
            reader.Read();
            r.Ep = reader.GetInt32();
            reader.Read();
            r.Gp = reader.GetInt32();
            reader.Read();
            r.Ratio = reader.GetDouble();

            // Read past the end array token
            reader.Read();

            return r;
        }

        public override void Write(Utf8JsonWriter writer, Raider value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            writer.WriteStringValue(value.Name);
            writer.WriteStringValue(value.Class);
            writer.WriteStringValue(value.Rank);
            writer.WriteNumberValue(value.Ep);
            writer.WriteNumberValue(value.Gp);
            writer.WriteNumberValue(value.Ratio);
            writer.WriteEndArray();
        }
    }
}
