using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Diary_Client.Services;
using Diary_Client.Models;

namespace Diary_Client.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IApiUserService _userService;

        public RegisterModel(IApiUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; }

        public class RegisterInputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string UserName { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var registerUserDto = new RegisterUserDto
                {
                    Email = Input.Email,
                    Password = Input.Password,
                    ConfirmPassword = Input.ConfirmPassword,
                    UserName = Input.UserName
                };

                await _userService.RegisterUser(registerUserDto);
                return RedirectToPage("/Account/Login");
            }

            return Page();
        }
    }
}
