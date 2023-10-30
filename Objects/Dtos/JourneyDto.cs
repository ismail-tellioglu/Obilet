using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Objects.Dtos
{
    public class JourneyDto
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public DateTime DepartureTimeDt { get; set; }
        public float Price { get; set; }  
        public string Currency { get; set;}
    }

    public class JourneyResponseRootObject
    {
        public string status { get; set; }
        public Datum[] data { get; set; }
        public object message { get; set; }
        public object usermessage { get; set; }
        public object apirequestid { get; set; }
        public string controller { get; set; }
        public object clientrequestid { get; set; }
        public object webcorrelationid { get; set; }
        public string correlationid { get; set; }
        public object parameters { get; set; }
    }

    public class Datum
    {
        public int id { get; set; }
        public int partnerid { get; set; }
        public string partnername { get; set; }
        public int routeid { get; set; }
        public string bustype { get; set; }
        public string bustypename { get; set; }
        public int totalseats { get; set; }
        public int availableseats { get; set; }
        public Journey journey { get; set; }
        public Feature[] features { get; set; }
        public string originlocation { get; set; }
        public string destinationlocation { get; set; }
        public bool isactive { get; set; }
        public int originlocationid { get; set; }
        public int destinationlocationid { get; set; }
        public bool ispromoted { get; set; }
        public int cancellationoffset { get; set; }
        public bool hasbusshuttle { get; set; }
        public bool disablesaleswithoutgovid { get; set; }
        public string displayoffset { get; set; }
        public object partnerrating { get; set; }
        public bool hasdynamicpricing { get; set; }
        public bool disablesaleswithouthescode { get; set; }
        public bool disablesingleseatselection { get; set; }
        public int changeoffset { get; set; }
        public int rank { get; set; }
        public bool displaycouponcodeinput { get; set; }
        public bool disablesaleswithoutdateofbirth { get; set; }
        public int? openoffset { get; set; }
        public object displaybuffer { get; set; }
        public bool allowsalesforeignpassenger { get; set; }
        public bool hasseatselection { get; set; }
        public object[] brandedfares { get; set; }
        public bool hasgenderselection { get; set; }
        public bool hasdynamiccancellation { get; set; }
        public object partnertermsandconditions { get; set; }
        public string partneravailablealphabets { get; set; }
        public int providerid { get; set; }
        public string partnercode { get; set; }
        public bool enablefulljourneydisplay { get; set; }
        public string providername { get; set; }
        public bool enableallstopsdisplay { get; set; }
        public bool isdestinationdomestic { get; set; }
        public object minlengovid { get; set; }
        public object maxlengovid { get; set; }
        public bool requireforeigngovid { get; set; }
        public bool iscancellationinfotext { get; set; }
        public object cancellationoffsetinfotext { get; set; }
        public bool istimezonenotequal { get; set; }
    }

    public class Journey
    {
        public string kind { get; set; }
        public string code { get; set; }
        public Stop[] stops { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public DateTime departure { get; set; }
        public DateTime arrival { get; set; }
        public string currency { get; set; }
        public string duration { get; set; }
        [JsonPropertyName("original-price")]
        public float originalprice { get; set; }
        public float internetprice { get; set; }
        public float? providerinternetprice { get; set; }
        public object booking { get; set; }
        public string busname { get; set; }
        public Policy policy { get; set; }
        public string[] features { get; set; }
        public string description { get; set; }
        public object available { get; set; }
        public object partnerprovidercode { get; set; }
        public object peronno { get; set; }
        public object partnerproviderid { get; set; }
        public bool issegmented { get; set; }
        public object partnername { get; set; }
        public object providercode { get; set; }
    }

    public class Policy
    {
        public object maxseats { get; set; }
        public int? maxsingle { get; set; }
        public int? maxsinglemales { get; set; }
        public int maxsinglefemales { get; set; }
        public bool mixedgenders { get; set; }
        public bool govid { get; set; }
        public bool lht { get; set; }
    }

    public class Stop
    {
        public int id { get; set; }
        public int? kolayCarLocationId { get; set; }
        public string name { get; set; }
        public string station { get; set; }
        public DateTime? time { get; set; }
        public bool isorigin { get; set; }
        public bool isdestination { get; set; }
    }

    public class Feature
    {
        public int id { get; set; }
        public int? priority { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool ispromoted { get; set; }
        public string backcolor { get; set; }
        public string forecolor { get; set; }
    }

}
