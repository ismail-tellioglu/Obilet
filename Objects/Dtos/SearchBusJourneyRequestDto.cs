using Objects.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects.Dtos
{
    public class SearchBusJourneyRequestDto : ApiRequestBase
    {
        public int OriginId { get; set; }
        public int DestinationId { get; set; }
        public DateTime DepartureTime { get; set; }
    }
}
