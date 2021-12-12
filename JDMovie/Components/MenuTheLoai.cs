using JDMovie.Models;
using Microsoft.AspNetCore.Mvc;

namespace JDMovie.Components
{
    public class MenuTheLoai : ViewComponent
    {
        private readonly dbDACNContext _context;
        public MenuTheLoai(dbDACNContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var theloai = _context.TheLoais.OrderBy(p => p.TenTheLoai);
            return View(theloai);
        }
    }
}
