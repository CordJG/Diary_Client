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
                var client = _httpClientFactory.CreateClient();
                var loginData = JsonSerializer.Serialize(new { Username = Input.Username, Password = Input.Password });
                var content = new StringContent(loginData, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://localhost:7215/User/login", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Diary/Index");
                    /*  var responseData = await response.Content.ReadAsStringAsync();
                      var token = JsonDocument.Parse(responseData).RootElement.GetProperty("token").GetString();*/

                    /* if (!string.IsNullOrEmpty(token))
                     {*/

                    /*}*/
                }

                ModelState.AddModelError(string.Empty, "Invalid username or password");
            }
            return Page();
        }
    }
}
