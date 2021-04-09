using System;
using System.Collections.Generic;

#nullable disable

namespace BillsEntity.Models
{
    public partial class BILDTL
    {
        public int DTLCOD { get; set; }
        public int BILCOD { get; set; }
        public int ITMCOD { get; set; }
        public decimal ITMPRC { get; set; }
        public int ITMQTY { get; set; }

        public virtual BILHDR BILCODNavigation { get; set; }
        public virtual ITMDTL ITMCODNavigation { get; set; }
    }
}
