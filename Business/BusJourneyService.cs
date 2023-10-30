using AutoMapper;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Objects;
using Objects.Abstracts;
using Objects.Dtos;
using System.Dynamic;
using System.Text.Json;

namespace Business
{
    public class BusJourneyService : IBusJourneyService
    {
        private IApiCaller _apiCaller;
        
        private readonly IConfiguration _configuration;
        private readonly string _apiUrl;
        private readonly string _apiToken;
        private readonly IMemoryCache _cache;
        private const string locationListCacheKey = "locationList";

        public BusJourneyService(IApiCaller apicaller, IConfiguration configuration, IMemoryCache cache)
        {
            this._apiCaller = apicaller;
            _cache = cache;
            _configuration = configuration;
            _apiUrl = _configuration.GetRequiredSection("ApiInfo")["ApiUrl"] ?? "";
            _apiToken = _configuration.GetRequiredSection("ApiInfo")["ApiToken"] ?? "";
        }

        public async Task<List<JourneyDto>> SearchBusJourney(string sessionId, string deviceId,SearchBusJourneyRequestDto request)
        {
            List<JourneyDto> journeys= new List<JourneyDto>();
            request.DeviceSession = new DeviceSession
            {
                DeviceId = deviceId,
                SessionId = sessionId
            };
            request.Language = "tr-TR";
            request.Data = new JourneyRequestData
            {
                DepartureTime = request.DepartureTime.ToString("yyyy-MM-dd"),
                Origin=request.OriginId,
                DestinationId=request.DestinationId
            };

            GenericApiResponseDto response = await _apiCaller.ProcessApiRequest(_apiUrl + "/journey/getbusjourneys", _apiToken, "POST", request);
            if (response.statusCode == 200)
            {
               
                    var journeyResponse = JsonSerializer.Deserialize<JourneyResponseRootObject>(response.response);
                foreach (var item in journeyResponse.data)
                {

                        journeys.Add(new JourneyDto
                        {
                            ArrivalTime = item.journey.arrival.ToString("HH:mm"),
                            DepartureTime= item.journey.departure.ToString("HH:mm"),
                            Origin=item.journey.origin,
                            Destination=item.journey.destination,
                            DepartureTimeDt= item.journey.departure,
                            Price=item.journey.originalprice,
                            Currency=item.journey.currency,
                        });

                }
            }
            else
            {
                throw new Exception();
            }
            
            return journeys.OrderBy(p=>p.DepartureTimeDt).ToList();
        }

        public async Task<List<BusLocationDto>> GetLocations(string sessionId, string deviceId)
        {
            if (!_cache.TryGetValue(locationListCacheKey, out List<BusLocationDto> locations))
            {
                GetAllBusLocationsRequestDto request = new GetAllBusLocationsRequestDto
                {
                    Date = DateTime.Now,
                    Language = "tr-TR",
                    DeviceSession = new DeviceSession
                    {
                        DeviceId = deviceId,
                        SessionId = sessionId
                    }
                };

                GenericApiResponseDto response = await _apiCaller.ProcessApiRequest(_apiUrl + "/location/getbuslocations", _apiToken, "POST", request);
                if (response.statusCode == 200)
                {
                    {
                        dynamic returnedObj = JsonSerializer.Deserialize<ExpandoObject>(response.response);
                        locations =
                             JsonSerializer.Deserialize<List<BusLocationDto>>(((IDictionary<String, Object>)returnedObj).Where(P => P.Key == "data").First().Value.ToString());

                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(600))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(36000))
                        .SetPriority(CacheItemPriority.Normal);
                        _cache.Set(locationListCacheKey, locations, cacheEntryOptions);
                        
                        return locations.OrderBy(p=>p.Rank).ToList();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            else
                return locations;
        }

    }
}