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

            // string Ÿ���� userId�� long Ÿ������ ��ȯ
            if (long.TryParse(userIdString, out long userId))
            {
                Diaries = await _diaryService.GetUserDiarys(userId);
            }
            else
            {
                // ��ȯ�� ������ ���, Diaries�� ����ִ� ���·� ����
                Diaries = new List<DiaryDto>();
            }
        }
    }
}
