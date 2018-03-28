using System;

namespace MessageManager.Domain.Import
{
    public class Message
    {
        public string Mp3FileName { get; set; }
        public DateTime DateOfRecording { get; set; }

        public bool Imported { get; set; }
    }
}
