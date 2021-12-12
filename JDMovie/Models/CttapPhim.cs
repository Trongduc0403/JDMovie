using System;
using System.Collections.Generic;

namespace JDMovie.Models
{
    public partial class CttapPhim
    {
        public int Idphim { get; set; }
        public int? TapPhim { get; set; }
        public int? Id { get; set; }
        public string? Link { get; set; }

        public virtual DsphimBo? IdNavigation { get; set; }
    }
}
