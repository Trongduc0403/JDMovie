using System;
using System.Collections.Generic;

namespace JDMovie.Models
{
    public partial class TheLoaiPhimBo
    {
        public int IdphimBo { get; set; }
        public int IdtheLoai { get; set; }
        public string? K { get; set; }

        public virtual DsphimBo IdphimBoNavigation { get; set; } = null!;
        public virtual TheLoai IdtheLoaiNavigation { get; set; } = null!;
    }
}
