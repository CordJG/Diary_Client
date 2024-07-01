using Diary_Client.Services;
using Diary_Server.Dtos.Diarys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Diary_Client.Pages.Diary
{
    public class DetailModel : PageModel
    {
        private readonly IApiDiaryService _diaryService;

        public DetailModel(IApiDiaryService diaryService)
        {
            _diaryService = diaryService;
        }

        public DiaryDto Diary { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            Diary = await _diaryService.GetDiaryById(id);

            if (Diary == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
