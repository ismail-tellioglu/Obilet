using Objects.ApiRequestObjects;
using Objects.Dtos;

namespace Business
{
    public interface IBusJourneyService
    {
        Task<List<BusLocationDto>> GetLocations(string sessionId, string deviceId);
        Task<List<JourneyDto>> SearchBusJourney(string sessionId, string deviceId, SearchBusJourneyRequestDto request);
    }
}
