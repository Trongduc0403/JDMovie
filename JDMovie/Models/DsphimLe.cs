using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace JDMovie.Models
{
    public partial class DsphimLe
    {
        public DsphimLe()
        {
            TheLoaiPhimLes = new HashSet<TheLoaiPhimLe>();
        }

        public int Id { get; set; }
        public string? TenPhim { get; set; }
        public string? NoiDung { get; set; }
        public int? NamPhatHanh { get; set; }
        public string? ThoiLuong { get; set; }
        public string? Img { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
        public int? MaQg { get; set; }
        public int? LuotXem { get; set; }
        public string? Link { get; set; }

        public virtual QuocGium? MaQgNavigation { get; set; }
        public virtual Nam? NamPhatHanhNavigation { get; set; }
        public virtual ICollection<TheLoaiPhimLe> TheLoaiPhimLes { get; set; }
    }
}
