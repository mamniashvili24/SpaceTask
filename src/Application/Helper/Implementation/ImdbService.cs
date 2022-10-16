using Domain.Errors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using Microsoft.Extensions.Options;
using Application.Helper.Abstraction;

namespace Application.Helper.Implementation;

public class ImdbService : IImdbService
{
    private readonly IHttpClientFactory _httpClient;
    private readonly IOptions<ImdbClientKey> _imdbClientKey;

    public ImdbService(IHttpClientFactory httpClient, IOptions<ImdbClientKey> imdbClientKey)
    {
        _httpClient = httpClient;
        _imdbClientKey = imdbClientKey;
    }
    public async Task<T> GetAsync<T>(string methodName, string queryParameter)
    {
        var languageCode = CultureInfo.CurrentCulture.Name;

        var client = _httpClient.CreateClient(HttpClientStrings.IMDBClient);
        var path = string.Format("{0}/API/{1}/{2}/{3}", languageCode, methodName, _imdbClientKey.Value.Key, queryParameter);

        var responseMessage = await client.GetAsync(path);

        if (!responseMessage.IsSuccessStatusCode)
            throw new Exception(ErrorMessages.ApiCallError);

        var responseString = await responseMessage.Content.ReadAsStringAsync();
        var jToken = JObject.Parse(responseString);

        if (jToken == null)
            throw new Exception(ErrorMessages.ApiCallError);

        if (jToken.TryGetValue("errorMessage", StringComparison.OrdinalIgnoreCase, out JToken errorMessage))
        {
            if (!string.IsNullOrEmpty(errorMessage.ToString()))
                throw new Exception(errorMessage.ToString());
        }

        var json = jToken.TryGetValue("results", StringComparison.OrdinalIgnoreCase, out JToken result) ? result.ToString() : jToken.ToString();

        var response = JsonConvert.DeserializeObject<T>(json);

        return response;
    }
}