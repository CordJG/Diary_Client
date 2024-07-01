using System.Threading.Tasks;
using Diary_Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Diary_Client.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly IApiUserService _userService;

        public LogoutModel(IApiUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _userService.Logout();
            return RedirectToPage("/Account/Login");
        }
    }
}
