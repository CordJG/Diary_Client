using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Diary_Client.Models;
using Microsoft.AspNetCore.Http;

namespace Diary_Client.Services
{
    public interface IApiUserService
    {
        Task RegisterUser(RegisterUserDto registerUserDto);
        Task Logout();
    }
    public class ApiUserService : IApiUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiUserService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient("DiaryClient");
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task RegisterUser(RegisterUserDto registerUserDto)
        {
            var registerUserJson = JsonSerializer.Serialize(registerUserDto);
            var content = new StringContent(registerUserJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/auth/register", content);

            response.EnsureSuccessStatusCode();
        }

        public async Task Logout()
        {
            // 클라이언트 측에서 세션 종료
            _httpContextAccessor.HttpContext.Session.Clear();

        }
    }
}
