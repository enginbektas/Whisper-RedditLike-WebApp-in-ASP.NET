using System.Collections.Generic;
using Whisper.Entities.Enums;
using Whisper.Entities;

namespace Whisper.Models.UserModels
{
    public class UsersViewModel
    {
        public User User { get; set; }
        public IEnumerable<User> Users { get; set; }
        public string ErrorMessage { get; set; }
        public string Password = string.Empty;


        public UsersViewModel()
        {
            User = new User();
        }
    }
}
