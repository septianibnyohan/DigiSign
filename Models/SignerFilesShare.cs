using System;
using System.Collections.Generic;

namespace DigiSign.Models
{
    public partial class SignerFilesShare
    {
        public int ShareId { get; set; }
        public int RequestId { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeEmail { get; set; }
        public string Rules { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string SharedKey { get; set; }
    }
}
