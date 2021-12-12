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
    public class AdminGioithieuxController : Controller
    {
        private readonly dbDACNContext _context;

        public AdminGioithieuxController(dbDACNContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminGioithieux
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gioithieus.ToListAsync());
        }

        // GET: Admin/AdminGioithieux/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioithieu = await _context.Gioithieus
                .FirstOrDefaultAsync(m => m.Idgioitin == id);
            if (gioithieu == null)
            {
                return NotFound();
            }

            return View(gioithieu);
        }

        // GET: Admin/AdminGioithieux/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminGioithieux/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idgioitin,Noidung,Sdtlien")] Gioithieu gioithieu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gioithieu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gioithieu);
        }

        // GET: Admin/AdminGioithieux/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioithieu = await _context.Gioithieus.FindAsync(id);
            if (gioithieu == null)
            {
                return NotFound();
            }
            return View(gioithieu);
        }

        // POST: Admin/AdminGioithieux/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idgioitin,Noidung,Sdtlien")] Gioithieu gioithieu)
        {
            if (id != gioithieu.Idgioitin)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gioithieu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GioithieuExists(gioithieu.Idgioitin))
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
            return View(gioithieu);
        }

        // GET: Admin/AdminGioithieux/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioithieu = await _context.Gioithieus
                .FirstOrDefaultAsync(m => m.Idgioitin == id);
            if (gioithieu == null)
            {
                return NotFound();
            }

            return View(gioithieu);
        }

        // POST: Admin/AdminGioithieux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gioithieu = await _context.Gioithieus.FindAsync(id);
            _context.Gioithieus.Remove(gioithieu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GioithieuExists(int id)
        {
            return _context.Gioithieus.Any(e => e.Idgioitin == id);
        }
    }
}
