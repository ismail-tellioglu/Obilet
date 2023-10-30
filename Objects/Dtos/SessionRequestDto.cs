using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Objects.Dtos
{
    public  class SessionRequestDto
    {
        public int Type { get; set; }
        public Browser Browser { get; set; }
        public Connection Connection { get; set; }
    }
    public class Connection
    {
        [JsonPropertyName("ip-address")]
        public string IP { get; set; }
        public int Port { get; set; }
    }
    public class Browser
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }

}
