using AutoMapper;
using Objects.Dtos;

namespace Business
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<SearchBusModel, SearchBusJourneyRequestDto>();
        }
    }
}
