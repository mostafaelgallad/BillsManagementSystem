using System;
using System.Collections.Generic;

#nullable disable

namespace BillsEntity.Models
{
    public partial class BILHDR
    {
        public BILHDR()
        {
            BILDTLs = new HashSet<BILDTL>();
        }

        public int BILCOD { get; set; }
        public DateTime BILDAT { get; set; }
        public int VNDCOD { get; set; }
        public decimal? BILPRC { get; set; }
        public string BILIMG { get; set; }

        public virtual VNDDTL VNDCODNavigation { get; set; }
        public virtual ICollection<BILDTL> BILDTLs { get; set; }
    }
}
