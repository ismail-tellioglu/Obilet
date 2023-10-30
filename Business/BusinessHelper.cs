using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Objects.ApiRequestObjects;
using Objects.Dtos;
using System.Dynamic;
using System.Text.Json;

namespace Business
{
    public class BusinessHelper : IBusinessHelper
    {
        private readonly IApiCaller _apiCaller;
        private readonly IConfiguration _configuration;
        private readonly string _apiUrl;
        private readonly string _apiToken;
        public BusinessHelper(IApiCaller apicaller, IConfiguration configuration)
        {
            this._apiCaller = apicaller;
            _configuration = configuration;
            _apiUrl = _configuration.GetRequiredSection("ApiInfo")["ApiUrl"] ?? "";
            _apiToken = _configuration.GetRequiredSection("ApiInfo")["ApiToken"] ?? "";

        }

        /// <summary>
        /// Gets session-id from obilet api
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<SessionDto> GetSession(SessionRequestDto request)
        {
            request.Type = 1;
            GenericApiResponseDto response = await _apiCaller.ProcessApiRequest(_apiUrl + "/client/getsession",
                _apiToken, "POST", request);
            if (response.statusCode == 200)
            {
                {
                    dynamic returnedObj = JsonSerializer.Deserialize<ExpandoObject>(response.response);
                    string returnedData = ((IDictionary<String, Object>)returnedObj).Where(P => P.Key == "data").First().Value.ToString();
                   
                     return JsonSerializer.Deserialize<SessionDto>(returnedData);
                    //return new SessionDto { SessionId = ((IDictionary<String, Object>)returnedData).Where(P => P.Key == "session-id").First().Value.ToString(),
                    //DeviceId= ((IDictionary<String, Object>)returnedData).Where(P => P.Key == "device-id").First().Value.ToString()};
                }
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
