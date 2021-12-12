using JDMovie.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace JDMovie.Controllers
{
    public class PhimBoController : Controller
    {
        private readonly ILogger<PhimBoController> _logger;

        private dbDACNContext db = new dbDACNContext();
        public PhimBoController(ILogger<PhimBoController> logger)
        {
            _logger = logger;

        }

        public IActionResult Index()
        {
            var homephimbo = (from phimbo in db.DsphimBos select phimbo).ToList();
            var homecttapphim = (from ctphim in db.CttapPhims select ctphim).ToList();

            ViewBag.phimbo = homephimbo;
            ViewBag.ctphim = homecttapphim;


            return View();
        }
        public IActionResult Details()
        {
            var homephimbo = (from phimbo in db.DsphimBos select phimbo).ToList();
            var homecttapphim = (from ctphim in db.CttapPhims select ctphim).ToList();

            ViewBag.phimbo = homephimbo;
            ViewBag.ctphim = homecttapphim;

            return View();
        }
    }
}
