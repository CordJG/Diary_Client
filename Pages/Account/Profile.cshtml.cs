using Diary_Client.Services;
using Diary_Server.Dtos.Diarys;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Diary_Client.Models;

namespace Diary_Client.Pages.Account
{
    public class ProfileModel : PageModel
    {
        private readonly IApiDiaryService _diaryService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileModel(IApiDiaryService diaryService, IHttpContextAccessor httpContextAccessor)
        {
            _diaryService = diaryService;
            _httpContextAccessor = httpContextAccessor;
        }

        public UserProfileModel UserProfile { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userEmail = _httpContextAccessor.HttpContext.Session.GetString("UserEmail");
            var userName = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            var userIdString = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userIdString))
            {
                return RedirectToPage("/Account/Login");
            }

            if (long.TryParse(userIdString, out var userId))
            {
                var diaries = await _diaryService.GetUserDiarys(userId);
                UserProfile = new UserProfileModel
                {
                    Email = userEmail,
                    Name = userName,
                    DiaryCount = diaries.Count(),
                    DiariesByYearMonth = diaries
                        .GroupBy(d => d.DiaryDate.Year)
                        .ToDictionary(
                            g => g.Key,
                            g => g.GroupBy(d => d.DiaryDate.Month)
                                  .ToDictionary(
                                      mg => mg.Key,
                                      mg => mg.ToList()
                                  )
                        )
                };

                return Page();
            }
            else
            {
                return RedirectToPage("/Account/Login");
            }
        }
    }
}