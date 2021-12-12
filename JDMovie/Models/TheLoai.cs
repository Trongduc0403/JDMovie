using System;
using System.Collections.Generic;

namespace JDMovie.Models
{
    public partial class TheLoai
    {
        public TheLoai()
        {
            TheLoaiPhimBos = new HashSet<TheLoaiPhimBo>();
            TheLoaiPhimLes = new HashSet<TheLoaiPhimLe>();
        }

        public int IdtheLoai { get; set; }
        public string? TenTheLoai { get; set; }

        public virtual ICollection<TheLoaiPhimBo> TheLoaiPhimBos { get; set; }
        public virtual ICollection<TheLoaiPhimLe> TheLoaiPhimLes { get; set; }
    }
}
