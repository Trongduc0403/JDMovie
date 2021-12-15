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
    public class AdminDsphimLesController : Controller
    {
        private readonly dbDACNContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminDsphimLesController(dbDACNContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Admin/AdminDsphimLes
        public async Task<IActionResult> Index()
        {
            var dbDACNContext = _context.DsphimLes.Include(d => d.MaQgNavigation).Include(d => d.NamPhatHanhNavigation);
            return View(await dbDACNContext.ToListAsync());
        }

        // GET: Admin/AdminDsphimLes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dsphimLe = await _context.DsphimLes
                .Include(d => d.MaQgNavigation)
                .Include(d => d.NamPhatHanhNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dsphimLe == null)
            {
                return NotFound();
            }

            return View(dsphimLe);
        }

        // GET: Admin/AdminDsphimLes/Create
        public IActionResult Create()
        {
            
            ViewData["MaQg"] = new SelectList(_context.QuocGia, nameof(QuocGium.MaQg), nameof(QuocGium.TenQg));
            ViewData["NamPhatHanh"] = new SelectList(_context.Nams, nameof(Nam.MaNam), nameof(Nam.TenNam));
            ViewData["TheLoai"] = new SelectList(_context.TheLoais, nameof(TheLoai.IdtheLoai), nameof(TheLoai.TenTheLoai));
            return View();
        }

        // POST: Admin/AdminDsphimLes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenPhim,NoiDung,NamPhatHanh,ThoiLuong,ImageFile,MaQg,LuotXem,Link,TheLoaiPhimLes")] DsphimLe dsphimLe,[Bind("IdphimLe,IdtheLoai")] List<TheLoaiPhimLe> theLoaiPhimLe)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(dsphimLe.ImageFile.FileName);
                string extension = Path.GetExtension(dsphimLe.ImageFile.FileName);
                dsphimLe.Img = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/img/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await dsphimLe.ImageFile.CopyToAsync(fileStream);
                }
                _context.Add(dsphimLe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaQg"] = new SelectList(_context.QuocGia, nameof(QuocGium.MaQg), nameof(QuocGium.TenQg), dsphimLe.MaQg);
            ViewData["NamPhatHanh"] = new SelectList(_context.Nams, nameof(Nam.MaNam), nameof(Nam.TenNam), dsphimLe.NamPhatHanh);
            ViewData["TheLoai"] = new SelectList(_context.TheLoais, nameof(TheLoai.IdtheLoai), nameof(TheLoai.TenTheLoai));
            return View(dsphimLe);
        }

        // GET: Admin/AdminDsphimLes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dsphimLe = await _context.DsphimLes.FindAsync(id);
            if (dsphimLe == null)
            {
                return NotFound();
            }
            ViewData["MaQg"] = new SelectList(_context.QuocGia, nameof(QuocGium.MaQg), nameof(QuocGium.TenQg), dsphimLe.MaQg);
            ViewData["NamPhatHanh"] = new SelectList(_context.Nams, nameof(Nam.MaNam), nameof(Nam.TenNam), dsphimLe.NamPhatHanh);
            ViewData["TheLoai"] = new SelectList(_context.TheLoais, nameof(TheLoai.IdtheLoai), nameof(TheLoai.TenTheLoai));
            return View(dsphimLe);
        }

        // POST: Admin/AdminDsphimLes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenPhim,NoiDung,NamPhatHanh,ThoiLuong,ImageFile,MaQg,LuotXem,Link")] DsphimLe dsphimLe)
        {
            if (id != dsphimLe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(dsphimLe.ImageFile.FileName);
                    string extension = Path.GetExtension(dsphimLe.ImageFile.FileName);
                    dsphimLe.Img = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/img/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await dsphimLe.ImageFile.CopyToAsync(fileStream);
                    }

                    _context.Update(dsphimLe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DsphimLeExists(dsphimLe.Id))
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
            ViewData["MaQg"] = new SelectList(_context.QuocGia, nameof(QuocGium.MaQg), nameof(QuocGium.TenQg), dsphimLe.MaQg);
            ViewData["NamPhatHanh"] = new SelectList(_context.Nams, nameof(Nam.MaNam), nameof(Nam.TenNam), dsphimLe.NamPhatHanh);
            ViewData["TheLoai"] = new SelectList(_context.TheLoais, nameof(TheLoai.IdtheLoai), nameof(TheLoai.TenTheLoai));
            return View(dsphimLe);
        }

        // GET: Admin/AdminDsphimLes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dsphimLe = await _context.DsphimLes
                .Include(d => d.MaQgNavigation)
                .Include(d => d.NamPhatHanhNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dsphimLe == null)
            {
                return NotFound();
            }

            return View(dsphimLe);
        }

        // POST: Admin/AdminDsphimLes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dsphimLe = await _context.DsphimLes.FindAsync(id);
            _context.DsphimLes.Remove(dsphimLe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DsphimLeExists(int id)
        {
            return _context.DsphimLes.Any(e => e.Id == id);
        }
    }
}
