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

    private async Task<object> GetFeedbacks(int ClubID)
    {
        string prompt = await  _ClubFeedbackService.GeneratePrompt(ClubID);
        object request = new
        {
            contents = new[]
            {
              new {
                parts = new[] {
                    new { text = prompt }
                }
              }
            }
        };

        return request;
    }

    public async Task<string> GenerateResponseAsync(int ClubID)
    {
        var _Request =  await GetFeedbacks(ClubID);
        var jsonContent = JsonSerializer.Serialize(_Request);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_RequestURL, httpContent);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Request failed with status {response.StatusCode}: {error}");
        }

        return await response.Content.ReadAsStringAsync();
    }
}
