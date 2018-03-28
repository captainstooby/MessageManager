using System;

namespace MessageManager.Domain.Import
{
    public class Message
    {
        public string MessagePath { get; set; }
        public DateTime MessageRecordingDate { get; set; }

        public bool Imported { get; set; }
    }
}
