using System.Collections.Generic;

namespace Whisper.Entities
{
    public class Thread : EntityBase<string>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int Upvotes { get; set; } = 0;

        public int Downvotes { get; set; } = 0;

        #region Navigation Properties

        public string UserId { get; set; }

        public User User { get; set; }

        public string CommunityId { get; set; }

        public Community Community { get; set; }

        public List<Comment> Comments { get; set; }


        #endregion
    }
}
