using System;
using System.Text;

namespace MessageManager.Domain.Import
{
    public class Message
    {
        public string MessagePath { get; set; }
        public DateTime MessageRecordingDate { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public bool Imported { get; set; }

        public override string ToString()
        {
            return ((new StringBuilder())
                .AppendFormat("Message Path:  {0}", MessagePath)
                .AppendLine()
                .AppendFormat("Message Recording Date:  {0}", MessageRecordingDate))
                .ToString();
        }
    }
}
