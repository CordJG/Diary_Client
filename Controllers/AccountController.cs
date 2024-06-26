using Diary_Client.Models;
using Diary_Server.Dtos.Users;
using Diary_Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Diary_Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.Authenticate(new LoginUserDto
                {
                    Username = model.Username,
                    Password = model.Password
                });

                if (user != null)
                {
                    // 로그인 성공
                    // 세션이나 쿠키를 설정하여 사용자를 인증된 상태로 만듭니다.
                    return RedirectToAction("Index", "Diary");
                }

                ModelState.AddModelError("", "Invalid username or password");
            }
            return View(model);
        }
    }
}
