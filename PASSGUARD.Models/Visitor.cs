using System.Collections.Generic;

namespace PASSGUARD.Models
{
    public class Visitor
    {
        public int VisitorId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public ICollection<Pass> Passes { get; set; }
    }
}