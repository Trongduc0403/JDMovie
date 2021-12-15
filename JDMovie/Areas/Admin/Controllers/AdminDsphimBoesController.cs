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
    public class AdminDsphimBoesController : Controller
    {
        private readonly dbDACNContext _context;

        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminDsphimBoesController(dbDACNContext context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Admin/AdminDsphimBoes
        public async Task<IActionResult> Index()
        {

            var dbDACNContext = _context.DsphimBos.Include(d => d.MaQgNavigation).Include(d => d.NamPhatHanhNavigation);
          
            return View(await dbDACNContext.ToListAsync());
        }

        // GET: Admin/AdminDsphimBoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dsphimBo = await _context.DsphimBos
                
                .Include(d => d.MaQgNavigation)
                .Include(d => d.NamPhatHanhNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dsphimBo == null)
            {
                return NotFound();
            }

            return View(dsphimBo);
        }

        // GET: Admin/AdminDsphimBoes/Create
        public IActionResult Create()
        {
            ViewData["IdtheLoai"] = new SelectList(_context.TheLoais, "IdtheLoai", "IdtheLoai");
            ViewData["MaQg"] = new SelectList(_context.QuocGia, nameof(QuocGium.MaQg), nameof(QuocGium.TenQg));
            ViewData["NamPhatHanh"] = new SelectList(_context.Nams, nameof(Nam.MaNam), nameof(Nam.TenNam));
            return View();
        }

        // POST: Admin/AdminDsphimBoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenPhim,NoiDung,NamPhatHanh,IdtheLoai,ThoiLuong,ImageFile,MaQg,LuotXem")] DsphimBo dsphimBo)
        {
            if (ModelState.IsValid)
            {
                //Save image to wwwroot/img
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(dsphimBo.ImageFile.FileName);
                string extension = Path.GetExtension(dsphimBo.ImageFile.FileName);
                dsphimBo.Img = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/img/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await dsphimBo.ImageFile.CopyToAsync(fileStream);
                }

                _context.Add(dsphimBo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["MaQg"] = new SelectList(_context.QuocGia, nameof(QuocGium.MaQg), nameof(QuocGium.TenQg), dsphimBo.MaQg);
            ViewData["NamPhatHanh"] = new SelectList(_context.Nams, nameof(Nam.MaNam), nameof(Nam.TenNam), dsphimBo.NamPhatHanh);
            return View(dsphimBo);
        }

        // GET: Admin/AdminDsphimBoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dsphimBo = await _context.DsphimBos.FindAsync(id);
            if (dsphimBo == null)
            {
                return NotFound();
            }

            ViewData["MaQg"] = new SelectList(_context.QuocGia, nameof(QuocGium.MaQg), nameof(QuocGium.TenQg), dsphimBo.MaQg);
            ViewData["NamPhatHanh"] = new SelectList(_context.Nams, nameof(Nam.MaNam), nameof(Nam.TenNam), dsphimBo.NamPhatHanh);
            return View(dsphimBo);
        }

        // POST: Admin/AdminDsphimBoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenPhim,NoiDung,NamPhatHanh,IdtheLoai,ThoiLuong,ImageFile,MaQg,LuotXem")] DsphimBo dsphimBo)
        {
            if (id != dsphimBo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(dsphimBo.ImageFile.FileName);
                    string extension = Path.GetExtension(dsphimBo.ImageFile.FileName);
                    dsphimBo.Img = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/img/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await dsphimBo.ImageFile.CopyToAsync(fileStream);
                    }
                    _context.Update(dsphimBo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DsphimBoExists(dsphimBo.Id))
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

            ViewData["MaQg"] = new SelectList(_context.QuocGia, nameof(QuocGium.MaQg), nameof(QuocGium.TenQg), dsphimBo.MaQg);
            ViewData["NamPhatHanh"] = new SelectList(_context.Nams, nameof(Nam.MaNam), nameof(Nam.TenNam), dsphimBo.NamPhatHanh);
            return View(dsphimBo);
        }

        // GET: Admin/AdminDsphimBoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dsphimBo = await _context.DsphimBos
               
                .Include(d => d.MaQgNavigation)
                .Include(d => d.NamPhatHanhNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dsphimBo == null)
            {
                return NotFound();
            }

            return View(dsphimBo);
        }

        // POST: Admin/AdminDsphimBoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dsphimBo = await _context.DsphimBos.FindAsync(id);
            _context.DsphimBos.Remove(dsphimBo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DsphimBoExists(int id)
        {
            return _context.DsphimBos.Any(e => e.Id == id);
        }
    }
}
