using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JDMovie.Models;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace JDMovie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCttapPhimsController : Controller
    {
        private readonly dbDACNContext _context;
        public INotyfService _notyfService { get; }

        public AdminCttapPhimsController(dbDACNContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminCttapPhims
        public async Task<IActionResult> Index(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }    
            var dbDACNContext = (from phim in _context.CttapPhims
                                 where phim.Id == id
                                 select phim).Include(c => c.IdNavigation);

            ViewBag.Id = id;
            return View(await dbDACNContext.ToListAsync());
        }

        // GET: Admin/AdminCttapPhims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cttapPhim = await _context.CttapPhims
                .Include(c => c.IdNavigation)
                .FirstOrDefaultAsync(m => m.Idphim == id);
            if (cttapPhim == null)
            {
                return NotFound();
            }

            return View(cttapPhim);
        }

        // GET: Admin/AdminCttapPhims/Create
        public IActionResult Create(int id)
        {
            ViewData["Id"] = new SelectList(_context.DsphimBos, nameof(DsphimBo.Id), nameof(DsphimBo.TenPhim));
            return View();
        }

        // POST: Admin/AdminCttapPhims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,[Bind("Idphim,TapPhim,Id,Link")] CttapPhim cttapPhim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cttapPhim);
                await _context.SaveChangesAsync();
                ViewData["Id"] = new SelectList(_context.DsphimBos, nameof(DsphimBo.Id), nameof(DsphimBo.TenPhim), cttapPhim.Id);
                _notyfService.Success("Thêm thành công!");
                return View(cttapPhim);
            }
            ViewData["Id"] = new SelectList(_context.DsphimBos, nameof(DsphimBo.Id), nameof(DsphimBo.TenPhim), cttapPhim.Id);
            return View(cttapPhim);
        }

        // GET: Admin/AdminCttapPhims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.idphim = id;

            var cttapPhim = await _context.CttapPhims.FindAsync(id);
            if (cttapPhim == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.DsphimBos, nameof(DsphimBo.Id), nameof(DsphimBo.TenPhim), cttapPhim.Id);
            return View(cttapPhim);
        }

        // POST: Admin/AdminCttapPhims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idphim,TapPhim,Id,Link")] CttapPhim cttapPhim)
        {
            if (id != cttapPhim.Idphim)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.CttapPhims.Update(cttapPhim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CttapPhimExists(cttapPhim.Idphim))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var routeValue = new RouteValueDictionary(new { cttapPhim.Id, action = "Index", controller = "AdminCttapPhims" });

                return RedirectToRoute(routeValue);
            }
            ViewData["Id"] = new SelectList(_context.DsphimBos, nameof(DsphimBo.Id), nameof(DsphimBo.TenPhim), cttapPhim.Id);
            return View(cttapPhim);
        }

        // GET: Admin/AdminCttapPhims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cttapPhim = await _context.CttapPhims
                .Include(c => c.IdNavigation)
                .FirstOrDefaultAsync(m => m.Idphim == id);
            if (cttapPhim == null)
            {
                return NotFound();
            }

            return View(cttapPhim);
        }

        // POST: Admin/AdminCttapPhims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cttapPhim = await _context.CttapPhims.FindAsync(id);
            _context.CttapPhims.Remove(cttapPhim);
            await _context.SaveChangesAsync();

            var routeValue = new RouteValueDictionary(new { cttapPhim.Id, action = "Index", controller = "AdminCttapPhims" });

            return RedirectToRoute(routeValue);
        }

        private bool CttapPhimExists(int id)
        {
            return _context.CttapPhims.Any(e => e.Idphim == id);
        }
    }
}
