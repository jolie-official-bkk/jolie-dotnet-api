using JolieApi.Models;

namespace JolieApi.ViewModels
{
    public class UserInfo
    {
        public int user_id { get; set; }
        public string email { get; set; }
        public string first_name { get;set; }
        public string last_name { get; set; }
        public DateTime date_of_birth { get; set; }
        public Gender gender { get; set;}
    }
}
