using System;
using System.Collections.Generic;

#nullable disable

namespace BillsEntity.Models
{
    public partial class VNDDTL
    {
        public VNDDTL()
        {
            BILHDRs = new HashSet<BILHDR>();
        }

        public int VNDCOD { get; set; }
        public string VNDNAM { get; set; }

        public virtual ICollection<BILHDR> BILHDRs { get; set; }
    }
}
