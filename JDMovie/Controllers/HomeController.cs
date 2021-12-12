using JDMovie.Helper;
using JDMovie.Models;
using JDMovie.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace JDMovie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private dbDACNContext db = new dbDACNContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
        }

        public IActionResult Index()
        {
            var homephimle = (from phimle in db.DsphimLes select phimle ).ToList();
            var homephimbo = (from phimbo in db.DsphimBos select phimbo).ToList();
            var hometintuc = (from tintuc in db.Tintucphims select tintuc).ToList();

            
            ViewBag.phim = homephimle;
            ViewBag.tintuc = hometintuc;
            ViewBag.phimbo = homephimbo;
            
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Search(string searchphim)
        {
            var phim = (from d in db.DsphimBos
                       where
                         d.TenPhim.Contains(searchphim)
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
                          d0.TenPhim.Contains(searchphim)
                        select new
                        {
                            TenPhim = d0.TenPhim,
                            Img = d0.Img,
                            Id = d0.Id,
                            controller = "DsPhimLes"
                        }
                    )
                    .ToList();

            ViewBag.phimhome = phim.ToDynamicList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}