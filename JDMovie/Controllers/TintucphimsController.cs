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
    public class TintucphimsController : Controller
    {
        private readonly dbDACNContext _context;

        public TintucphimsController(dbDACNContext context)
        {
            _context = context;
        }

        // GET: Tintucphims
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var lsTinTuc = _context.Tintucphims.AsNoTracking().OrderByDescending(x => x.Idtintuc);
            PagedList<Tintucphim> models = new PagedList<Tintucphim>(lsTinTuc, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
            
        }

        // GET: Tintucphims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tintucphim = await _context.Tintucphims
                .FirstOrDefaultAsync(m => m.Idtintuc == id);
            if (tintucphim == null)
            {
                return NotFound();
            }

            return View(tintucphim);
        }

   
       
    }
}
