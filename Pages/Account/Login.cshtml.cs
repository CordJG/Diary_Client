using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using Diary_Client.Models;

namespace Diary_Client.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient("DiaryClient");
                var loginData = JsonSerializer.Serialize(new { Email = Input.Email, Password = Input.Password });
                var content = new StringContent(loginData, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var jsonDoc = JsonDocument.Parse(responseData);
                    var token = jsonDoc.RootElement.GetProperty("token").GetString();
                    var user = jsonDoc.RootElement.GetProperty("user");
                    var userId = user.GetProperty("id").GetInt64();
                    var userEmail = user.GetProperty("email").GetString();
                    var userName = user.GetProperty("userName").GetString();
                   

                    if (!string.IsNullOrEmpty(token))
                    {
                        // 로컬 스토리지에 토큰 저장
                        HttpContext.Session.SetString("token", token);
                        HttpContext.Session.SetString("UserId", userId.ToString());
                        HttpContext.Session.SetString("UserEmail", userEmail);
                        HttpContext.Session.SetString("UserName", userName);
                        return RedirectToPage("/Diary/Index");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid email or password");
            }
            return Page();
        }
    }
}
