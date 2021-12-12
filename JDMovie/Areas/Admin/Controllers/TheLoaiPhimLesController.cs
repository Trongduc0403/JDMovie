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
    public class TheLoaiPhimLesController : Controller
    {
        private readonly dbDACNContext _context;

        public TheLoaiPhimLesController(dbDACNContext context)
        {
            _context = context;
        }

        // GET: Admin/TheLoaiPhimLes
        public async Task<IActionResult> Index()
        {
            var dbDACNContext = _context.TheLoaiPhimLes.Include(t => t.IdphimLeNavigation).Include(t => t.IdtheLoaiNavigation);
            return View(await dbDACNContext.ToListAsync());
        }

        // GET: Admin/TheLoaiPhimLes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theLoaiPhimLe = await _context.TheLoaiPhimLes
                .Include(t => t.IdphimLeNavigation)
                .Include(t => t.IdtheLoaiNavigation)
                .FirstOrDefaultAsync(m => m.IdphimLe == id);
            if (theLoaiPhimLe == null)
            {
                return NotFound();
            }

            return View(theLoaiPhimLe);
        }

        // GET: Admin/TheLoaiPhimLes/Create
        public IActionResult Create()
        {
            ViewData["IdphimLe"] = new SelectList(_context.DsphimLes, nameof(DsphimLe.Id), nameof(DsphimLe.TenPhim));
            ViewData["IdtheLoai"] = new SelectList(_context.TheLoais, nameof(TheLoai.IdtheLoai), nameof(TheLoai.TenTheLoai));
            return View();
        }

        // POST: Admin/TheLoaiPhimLes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdphimLe,IdtheLoai,K")] TheLoaiPhimLe theLoaiPhimLe)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(theLoaiPhimLe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdphimLe"] = new SelectList(_context.DsphimLes, nameof(DsphimLe.Id), nameof(DsphimLe.TenPhim), theLoaiPhimLe.IdphimLe);
            ViewData["IdtheLoai"] = new SelectList(_context.TheLoais, nameof(TheLoai.IdtheLoai), nameof(TheLoai.TenTheLoai), theLoaiPhimLe.IdtheLoai);
            return View(theLoaiPhimLe);
        }

        // GET: Admin/TheLoaiPhimLes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theLoaiPhimLe = await _context.TheLoaiPhimLes.FindAsync(id);
            if (theLoaiPhimLe == null)
            {
                return NotFound();
            }
            ViewData["IdphimLe"] = new SelectList(_context.DsphimLes, "Id", "Id", theLoaiPhimLe.IdphimLe);
            ViewData["IdtheLoai"] = new SelectList(_context.TheLoais, "IdtheLoai", "IdtheLoai", theLoaiPhimLe.IdtheLoai);
            return View(theLoaiPhimLe);
        }

        // POST: Admin/TheLoaiPhimLes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdphimLe,IdtheLoai,K")] TheLoaiPhimLe theLoaiPhimLe)
        {
            if (id != theLoaiPhimLe.IdphimLe)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(theLoaiPhimLe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TheLoaiPhimLeExists(theLoaiPhimLe.IdphimLe))
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
            ViewData["IdphimLe"] = new SelectList(_context.DsphimLes, "Id", "Id", theLoaiPhimLe.IdphimLe);
            ViewData["IdtheLoai"] = new SelectList(_context.TheLoais, "IdtheLoai", "IdtheLoai", theLoaiPhimLe.IdtheLoai);
            return View(theLoaiPhimLe);
        }

        // GET: Admin/TheLoaiPhimLes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theLoaiPhimLe = await _context.TheLoaiPhimLes
                .Include(t => t.IdphimLeNavigation)
                .Include(t => t.IdtheLoaiNavigation)
                .FirstOrDefaultAsync(m => m.IdphimLe == id);
            if (theLoaiPhimLe == null)
            {
                return NotFound();
            }

            return View(theLoaiPhimLe);
        }

        // POST: Admin/TheLoaiPhimLes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var theLoaiPhimLe = await _context.TheLoaiPhimLes.FindAsync(id);
            _context.TheLoaiPhimLes.Remove(theLoaiPhimLe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TheLoaiPhimLeExists(int id)
        {
            return _context.TheLoaiPhimLes.Any(e => e.IdphimLe == id);
        }
    }
}
