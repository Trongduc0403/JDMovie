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
    public class AdminQuocGiumsController : Controller
    {
        private readonly dbDACNContext _context;
        public INotyfService _notyfService { get; }
        public AdminQuocGiumsController(dbDACNContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminQuocGiums
        public async Task<IActionResult> Index()
        {
            return View(await _context.QuocGia.ToListAsync());
        }

        // GET: Admin/AdminQuocGiums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quocGium = await _context.QuocGia
                .FirstOrDefaultAsync(m => m.MaQg == id);
            if (quocGium == null)
            {
                return NotFound();
            }

            return View(quocGium);
        }

        // GET: Admin/AdminQuocGiums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminQuocGiums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaQg,TenQg")] QuocGium quocGium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quocGium);
                await _context.SaveChangesAsync();
                _notyfService.Success("Thêm Thành Công !");
                return RedirectToAction(nameof(Index));
            }
            return View(quocGium);
        }

        // GET: Admin/AdminQuocGiums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quocGium = await _context.QuocGia.FindAsync(id);
            if (quocGium == null)
            {
                return NotFound();
            }
            return View(quocGium);
        }

        // POST: Admin/AdminQuocGiums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaQg,TenQg")] QuocGium quocGium)
        {
            if (id != quocGium.MaQg)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quocGium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuocGiumExists(quocGium.MaQg))
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
            return View(quocGium);
        }

        // GET: Admin/AdminQuocGiums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quocGium = await _context.QuocGia
                .FirstOrDefaultAsync(m => m.MaQg == id);
            if (quocGium == null)
            {
                return NotFound();
            }

            return View(quocGium);
        }

        // POST: Admin/AdminQuocGiums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quocGium = await _context.QuocGia.FindAsync(id);
            _context.QuocGia.Remove(quocGium);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuocGiumExists(int id)
        {
            return _context.QuocGia.Any(e => e.MaQg == id);
        }
    }
}
