namespace JDMovie.Models.ViewModels
{
    public class HeaderViewModel
    {
        public int IdTheLoai { get; set; }
        public string TenTheLoai { get; set; }
        public int MaNam { get; set; }
        public string TenNam { get; set; }
        public int MaQG { get; set; }
        public string TenQG { get; set; }
        public List<TheLoai> ListTheLoai { get; set; }
        public List<Nam> ListNam { get; set; }
        public List<QuocGium> ListQuocQia { get; set; }

    }
}
