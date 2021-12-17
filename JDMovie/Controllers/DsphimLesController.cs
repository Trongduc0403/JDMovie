using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JDMovie.Models;
using PagedList.Core;

namespace JDMovie.Controllers
{
    public class DsphimLesController : Controller
    {
        private readonly dbDACNContext _context;

        public DsphimLesController(dbDACNContext context)
        {
            _context = context;
        }

        // GET: DsphimLes
        public async Task<IActionResult> Index()
        {
            ViewData["MaQg"] = new SelectList(_context.QuocGia, "MaQg", "TenQg");
            ViewData["NamPhatHanh"] = new SelectList(_context.Nams, "MaNam", "TenNam");
            var dbDACNContext = _context.DsphimLes.Include(d => d.MaQgNavigation).Include(d => d.NamPhatHanhNavigation);
           return View(await dbDACNContext.ToListAsync());
        }

        // GET: DsphimLes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var dsphimLe = await _context.DsphimLes
                .Include(d => d.MaQgNavigation)
                .Include(d => d.NamPhatHanhNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dsphimLe == null)
            {
                return NotFound();
            }

            var nam = (from n in _context.Nams
                       where n.MaNam == dsphimLe.NamPhatHanh
                       select n).FirstOrDefault();

            var quocgia = (from q in _context.QuocGia
                           where q.MaQg == dsphimLe.MaQg
                           select q).FirstOrDefault();

            var theloai = (from t in _context.TheLoaiPhimLes
                           where t.IdphimLe == id
                           select t).Include(d => d.IdtheLoaiNavigation).ToList(); ;

            ViewBag.nam = nam;
            ViewBag.quocgia = quocgia;
            ViewBag.theloai = theloai;



            string route = "https://meet.google.com/jgb-roos-uwp/"+id;

            ViewBag.route = route;

            return View(dsphimLe);
        }

    }
}
