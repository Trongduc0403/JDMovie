using JDMovie.Helper;
using JDMovie.Models;
using Microsoft.AspNetCore.Mvc;

namespace JDMovie.Controllers
{
    public class TimKiemController : Controller
    {
        private dbDACNContext db = new dbDACNContext();
        public IActionResult Nam(int id)
        {
            var phim = (from d in db.DsphimBos
                        where
                          d.NamPhatHanh == id
                        select new
                        {
                            d.TenPhim,
                            d.Img,
                            d.Id,
                            controller = "DsPhimBoes"
                        }
                    ).Concat
                    (
                        from d0 in db.DsphimLes
                        where
                          d0.NamPhatHanh == id
                        select new
                        {
                            TenPhim = d0.TenPhim,
                            Img = d0.Img,
                            Id = d0.Id,
                            controller = "DsPhimLes"
                        }
                    )
                    .ToList();

            ViewBag.phimnam = phim.ToDynamicList();

            return View();
        }

        public IActionResult QuocGia(int id)
        {
            var phim = (from d in db.DsphimBos
                        where d.MaQg == id
                        select new
                        {
                            d.TenPhim,
                            d.Img,
                            d.Id,
                            controller = "DsPhimBoes"
                        }
                    ).Concat
                    (
                        from d0 in db.DsphimLes
                        where d0.MaQg == id
                        select new
                        {
                            TenPhim = d0.TenPhim,
                            Img = d0.Img,
                            Id = d0.Id,
                            controller = "DsPhimLes"
                        }
                    ).ToList();

            ViewBag.phimqg = phim.ToDynamicList();

            return View();
        }

        public IActionResult TheLoai(int id)
        {
            var phim = (from s in db.TheLoaiPhimBos
                        where s.IdtheLoai == id
                        select new
                        {
                            TenPhim =  s.IdphimBoNavigation.TenPhim,
                            Img = s.IdphimBoNavigation.Img,
                            Id = s.IdphimBoNavigation.Id,
                            controller = "DsPhimBoes"
                        }
                        ).Concat
                        (
                            from s0 in db.TheLoaiPhimLes
                            where s0.IdtheLoai == id
                            select new
                            {
                                s0.IdphimLeNavigation.TenPhim,
                                s0.IdphimLeNavigation.Img,
                                s0.IdphimLeNavigation.Id,
                                controller = "DsPhimLes"
                            }
                        ).ToList();

            ViewBag.phimtl = phim.ToDynamicList();
            return View();
        }


    }
}
