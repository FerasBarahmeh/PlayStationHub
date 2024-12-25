using Microsoft.Extensions.Configuration;
using PlayStationHub.Business.Interfaces.Services;
using System.Text;
using System.Text.Json;

namespace PlayStationHub.Business.Services;

public class GeminiService :  IGeminiService
{
    private readonly HttpClient _httpClient;
    private readonly string _BaseUrl;
    private readonly string _ApiKey;
    private readonly string _RequestURL;
    private readonly string _Prompt;
    private readonly IClubFeedbackService _ClubFeedbackService;

    public GeminiService(HttpClient httpClientFactory, IConfiguration configuration, IClubFeedbackService clubFeedbackService)
    {
        _BaseUrl = configuration["GoogleGemini:url"];
        _ApiKey = configuration["GoogleGemini:API_KEY"];
        _httpClient = httpClientFactory;
        _RequestURL = $"{_BaseUrl}?key={_ApiKey}";
        _ClubFeedbackService = clubFeedbackService;
    }

    private  object GetFeedbacks(int ClubID)
    {
        
        object request = new
        {
            contents = new[]
            {
              new {
                parts = new[] {
                    new { text = "What is C# in many words" }
                }
              }
            }
        };

        return request;
    }

    public async Task<string> GenerateResponseAsync(int ClubID)
    {
        object _Request = GetFeedbacks(ClubID);
        var jsonContent = JsonSerializer.Serialize(_Request);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_RequestURL, httpContent);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Request failed with status {response.StatusCode}: {error}");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var responseData = JsonSerializer.Deserialize<JsonElement>(responseContent);
        return responseData.ToString();
    }
}
