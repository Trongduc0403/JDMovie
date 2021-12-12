using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JDMovie.Models;

namespace JDMovie.Controllers
{
    public class DsphimBoesController : Controller
    {
        private readonly dbDACNContext _context;
        private dbDACNContext db = new dbDACNContext();

        public DsphimBoesController(dbDACNContext context)
        {
            _context = context;
        }

        // GET: DsphimBoes
        public async Task<IActionResult> Index()
        {
            var dbDACNContext = _context.DsphimBos.Include(d => d.MaQgNavigation).Include(d => d.NamPhatHanhNavigation);
            
            return View(await dbDACNContext.ToListAsync());
        }

        private List<CttapPhim> laytap(int id, int count)
        {

            return db.CttapPhims.Where(m => m.Id == id).Take(count).ToList();
        }

        // GET: DsphimBoes/Details/5
        public async Task<IActionResult> Details(int? id, int? Tap)
        {
            if (id == null)
            {
                return NotFound();
            }
            var homephimbo = (from phimbo in _context.DsphimBos select phimbo).ToList();
            var homecttapphim = (from ctphim in _context.CttapPhims where ctphim.Id == id select ctphim).ToList();

            ViewBag.phimbo = homephimbo;
            ViewBag.ctphim = homecttapphim;
            var dsphimBo = await _context.DsphimBos
                .Include(d => d.MaQgNavigation)
                .Include(d => d.NamPhatHanhNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dsphimBo == null)
            {
                return NotFound();
            }

            return View(dsphimBo);
        }


    }
}
