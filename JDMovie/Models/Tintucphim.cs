using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace JDMovie.Models
{
    public partial class Tintucphim
    {
        public int Idtintuc { get; set; }
        public string? Tieude { get; set; }
        public string? Tomtat { get; set; }
        public string? Noidung { get; set; }
        public string? Hinhanh { get; set; }
        public DateTime? Ngaycapnhat { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
        public int? Luotxem { get; set; }
    }
}
