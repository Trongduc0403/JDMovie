using System;
using System.Collections.Generic;

namespace JDMovie.Models
{
    public partial class LichSu
    {
        public int Idtk { get; set; }
        public int Idphim { get; set; }

        public virtual DsphimBo IdphimNavigation { get; set; } = null!;
        public virtual TaiKhoan IdtkNavigation { get; set; } = null!;
    }
}
