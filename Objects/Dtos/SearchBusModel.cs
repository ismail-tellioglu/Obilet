using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects.Dtos
{
    public  class SearchBusModel
    {
        public IEnumerable<SelectListItem> OriginList { get; set; }
        public IEnumerable<SelectListItem> DestinationList { get; set; }  
        public DateTime DepartureTime { get; set; }
        [Required(ErrorMessage = "Lütfen bir seçim yapınız")]
        public int OriginId { get; set; }
        [Required(ErrorMessage = "Lütfen bir seçim yapınız")]
        public int DestinationId { get; set; }
        public string DestinationName { get; set; } 
        public string OriginName { get; set; }
    }
}
