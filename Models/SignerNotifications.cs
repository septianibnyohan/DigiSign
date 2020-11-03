using System;
using System.Collections.Generic;

namespace DigiSign.Models
{
    public partial class SignerNotifications
    {
        public int Id { get; set; }
        public int? Users { get; set; }
        public string Type { get; set; }
        public DateTime? Time { get; set; }
        public string Message { get; set; }
    }
}
