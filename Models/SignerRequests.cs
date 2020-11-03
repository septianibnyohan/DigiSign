using System;
using System.Collections.Generic;

namespace DigiSign.Models
{
    public partial class SignerRequests
    {
        public int Id { get; set; }
        public int? Company { get; set; }
        public string Sender { get; set; }
        public string SenderNote { get; set; }
        public DateTime? SendTime { get; set; }
        public string Status { get; set; }
        public string WorkflowMethod { get; set; }
        public int? NextOrder { get; set; }
        public string RequestorEmail { get; set; }
        public string DepartmentId { get; set; }
        public int? ClassificationId { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string PdfCertified { get; set; }
        public int? SenderOld { get; set; }
        public string Reason { get; set; }
    }
}
