using System;
using System.Collections.Generic;

namespace JDMovie.Models
{
    public partial class Banner
    {
        public int Id { get; set; }
        public string? Img { get; set; }
        public int? Idphim { get; set; }
    }
}
