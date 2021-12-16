using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JDMovie.Models;
using PagedList.Core;
using JDMovie.Helper;
using ReflectionIT.Mvc.Paging;

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
        public async Task<IActionResult> Details(int id, int? Tap = 1)
        {
            if (id == null)
            {
                return NotFound();
            }
            var homephimbo = (from phimbo in _context.DsphimBos where phimbo.Id == id select phimbo).FirstOrDefault();
            var homecttapphim = (from ctphim in _context.CttapPhims where ctphim.Id == id select ctphim).AsNoTracking().OrderBy(s => s.TapPhim);


            var nam = (from n in _context.Nams
                      where n.MaNam == homephimbo.NamPhatHanh 
                      select n).FirstOrDefault();

            var quocgia = (from q in _context.QuocGia
                      where q.MaQg == homephimbo.MaQg
                      select q).FirstOrDefault();


            ViewBag.phimbo = homephimbo;
            ViewBag.ctphim = homecttapphim;
            ViewBag.nam = nam; 
            ViewBag.quocgia = quocgia;

            string route = "https://meet.google.com/rff-exao-iam/" + id;
            ViewBag.route = route;



            if (ViewBag.phimbo == null)
            {
                return NotFound();
            }



            var model = await PagingList.CreateAsync(homecttapphim, 1, (int)Tap);

            return View(model);

        }


    }
}
