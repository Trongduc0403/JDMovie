using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JDMovie.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using JDMovie.Areas.Admin.Models.ViewModels;

namespace JDMovie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminDsphimBoesController : Controller
    {
        private readonly dbDACNContext _context;

        private readonly IWebHostEnvironment _hostEnvironment;
        public INotyfService _notyfService { get; }

        public AdminDsphimBoesController(dbDACNContext context,IWebHostEnvironment hostEnvironment, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
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
            ViewData["MaQg"] = new SelectList(_context.QuocGia, nameof(QuocGium.MaQg), nameof(QuocGium.TenQg));
            ViewData["NamPhatHanh"] = new SelectList(_context.Nams, nameof(Nam.MaNam), nameof(Nam.TenNam));
            ViewData["IdphimBo"] = new SelectList(_context.DsphimBos, nameof(DsphimBo.Id), nameof(DsphimBo.TenPhim));
            ViewData["IdtheLoai"] = new SelectList(_context.TheLoais, nameof(TheLoai.IdtheLoai), nameof(TheLoai.TenTheLoai));
            return View();
        }

        // POST: Admin/AdminDsphimBoes/Create
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

                    //Save image to wwwroot/img
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                    string extension = Path.GetExtension(model.ImageFile.FileName);
                    model.Img = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/img/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(fileStream);
                    }


                    var lastphim = (from DsphimBo in _context.DsphimBos
                                    orderby DsphimBo.Id descending
                                    select DsphimBo).FirstOrDefault();

                    DsphimBo dsphimBo = new DsphimBo();
                    dsphimBo.Id = lastphim.Id + 1;
                    dsphimBo.TenPhim = model.TenPhim;
                    dsphimBo.NoiDung = model.NoiDung;
                    dsphimBo.NamPhatHanh = model.NamPhatHanh;
                    dsphimBo.ThoiLuong = model.ThoiLuong;
                    dsphimBo.Img = model.Img;
                    dsphimBo.MaQg = model.MaQg;

                    _context.Add(dsphimBo);

                    TheLoaiPhimBo theloai = new TheLoaiPhimBo();
                    theloai.IdtheLoai = model.IdtheLoai;
                    theloai.IdphimBo = dsphimBo.Id;

                    _context.Add(theloai);

                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.DSPhimBo ON;");
                    await _context.SaveChangesAsync();
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.DSPhimBo OFF;");
                    transaction.Commit();
                    return RedirectToAction(nameof(Index));
                }    
            }
            
            ViewData["MaQg"] = new SelectList(_context.QuocGia, nameof(QuocGium.MaQg), nameof(QuocGium.TenQg), model.MaQg);
            ViewData["NamPhatHanh"] = new SelectList(_context.Nams, nameof(Nam.MaNam), nameof(Nam.TenNam), model.NamPhatHanh);
            ViewData["IdphimBo"] = new SelectList(_context.DsphimBos, nameof(DsphimBo.Id), nameof(DsphimBo.TenPhim), model.IdphimLe);
            ViewData["IdtheLoai"] = new SelectList(_context.TheLoais, nameof(TheLoai.IdtheLoai), nameof(TheLoai.TenTheLoai), model.IdtheLoai);
            return View(model);
        }

        // GET: Admin/AdminDsphimBoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _notyfService.Information("Vui lòng chọn lại hình ảnh !");

            var dsphimbo = await _context.DsphimBos.FindAsync(id);
            PhimLeViewModel model = new PhimLeViewModel();
            model.TenPhim = dsphimbo.TenPhim;
            model.NoiDung = dsphimbo.NoiDung;
            model.NamPhatHanh = (int)dsphimbo.NamPhatHanh;
            model.ThoiLuong = dsphimbo.ThoiLuong;
            model.Img = dsphimbo.Img;
            model.MaQg = (int)dsphimbo.MaQg;
            model.IdphimLe = (int)id;

            var theloaip = (from tl in _context.TheLoaiPhimBos
                            where tl.IdphimBo == id
                            select tl).Include(t => t.IdphimBoNavigation).ToList();

            ViewBag.theloaip = theloaip;


            if (dsphimbo == null)
            {
                return NotFound();
            }

            ViewData["MaQg"] = new SelectList(_context.QuocGia, nameof(QuocGium.MaQg), nameof(QuocGium.TenQg), dsphimbo.MaQg);
            ViewData["NamPhatHanh"] = new SelectList(_context.Nams, nameof(Nam.MaNam), nameof(Nam.TenNam), dsphimbo.NamPhatHanh);
            ViewData["IdphimBo"] = new SelectList(_context.DsphimBos, nameof(DsphimBo.Id), nameof(DsphimBo.TenPhim), model.IdphimLe);
            ViewData["IdtheLoai"] = new SelectList(_context.TheLoais, nameof(TheLoai.IdtheLoai), nameof(TheLoai.TenTheLoai), model.IdtheLoai);
            return View(model);
        }

        // POST: Admin/AdminDsphimBoes/Edit/5
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
                        ViewData["IdphimBo"] = new SelectList(_context.DsphimLes, nameof(DsphimBo.Id), nameof(DsphimBo.TenPhim));
                        ViewData["IdtheLoai"] = new SelectList(_context.TheLoais, nameof(TheLoai.IdtheLoai), nameof(TheLoai.TenTheLoai));
                        var theloaip = (from tl in _context.TheLoaiPhimBos
                                        where tl.IdphimBo == id
                                        select tl).Include(t => t.IdphimBoNavigation).ToList();

                        ViewBag.theloaip = theloaip;
                        return View(model);
                    }



                    DsphimBo dsphimBo = new DsphimBo();
                    dsphimBo.Id = id;
                    dsphimBo.TenPhim = model.TenPhim;
                    dsphimBo.NoiDung = model.NoiDung;
                    dsphimBo.NamPhatHanh = model.NamPhatHanh;
                    dsphimBo.ThoiLuong = model.ThoiLuong;
                    dsphimBo.Img = model.Img;
                    dsphimBo.MaQg = model.MaQg;

                    if (dsphimBo.Img == null)
                    {
                        _notyfService.Error("Vui lòng chọn hình ảnh !");
                        ViewData["MaQg"] = new SelectList(_context.QuocGia, nameof(QuocGium.MaQg), nameof(QuocGium.TenQg), model.MaQg);
                        ViewData["NamPhatHanh"] = new SelectList(_context.Nams, nameof(Nam.MaNam), nameof(Nam.TenNam), model.NamPhatHanh);
                        ViewData["IdphimBo"] = new SelectList(_context.DsphimLes, nameof(DsphimBo.Id), nameof(DsphimBo.TenPhim));
                        ViewData["IdtheLoai"] = new SelectList(_context.TheLoais, nameof(TheLoai.IdtheLoai), nameof(TheLoai.TenTheLoai));
                        var theloaiph = (from tl in _context.TheLoaiPhimBos
                                        where tl.IdphimBo == id
                                        select tl).Include(t => t.IdphimBoNavigation).ToList();

                        ViewBag.theloaip = theloaiph;
                        return View(model);
                    }


                    _context.Update(dsphimBo);

                    TheLoaiPhimBo theloai = new TheLoaiPhimBo();
                    theloai.IdphimBo = id;
                    theloai.IdtheLoai = model.IdtheLoai;

                    _context.Add(theloai);


                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DsphimBoExists(model.IdphimLe))
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
                    ViewData["IdphimBo"] = new SelectList(_context.DsphimLes, nameof(DsphimBo.Id), nameof(DsphimBo.TenPhim));
                    ViewData["IdtheLoai"] = new SelectList(_context.TheLoais, nameof(TheLoai.IdtheLoai), nameof(TheLoai.TenTheLoai));
                    var theloaipt = (from tl in _context.TheLoaiPhimBos
                                    where tl.IdphimBo == id
                                    select tl).Include(t => t.IdphimBoNavigation).ToList();

                    ViewBag.theloaip = theloaipt;
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaQg"] = new SelectList(_context.QuocGia, nameof(QuocGium.MaQg), nameof(QuocGium.TenQg), model.MaQg);
            ViewData["NamPhatHanh"] = new SelectList(_context.Nams, nameof(Nam.MaNam), nameof(Nam.TenNam), model.NamPhatHanh);
            ViewData["IdphimBo"] = new SelectList(_context.DsphimLes, nameof(DsphimBo.Id), nameof(DsphimBo.TenPhim));
            ViewData["IdtheLoai"] = new SelectList(_context.TheLoais, nameof(TheLoai.IdtheLoai), nameof(TheLoai.TenTheLoai));
            var theloaipm = (from tl in _context.TheLoaiPhimBos
                            where tl.IdphimBo == id
                            select tl).Include(t => t.IdphimBoNavigation).ToList();

            ViewBag.theloaip = theloaipm;
            return View(model);
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

            var queryTheLoaiPhimBo = from TheLoaiPhimBo in _context.TheLoaiPhimBos
                                     where TheLoaiPhimBo.IdphimBo == id
                                     select TheLoaiPhimBo;
            foreach (var del in queryTheLoaiPhimBo)
            {
                _context.TheLoaiPhimBos.Remove(del);
            }

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

