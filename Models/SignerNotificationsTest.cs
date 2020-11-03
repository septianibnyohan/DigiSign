using System;
using System.Collections.Generic;

namespace DigiSign.Models
{
    public partial class SignerNotificationsTest
    {
        public int Id { get; set; }
        public int? User { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public DateTime? Time { get; set; }
    }
}
