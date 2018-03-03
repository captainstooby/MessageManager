using System;

namespace MessageManager.Domain.Import
{
    public class Message
    {
        public string Mp3FileName { get; internal set; }
        public DateTime DateOfRecording { get; internal set; }

        public bool Imported { get; set; }
    }
}
