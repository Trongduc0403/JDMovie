using System;
using System.Collections.Generic;

namespace JDMovie.Models
{
    public partial class Nam
    {
        public Nam()
        {
            DsphimBos = new HashSet<DsphimBo>();
            DsphimLes = new HashSet<DsphimLe>();
        }

        public int MaNam { get; set; }
        public int? TenNam { get; set; }

        public virtual ICollection<DsphimBo> DsphimBos { get; set; }
        public virtual ICollection<DsphimLe> DsphimLes { get; set; }
    }
}
