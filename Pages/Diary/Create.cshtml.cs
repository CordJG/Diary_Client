using Diary_Client.Services;
using Diary_Server.Dtos.Diarys;
using Diary_Server.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Diary_Client.Pages.Diary
{
    public class CreateModel : PageModel
    {
        private readonly IApiDiaryService _diaryService;
    

        public CreateModel(IApiDiaryService diaryService)
        {
            _diaryService = diaryService;
          
        }

        [BindProperty]
        public CreateDiaryDto Diary { get; set; } = new CreateDiaryDto();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            if (HttpContext.Session.TryGetValue("UserId", out var userIdBytes))
            {
                var userIdString = System.Text.Encoding.UTF8.GetString(userIdBytes);
                if (long.TryParse(userIdString, out long userId))
                {
                    Diary.UserId = userId;
                    await _diaryService.CreateEntry(Diary);
                    return RedirectToPage("./Index");
                }
            }

            return Page();
        }
    }
}
