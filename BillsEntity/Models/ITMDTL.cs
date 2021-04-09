using System;
using System.Collections.Generic;

#nullable disable

namespace BillsEntity.Models
{
    public partial class ITMDTL
    {
        public ITMDTL()
        {
            BILDTLs = new HashSet<BILDTL>();
        }

        public int ITMCOD { get; set; }
        public string ITMNAM { get; set; }
        public decimal ITMPRC { get; set; }

        public virtual ICollection<BILDTL> BILDTLs { get; set; }
    }
}
