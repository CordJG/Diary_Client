using Microsoft.AspNetCore.Mvc.RazorPages;
using Diary_Server.Dtos.Diarys;
using Diary_Server.Services;
using Microsoft.AspNetCore.Identity;
using Diary_Client.Services;

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

            string userIdString = "1";

            // string 타입의 userId를 long 타입으로 변환
            if (long.TryParse(userIdString, out long userId))
            {
                Diaries = await _diaryService.GetUserDiarys(userId);
            }
            else
            {
                // 변환에 실패한 경우, Diaries를 비어있는 상태로 설정
                Diaries = new List<DiaryDto>();
            }
        }
    }
}
