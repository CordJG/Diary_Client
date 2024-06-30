using Diary_Client.Services;
using Diary_Server.Dtos.Diarys;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Diary_Client.Pages.Diary
{
    public class IndexModel : PageModel
    {
        private readonly IApiDiaryService _diaryService;
  

        public IndexModel(IApiDiaryService diaryService)
        {
            _diaryService = diaryService;
           
        }

        public IEnumerable<DiaryDto> Diaries { get; set; }

        public async Task OnGetAsync()
        {

            var userIdString = HttpContext.Session.GetString("UserId");
            if (long.TryParse(userIdString, out long userId))
            {
                Diaries = await _diaryService.GetUserDiarys(userId);
            }
            else
            {
                Diaries = new List<DiaryDto>();
            }
        }
    }
}
