using System.ComponentModel.DataAnnotations;

namespace Whisper.Models.UserModels
{
    public class UserLoginViewModel
    {
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ErrorMessage { get; set; }

    }
}
