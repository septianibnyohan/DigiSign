using System;
using System.Collections.Generic;

namespace DigiSign.Models
{
    public partial class SignerPayloadCallback
    {
        public int Id { get; set; }
        public string RefId { get; set; }
        public string RequestId { get; set; }
        public string ApplicationId { get; set; }
        public string TransactionCode { get; set; }
        public string UserId { get; set; }
        public string TargetUserId { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
        public string ActionDate { get; set; }
    }
}
