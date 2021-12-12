using System;
using System.Collections.Generic;

namespace JDMovie.Models
{
    public partial class TheLoaiPhimLe
    {
        public int IdphimLe { get; set; }
        public int IdtheLoai { get; set; }
        public string? K { get; set; }

        public virtual DsphimLe IdphimLeNavigation { get; set; } = null!;
        public virtual TheLoai IdtheLoaiNavigation { get; set; } = null!;
    }
}
