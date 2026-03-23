using System;

namespace PASSGUARD.Models
{
    public class CheckIn
    {
        public int CheckInId { get; set; }

        public int PassId { get; set; }

        public int SecurityId { get; set; }

        public DateTime Time { get; set; }

        public Pass Pass { get; set; }

        public Security Security { get; set; }
    }
}