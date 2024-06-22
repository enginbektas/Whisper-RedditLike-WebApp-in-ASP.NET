using Microsoft.AspNetCore.Authentication.Cookies;

namespace Whisper.Entities
{
    public class Comment : EntityBase<string>
    {

        // Image File
        public string Description { get; set; }

        public int Upvotes { get; set; } = 0;

        public int Downvotes { get; set; } = 0;

        #region Navigation Properties

        public string UserId { get; set; }

        public User User { get; set; }

        public string ThreadId { get; set; }

        public Thread Thread { get; set; }

        #endregion
    }
}
