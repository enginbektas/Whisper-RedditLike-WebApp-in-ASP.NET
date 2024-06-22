using Whisper.Entities;
using System.Collections.Generic;


namespace Whisper.Models
{
    public class ThreadViewModel
    {
        public List<Thread> Threads { get; set; }
        public Community Community { get; set; }
        public Thread Thread { get; set; }
        public string ErrorMessage { get; set; }
    }
}
