using Infrastructure.Interfaces;
using Objects.Dtos;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Infrastructure.Concretes
{
    public class ApiCaller : IApiCaller
    {
        public async Task<GenericApiResponseDto> ProcessApiRequest(string endPonint, string token, string method, Object data)
        {
            GenericApiResponseDto response = new GenericApiResponseDto();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
                if (method == "GET")
                {
                    var result = await client.GetAsync(endPonint);
                    response.statusCode = (int)result.StatusCode;
                    response.response = await result.Content.ReadAsStringAsync();
                }
                else if (method == "POST")
                {
                    var myContent = JsonSerializer.Serialize(data);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    client.BaseAddress = new Uri(endPonint);
                    var result = await client.PostAsync("", byteContent);
                    response.statusCode = (int)result.StatusCode;
                    response.response = await result.Content.ReadAsStringAsync();
                }
            }
            return response;
        }
    }
}
