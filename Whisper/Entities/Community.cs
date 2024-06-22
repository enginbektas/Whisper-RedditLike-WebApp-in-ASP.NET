using System.Collections.Generic;

namespace Whisper.Entities
{
    public class Community : EntityBase<string>
    {
        public string Name { get; set; }

        public string Description { get; set; }


        #region Navigation Properties

        public string CreatorId { get; set; }

        public User Creator { get; set; }

        public List<User> Moderators { get; set; }

        public List<User> Members { get; set; }

        public List <Thread> Threads { get; set; }

        #endregion
    }
}
