using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JDMovie.Models;

namespace JDMovie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCttapPhimsController : Controller
    {
        private readonly dbDACNContext _context;

        public AdminCttapPhimsController(dbDACNContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminCttapPhims
        public async Task<IActionResult> Index()
        {
            var dbDACNContext = _context.CttapPhims.Include(c => c.IdNavigation);
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
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.DsphimBos, "Id", "Id");
            return View();
        }

        // POST: Admin/AdminCttapPhims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idphim,TapPhim,Id,Link")] CttapPhim cttapPhim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cttapPhim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.DsphimBos, "Id", "Id", cttapPhim.Id);
            return View(cttapPhim);
        }

        // GET: Admin/AdminCttapPhims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cttapPhim = await _context.CttapPhims.FindAsync(id);
            if (cttapPhim == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.DsphimBos, "Id", "Id", cttapPhim.Id);
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
                    _context.Update(cttapPhim);
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.DsphimBos, "Id", "Id", cttapPhim.Id);
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
            return RedirectToAction(nameof(Index));
        }

        private bool CttapPhimExists(int id)
        {
            return _context.CttapPhims.Any(e => e.Idphim == id);
        }
    }
}
