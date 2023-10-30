using Objects.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects.Dtos
{
    public  class GetAllBusLocationsRequestDto: ApiRequestBase
    {
        public DateTime Date { get; set; }
    }
}
