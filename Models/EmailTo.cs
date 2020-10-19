using System.Collections.Generic;
using System.Net.Mail;

namespace DigiSign.Models
{
    public class EmailTo
    {
        public List<string> To { get; set; }
        public List<string> Cc { get; set; }
        public List<string> Bcc { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}