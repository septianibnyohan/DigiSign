using System;
using System.Collections.Generic;

namespace DigiSign.Models
{
    public partial class SignerLogs
    {
        public string Activity { get; set; }
        public DateTime Time { get; set; }
        public string IpAddress { get; set; }
        public int? Users { get; set; }
        public string Uri { get; set; }
        public int Id { get; set; }
    }
}
