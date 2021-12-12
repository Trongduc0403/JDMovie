using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace JDMovie.Areas.Admin.Models.ViewModels
{
    public class PhimLeViewModel
    {
        public string TenPhim { get; set; }
        public string NoiDung { get; set;}
        public int NamPhatHanh { get;}
        public string ThoiLuong { get; set; }
        public string Img { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
        public int MaQg{ get; set; }
        public int LuotXem { get; set; }
        public string Link { get; set; }
        public int TheLoai { get; set;}
    }
}
