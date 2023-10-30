using AutoMapper;
using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Obilet.Models;
using Objects.Dtos;
using System.Diagnostics;
using System.Globalization;
using UAParser;

namespace Obilet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBusJourneyService _busJourneyService;
        private readonly IBusinessHelper _businessHelper;
        private IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, IBusJourneyService busJourneyService, IBusinessHelper helper, IMapper mapper)
        {
            _logger = logger;
            this._mapper = mapper;
            this._busJourneyService = busJourneyService;
            this._businessHelper = helper;
          
        }

        private async Task SetSessionId()
        {
            var userAgent = HttpContext.Request.Headers["User-Agent"];
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgent);

            var response=  await _businessHelper.GetSession(new SessionRequestDto
            {
                Browser = new Browser
                {
                    Name = c.UA.Family,
                    Version = c.UA.Major
                },
                Connection = new Connection
                {
                    IP = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Port = Request.HttpContext.Connection.RemotePort
                }
            });
            HttpContext.Session.SetString("session-id", response.SessionId);
            HttpContext.Session.SetString("device-id", response.DeviceId);
        }
        public async Task<IActionResult> Index(string errorMessage)
        {
            //set session-id if it was not saved before
            if (HttpContext.Session.GetString("session-id") == null)
            {
                await SetSessionId();
            }
            SearchBusModel md = new SearchBusModel();
           
            var locations = await _busJourneyService.GetLocations(HttpContext.Session.GetString("session-id"), HttpContext.Session.GetString("device-id"));


            md.DestinationList = locations.Select(x =>
                                   new SelectListItem()
                                   {
                                       Value = x.LocationId.ToString(),
                                       Text = x.Location
                                   });
            md.OriginList = locations.Select(x =>
                                   new SelectListItem()
                                   {
                                       Value = x.LocationId.ToString(),
                                       Text = x.Location,
                                   });
            if (Request.Cookies["last-chosen-origin"] != null)
                md.OriginId = int.Parse(Request.Cookies["last-chosen-origin"]);
            else
                md.OriginId = int.Parse(md.OriginList.First().Value);
            if (Request.Cookies["last-chosen-destination"] != null)
                md.DestinationId = int.Parse(Request.Cookies["last-chosen-destination"]);
            else
                md.DestinationId = int.Parse(md.OriginList.First().Value);
            //if there is a serach date in cookies and if it is not earlier than date of today set that date
            if (Request.Cookies["last-chosen-departuretime"] != null && DateTime.Parse(Request.Cookies["last-chosen-departuretime"]).Date>=DateTime.Now.Date)
            {
                md.DepartureTime = DateTime.Parse(Request.Cookies["last-chosen-departuretime"]);
            }
            else
                md.DepartureTime = System.DateTime.Now.AddDays(1);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.error=errorMessage;
            }
            return View(md);
        }

        [HttpPost]
        public async Task<IActionResult> SearchBus(SearchBusModel criterias)
        {
            HttpContext.Response.Cookies.Append("last-chosen-origin", criterias.OriginId.ToString());
            HttpContext.Response.Cookies.Append("last-chosen-destination", criterias.DestinationId.ToString());
            HttpContext.Response.Cookies.Append("last-chosen-departuretime", criterias.DepartureTime.ToString());

            if (criterias.OriginId == criterias.DestinationId)
                return RedirectToAction("Index", new { errorMessage = "Origin and destination can not be same location" });
            SearchBusJourneyRequestDto request= _mapper.Map<SearchBusJourneyRequestDto>(criterias);
            List< JourneyDto> journeys=   await this._busJourneyService.SearchBusJourney(HttpContext.Session.GetString("session-id"), HttpContext.Session.GetString("device-id"),
                request);
            if (journeys.Count > 0)
            {
                var locations = await _busJourneyService.GetLocations(HttpContext.Session.GetString("session-id"), HttpContext.Session.GetString("device-id"));
                
                ViewBag.journeyInfo = $"{locations.Where(p=>p.LocationId==criterias.OriginId).First().Location}-" +
                    $"{locations.Where(p => p.LocationId == criterias.DestinationId).First().Location}" +
                    $" {criterias.DepartureTime.ToString("dd MMMM", CultureInfo.CreateSpecificCulture("en-ss"))} {criterias.DepartureTime.DayOfWeek.ToString("G")}";
            }
            return View("Journeys", journeys);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}