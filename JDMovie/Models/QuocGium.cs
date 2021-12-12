using System;
using System.Collections.Generic;

namespace JDMovie.Models
{
    public partial class QuocGium
    {
        public QuocGium()
        {
            DsphimBos = new HashSet<DsphimBo>();
            DsphimLes = new HashSet<DsphimLe>();
        }

        public int MaQg { get; set; }
        public string? TenQg { get; set; }

        public virtual ICollection<DsphimBo> DsphimBos { get; set; }
        public virtual ICollection<DsphimLe> DsphimLes { get; set; }
    }
}
