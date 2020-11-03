using System;
using System.Collections.Generic;

namespace DigiSign.Models
{
    public partial class SignerDelegationHistory
    {
        public int Id { get; set; }
        public int UserOrigin { get; set; }
        public int? UserDelegated { get; set; }
        public string Reason { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
