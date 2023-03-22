using gamerszone.Shared;

namespace gamerszone.Utilities
{
    public class AskGPTTool
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public AskGPTTool(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.openai.com/v1/");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

        }
        public async Task<string> AskMe(string question, string model)
        {
            var requestBody = new
            {
                prompt = question,
                model,
                max_tokens = 150,
                temperature = 0.5
            };
            var response = await _httpClient.PostAsJsonAsync("completions", requestBody);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
