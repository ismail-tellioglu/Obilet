using System.Text.Json.Serialization;

namespace Objects.Dtos
{
    public  class BusLocationDto
    {
        [JsonPropertyName("name")]
        public string Location { get; set; }
        [JsonPropertyName("id")]
        public int LocationId { get; set; }
        [JsonPropertyName("rank")]
        public int Rank { get; set; }
    }
}
