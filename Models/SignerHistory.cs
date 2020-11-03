using System;
using System.Collections.Generic;

namespace DigiSign.Models
{
    public partial class SignerHistory
    {
        public string Files { get; set; }
        public string Activity { get; set; }
        public string Type { get; set; }
        public DateTime? Time { get; set; }
        public int Id { get; set; }
    }
}
