using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Objects.Abstracts
{
    public abstract class ApiRequestBase
    {
        [JsonPropertyName("device-session")]
        public DeviceSession DeviceSession { get; set; }
        public string Language { get; set; }

        [JsonPropertyName("data")]
        public Object Data { get; set; }
    }

    public  class DeviceSession
    {
        [JsonPropertyName("session-id")]
        public string SessionId { get; set; }
        [JsonPropertyName("device-id")]
        public string DeviceId { get; set; }

    }
}
