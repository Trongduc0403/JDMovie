using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JDMovie.Models;
using JDMovie.Areas.Admin.Models.ViewModels;
using JDMovie.Helper;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace JDMovie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminDsphimLesController : Controller
    {
        private readonly dbDACNContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public INotyfService _notyfService { get; }

        public AdminDsphimLesController(dbDACNContext context, IWebHostEnvironment hostEnvironment, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
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
            ViewData["IdphimLe"] = new SelectList(_context.DsphimLes, nameof(DsphimLe.Id), nameof(DsphimLe.TenPhim));
            ViewData["IdtheLoai"] = new SelectList(_context.TheLoais, nameof(TheLoai.IdtheLoai), nameof(TheLoai.TenTheLoai));
            return View();
        }

        // POST: Admin/AdminDsphimLes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenPhim,NoiDung,NamPhatHanh,ThoiLuong,ImageFile,MaQg,LuotXem,Link,IdphimLe,IdtheLoai")] PhimLeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                    string extension = Path.GetExtension(model.ImageFile.FileName);
                    model.Img = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/img/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(fileStream);
                    }

                

                    var lastphim = (from DSPhimLe in _context.DsphimLes
                                    orderby DSPhimLe.Id descending
                                    select DSPhimLe).FirstOrDefault();



                    DsphimLe dsphimLe = new DsphimLe();
                    dsphimLe.Id = lastphim.Id + 1;
                    dsphimLe.TenPhim = model.TenPhim;
                    dsphimLe.NoiDung = model.NoiDung;
                    dsphimLe.NamPhatHanh = model.NamPhatHanh;
                    dsphimLe.ThoiLuong = model.ThoiLuong;
                    dsphimLe.Img = model.Img;
                    dsphimLe.MaQg = model.MaQg;
                    dsphimLe.Link = model.Link;

                    _context.Add(dsphimLe);


                    TheLoaiPhimLe theloai = new TheLoaiPhimLe();
                    theloai.IdtheLoai = model.IdtheLoai;
                    theloai.IdphimLe = dsphimLe.Id;

                    _context.Add(theloai);

                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.DSPhimLe ON;");
                    await _context.SaveChangesAsync();
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.DSPhimLe OFF;");
                    transaction.Commit();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["MaQg"] = new SelectList(_context.QuocGia, nameof(QuocGium.MaQg), nameof(QuocGium.TenQg), model.MaQg);
            ViewData["NamPhatHanh"] = new SelectList(_context.Nams, nameof(Nam.MaNam), nameof(Nam.TenNam), model.NamPhatHanh);
            ViewData["IdphimLe"] = new SelectList(_context.DsphimLes, nameof(DsphimLe.Id), nameof(DsphimLe.TenPhim), model.IdphimLe);
            ViewData["IdtheLoai"] = new SelectList(_context.TheLoais, nameof(TheLoai.IdtheLoai), nameof(TheLoai.TenTheLoai), model.IdtheLoai);
            return View(model);
        }

        // GET: Admin/AdminDsphimLes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _notyfService.Information("Vui lòng chọn lại hình ảnh !");

            var dsphimLe = await _context.DsphimLes.FindAsync(id);
            PhimLeViewModel model = new PhimLeViewModel();
            model.TenPhim = dsphimLe.TenPhim;
            model.NoiDung = dsphimLe.NoiDung;
            model.NamPhatHanh = (int)dsphimLe.NamPhatHanh;
            model.ThoiLuong = dsphimLe.ThoiLuong;
            model.Img = dsphimLe.Img;
            model.MaQg = (int)dsphimLe.MaQg;
            model.Link = dsphimLe.Link;
            model.IdphimLe = (int)id;


            var theloaip = (from tl in _context.TheLoaiPhimLes
                            where tl.IdphimLe == id
                            select tl).Include(t => t.IdphimLeNavigation).ToList();

            ViewBag.theloaip = theloaip;



            if (dsphimLe == null)
            {
                return NotFound();
            }
            ViewData["MaQg"] = new SelectList(_context.QuocGia, nameof(QuocGium.MaQg), nameof(QuocGium.TenQg), dsphimLe.MaQg);
            ViewData["NamPhatHanh"] = new SelectList(_context.Nams, nameof(Nam.MaNam), nameof(Nam.TenNam), dsphimLe.NamPhatHanh);
            ViewData["IdphimLe"] = new SelectList(_context.DsphimLes, nameof(DsphimLe.Id), nameof(DsphimLe.TenPhim));
            ViewData["IdtheLoai"] = new SelectList(_context.TheLoais, nameof(TheLoai.IdtheLoai), nameof(TheLoai.TenTheLoai));
            return View(model);
        }

        // POST: Admin/AdminDsphimLes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenPhim,NoiDung,NamPhatHanh,ThoiLuong,ImageFile,MaQg,LuotXem,Link,IdphimLe,IdtheLoai")] PhimLeViewModel model)
        {
            if (id != model.IdphimLe)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    try
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                        string extension = Path.GetExtension(model.ImageFile.FileName);
                        model.Img = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/img/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await model.ImageFile.CopyToAsync(fileStream);
                        }
                    }catch (Exception ex)
                    {
                        _notyfService.Error("Vui lòng chọn lại hình ảnh !");
                        ViewData["MaQg"] = new SelectList(_context.QuocGia, nameof(QuocGium.MaQg), nameof(QuocGium.TenQg), model.MaQg);
                        ViewData["NamPhatHanh"] = new SelectList(_context.Nams, nameof(Nam.MaNam), nameof(Nam.TenNam), model.NamPhatHanh);
                        ViewData["IdphimLe"] = new SelectList(_context.DsphimLes, nameof(DsphimLe.Id), nameof(DsphimLe.TenPhim));
                        ViewData["IdtheLoai"] = new SelectList(_context.TheLoais, nameof(TheLoai.IdtheLoai), nameof(TheLoai.TenTheLoai));
                        var theloaipd = (from tl in _context.TheLoaiPhimLes
                                        where tl.IdphimLe == id
                                        select tl).Include(t => t.IdphimLeNavigation).ToList();

                        ViewBag.theloaip = theloaipd;
                        return View(model);
                    }



                    DsphimLe dsphimLe = new DsphimLe();
                    dsphimLe.Id = id;
                    dsphimLe.TenPhim = model.TenPhim;
                    dsphimLe.NoiDung = model.NoiDung;
                    dsphimLe.NamPhatHanh = model.NamPhatHanh;
                    dsphimLe.ThoiLuong = model.ThoiLuong;
                    dsphimLe.Img = model.Img;
                    dsphimLe.MaQg = model.MaQg;
                    dsphimLe.Link = model.Link;

                    if (dsphimLe.Img == null)
                    {
                        _notyfService.Error("Vui lòng chọn hình ảnh !");
                        ViewData["MaQg"] = new SelectList(_context.QuocGia, nameof(QuocGium.MaQg), nameof(QuocGium.TenQg), model.MaQg);
                        ViewData["NamPhatHanh"] = new SelectList(_context.Nams, nameof(Nam.MaNam), nameof(Nam.TenNam), model.NamPhatHanh);
                        ViewData["IdphimLe"] = new SelectList(_context.DsphimLes, nameof(DsphimLe.Id), nameof(DsphimLe.TenPhim));
                        ViewData["IdtheLoai"] = new SelectList(_context.TheLoais, nameof(TheLoai.IdtheLoai), nameof(TheLoai.TenTheLoai));
                        var theloaiph = (from tl in _context.TheLoaiPhimLes
                                        where tl.IdphimLe == id
                                        select tl).Include(t => t.IdphimLeNavigation).ToList();

                        ViewBag.theloaip = theloaiph;
                        return View(model);
                    }



                    _context.Update(dsphimLe);

                    TheLoaiPhimLe theloai = new TheLoaiPhimLe();
                    theloai.IdphimLe = id;
                    theloai.IdtheLoai = model.IdtheLoai;
                    _context.Add(theloai);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DsphimLeExists(model.IdphimLe))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    _notyfService.Error("Thể loại đã tồn tại !");
                    _notyfService.Information("Vui lòng chọn lại hình ảnh !");
                    
                    ViewData["MaQg"] = new SelectList(_context.QuocGia, nameof(QuocGium.MaQg), nameof(QuocGium.TenQg), model.MaQg);
                    ViewData["NamPhatHanh"] = new SelectList(_context.Nams, nameof(Nam.MaNam), nameof(Nam.TenNam), model.NamPhatHanh);
                    ViewData["IdphimLe"] = new SelectList(_context.DsphimLes, nameof(DsphimLe.Id), nameof(DsphimLe.TenPhim));
                    ViewData["IdtheLoai"] = new SelectList(_context.TheLoais, nameof(TheLoai.IdtheLoai), nameof(TheLoai.TenTheLoai));
                    var theloaipt = (from tl in _context.TheLoaiPhimLes
                                    where tl.IdphimLe == id
                                    select tl).Include(t => t.IdphimLeNavigation).ToList();

                    ViewBag.theloaip = theloaipt;
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaQg"] = new SelectList(_context.QuocGia, nameof(QuocGium.MaQg), nameof(QuocGium.TenQg), model.MaQg);
            ViewData["NamPhatHanh"] = new SelectList(_context.Nams, nameof(Nam.MaNam), nameof(Nam.TenNam), model.NamPhatHanh);
            ViewData["IdphimLe"] = new SelectList(_context.DsphimLes, nameof(DsphimLe.Id), nameof(DsphimLe.TenPhim));
            ViewData["IdtheLoai"] = new SelectList(_context.TheLoais, nameof(TheLoai.IdtheLoai), nameof(TheLoai.TenTheLoai));
            var theloaip = (from tl in _context.TheLoaiPhimLes
                            where tl.IdphimLe == id
                            select tl).Include(t => t.IdphimLeNavigation).ToList();

            ViewBag.theloaip = theloaip;

            return View(model);
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


            var queryTheLoaiPhimLe = from TheLoaiPhimLe in _context.TheLoaiPhimLes
                                    where TheLoaiPhimLe.IdphimLe == id
                                    select TheLoaiPhimLe;
            foreach (var del in queryTheLoaiPhimLe)
            {
                _context.TheLoaiPhimLes.Remove(del);
            }

            _context.DsphimLes.Remove(dsphimLe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DsphimLeExists(int id)
        {
            return _context.DsphimLes.Any(e => e.Id == id);
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddTheLoai(int id, [Bind("IdtheLoai")] PhimLeViewModel mode)
        //{
        //    if (!ModelState.IsValid)
        //    {



        //        _context.Add(theLoaiPhimLe);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewData["IdphimLe"] = new SelectList(_context.DsphimLes, nameof(DsphimLe.Id), nameof(DsphimLe.TenPhim), mode.IdphimLe);
        //    ViewData["IdtheLoai"] = new SelectList(_context.TheLoais, nameof(TheLoai.IdtheLoai), nameof(TheLoai.TenTheLoai), mode.IdtheLoai);
        //    return View(theLoaiPhimLe);
        //}
    }
}
