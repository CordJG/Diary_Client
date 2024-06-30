using Diary_Client.Services;
using Diary_Server.Dtos.Diarys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Diary_Client.Pages.Diary
{
    public class EditModel : PageModel
    {
        private readonly IApiDiaryService _diaryService;

        public EditModel(IApiDiaryService diaryService)
        {
            _diaryService = diaryService;
        }

        [BindProperty]
        public UpdateDiaryDto Diary { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return Page();
            }

            var diary = await _diaryService.GetDiaryById(id.Value);
            if (diary == null)
            {
                return NotFound();
            }

            Diary = new UpdateDiaryDto
            {
                Id = diary.Id,
                Title = diary.Title,
                Content = diary.Content,
                IsPrivate = diary.IsPrivate,
                DiaryDate = diary.DiaryDate
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Convert DiaryDate to UTC before updating
            Diary.DiaryDate = DateTime.SpecifyKind(Diary.DiaryDate, DateTimeKind.Utc);

            await _diaryService.UpdateEntry(Diary);

            return RedirectToPage("./Index");
        }
    }
}
