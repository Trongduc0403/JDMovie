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
    public class AdminTintucphimsController : Controller
    {
        private readonly dbDACNContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public AdminTintucphimsController(dbDACNContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Admin/AdminTintucphims
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tintucphims.ToListAsync());
        }

        // GET: Admin/AdminTintucphims/Details/5
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

        // GET: Admin/AdminTintucphims/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminTintucphims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idtintuc,Tieude,Tomtat,Noidung,Hinhanh,ImageFile,Ngaycapnhat,Luotxem")] Tintucphim tintucphim)
        {
            if (ModelState.IsValid)
            {

                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(tintucphim.ImageFile.FileName);
                string extension = Path.GetExtension(tintucphim.ImageFile.FileName);
                tintucphim.Hinhanh = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/img/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await tintucphim.ImageFile.CopyToAsync(fileStream);
                }

                _context.Add(tintucphim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tintucphim);
        }

        // GET: Admin/AdminTintucphims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tintucphim = await _context.Tintucphims.FindAsync(id);
            if (tintucphim == null)
            {
                return NotFound();
            }
            return View(tintucphim);
        }

        // POST: Admin/AdminTintucphims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idtintuc,Tieude,Tomtat,Noidung,Hinhanh,Ngaycapnhat,Luotxem")] Tintucphim tintucphim)
        {
            if (id != tintucphim.Idtintuc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tintucphim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TintucphimExists(tintucphim.Idtintuc))
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
            return View(tintucphim);
        }

        // GET: Admin/AdminTintucphims/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Admin/AdminTintucphims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tintucphim = await _context.Tintucphims.FindAsync(id);
            _context.Tintucphims.Remove(tintucphim);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TintucphimExists(int id)
        {
            return _context.Tintucphims.Any(e => e.Idtintuc == id);
        }
    }
}
