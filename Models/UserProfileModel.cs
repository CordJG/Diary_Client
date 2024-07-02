using Diary_Server.Dtos.Diarys;

namespace Diary_Client.Models
{
    // ProfileModel.cs
    public class UserProfileModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public int DiaryCount { get; set; }
        public Dictionary<int, Dictionary<int, List<DiaryDto>>> DiariesByYearMonth { get; set; }
    }

}
