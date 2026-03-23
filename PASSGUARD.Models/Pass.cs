using System;

namespace PASSGUARD.Models
{
    public class Pass
    {
        public int PassId { get; set; }

        public string Code { get; set; }

        public DateTime Expiry { get; set; }

        public int VisitorId { get; set; }

        public Visitor Visitor { get; set; }
    }
}