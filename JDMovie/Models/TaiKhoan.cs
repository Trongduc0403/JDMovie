using System;
using System.Collections.Generic;

namespace JDMovie.Models
{
    public partial class TaiKhoan
    {
        public TaiKhoan()
        {
            HopPhims = new HashSet<HopPhim>();
        }

        public int Idtk { get; set; }
        public string? Email { get; set; }
        public string? HoTen { get; set; }
        public string? MatKhau { get; set; }
        public string? Avatar { get; set; }
        public bool? Quyen { get; set; }

        public virtual ICollection<HopPhim> HopPhims { get; set; }
    }
}
