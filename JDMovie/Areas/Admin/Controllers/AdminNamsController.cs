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
    public class AdminNamsController : Controller
    {
        private readonly dbDACNContext _context;

        public AdminNamsController(dbDACNContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminNams
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nams.ToListAsync());
        }

        // GET: Admin/AdminNams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nam = await _context.Nams
                .FirstOrDefaultAsync(m => m.MaNam == id);
            if (nam == null)
            {
                return NotFound();
            }

            return View(nam);
        }

        // GET: Admin/AdminNams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminNams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNam,TenNam")] Nam nam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nam);
        }

        // GET: Admin/AdminNams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nam = await _context.Nams.FindAsync(id);
            if (nam == null)
            {
                return NotFound();
            }
            return View(nam);
        }

        // POST: Admin/AdminNams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNam,TenNam")] Nam nam)
        {
            if (id != nam.MaNam)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NamExists(nam.MaNam))
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
            return View(nam);
        }

        // GET: Admin/AdminNams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nam = await _context.Nams
                .FirstOrDefaultAsync(m => m.MaNam == id);
            if (nam == null)
            {
                return NotFound();
            }

            return View(nam);
        }

        // POST: Admin/AdminNams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nam = await _context.Nams.FindAsync(id);
            _context.Nams.Remove(nam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NamExists(int id)
        {
            return _context.Nams.Any(e => e.MaNam == id);
        }
    }
}
