using Diary_Client.Models;
using Diary_Server.Dtos.Diarys;
using Diary_Server.Services;
using System.Text;
using System.Text.Json;

namespace Diary_Client.Services
{
    public interface IApiDiaryService
    {
        Task<IEnumerable<DiaryDto>> GetUserDiarys(long userId);
        Task CreateEntry(CreateDiaryDto entry);
        Task UpdateEntry(UpdateDiaryDto entry);
        Task<DiaryDto> GetDiaryById(long id);
        Task DeleteEntry(long id);
    }
    public class ApiDiaryService : IApiDiaryService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiDiaryService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient("DiaryClient");
            _httpContextAccessor = httpContextAccessor;
        }

        private void AddAuthorizationHeader()
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");
            
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<IEnumerable<DiaryDto>> GetUserDiarys(long userId)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync($"/diary/user/{userId}");

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

        public async Task CreateEntry(CreateDiaryDto entry)
        {
            AddAuthorizationHeader();
            var entryJson = JsonSerializer.Serialize(entry);
            var content = new StringContent(entryJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/diary", content);

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateEntry(UpdateDiaryDto entry)
        {
            AddAuthorizationHeader();
            var entryJson = JsonSerializer.Serialize(entry);
            var content = new StringContent(entryJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/diary/{entry.Id}", content);

            response.EnsureSuccessStatusCode();
        }

        public async Task<DiaryDto> GetDiaryById(long id)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync($"/diary/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                return JsonSerializer.Deserialize<DiaryDto>(responseData, options);
            }

            return null;
        }

        public async Task DeleteEntry(long id)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"/diary/{id}");

            response.EnsureSuccessStatusCode();
        }
    }
}
