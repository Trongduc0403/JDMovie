using JDMovie.Models;
using Microsoft.AspNetCore.Mvc;

namespace JDMovie.Components
{
    public class MenuQuocGia : ViewComponent
    {
        private readonly dbDACNContext _context;
        public MenuQuocGia(dbDACNContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var quocgia = _context.QuocGia.OrderBy(p => p.TenQg);
            return View(quocgia);
        }
    }
}
