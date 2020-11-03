using System;
using System.Collections.Generic;

namespace DigiSign.Models
{
    public partial class SignerApiFace
    {
        public string TransactionId { get; set; }
        public string RequestId { get; set; }
        public string StatusFaceapp { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string ErrorLog { get; set; }
    }
}
