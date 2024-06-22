using Whisper.Entities;
using System.Collections.Generic;


namespace Whisper.Models
{
    public class CommunityViewModel
    {
        public List<Community> Communities { get; set; }
        public Community Community { get; set; }
        public string ErrorMessage { get; set; }
    }
}
