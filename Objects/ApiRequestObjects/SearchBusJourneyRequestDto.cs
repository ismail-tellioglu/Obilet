using Objects.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Objects.ApiRequestObjects
{
    public class SearchBusJourneyRequestDto : ApiRequestBase
    {
        public int OriginId { get; set; }
        public int DestinationId { get; set; }
        public DateTime DepartureTime { get; set; }
    }
    public class JourneyRequestData
    {
        [JsonPropertyName("origin-id")]
        public int Origin { get; set; }
        [JsonPropertyName("destination-id")]
        public int DestinationId { get; set; }
        [JsonPropertyName("departure-date")]
        public string DepartureTime { get; set; }
    }
}
