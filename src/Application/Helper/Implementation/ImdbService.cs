using Domain.Errors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CommonTypes.Abstractions;
using CommonTypes.Implementations;
using Application.Helper.Abstraction;

namespace Application.Helper.Implementation;

public class ImdbService : IImdbService
{
    private readonly IHttpClientFactory _httpClient;
    private readonly IImdbClientKey _imdbClientKey;

    public ImdbService(IHttpClientFactory httpClient, IImdbClientKey imdbClientKey)
    {
        _httpClient = httpClient;
        _imdbClientKey = imdbClientKey;
    }
    public async Task<IDataResponse<T>> GetAsync<T>(string methodName, string lanugageCode, string queryParameter)
    {
        try
        {
            var client = _httpClient.CreateClient(HttpClientStrings.IMDBClient);
            var path = string.Format("{0}/API/{1}/{2}/{3}", lanugageCode, methodName, _imdbClientKey.Key, queryParameter);

            var responseMessage = await client.GetAsync(path);

            if (!responseMessage.IsSuccessStatusCode)
                return new DataResponse<T>(ErrorMessages.ApiCallError);

            var responseString = await responseMessage.Content.ReadAsStringAsync();
            var jToken = JObject.Parse(responseString);

            if (jToken == null)
                return new DataResponse<T>(ErrorMessages.ApiCallError);
            
            if (jToken.TryGetValue("errorMessage", StringComparison.OrdinalIgnoreCase, out JToken errorMessage))
            {
                if (!string.IsNullOrEmpty(errorMessage.ToString()))
                    return new DataResponse<T>(errorMessage.ToString());
            }

            var json = jToken.TryGetValue("results", StringComparison.OrdinalIgnoreCase, out JToken result) ? result.ToString() : jToken.ToString();

            var response = JsonConvert.DeserializeObject<T>(json);

            return new DataResponse<T>(response);
        }
        catch (Exception ex)
        {
            return new DataResponse<T>(ex);
        }
    }
}