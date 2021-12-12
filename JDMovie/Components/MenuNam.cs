using JDMovie.Models;
using Microsoft.AspNetCore.Mvc;

namespace JDMovie.Components
{
    public class MenuNam : ViewComponent
    {
        private readonly dbDACNContext _context;
        public MenuNam(dbDACNContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var nam = _context.Nams.OrderBy(p => p.TenNam);
            return View(nam);
        }
    }
}
