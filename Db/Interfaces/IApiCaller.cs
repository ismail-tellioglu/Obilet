using Objects.Dtos;

namespace Infrastructure.Interfaces
{
    public interface IApiCaller
    {
        public Task<GenericApiResponseDto> ProcessApiRequest(string endPonint, string token, string method, Object data);
    }
}
