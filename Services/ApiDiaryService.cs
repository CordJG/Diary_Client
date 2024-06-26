using Diary_Server.Dtos.Diarys;
using Diary_Server.Services;
using System.Text.Json;

namespace Diary_Client.Services
{
    public interface IApiDiaryService
    {
        Task<IEnumerable<DiaryDto>> GetUserDiarys(long userId);
    }
    public class ApiDiaryService : IApiDiaryService
    {
        private readonly HttpClient _httpClient;

        public ApiDiaryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<DiaryDto>> GetUserDiarys(long userId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7215/diary/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                var diaries = JsonSerializer.Deserialize<IEnumerable<DiaryDto>>(responseData, options);
                return diaries;
            }

            return new List<DiaryDto>();
        }
    }
}
