using JDMovie.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JDMovie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        
        private dbDACNContext db = new dbDACNContext();
        public IActionResult Index()
        {
            var adminphimle = db.DsphimLes.Include(d => d.MaQgNavigation).Include(c => c.NamPhatHanhNavigation).ToList().Take(5);
            var admintintuc = (from tintuc in db.Tintucphims select tintuc).ToList().Take(5);
            var adminphimbo = (from phimbo in db.DsphimBos select phimbo).Include(p => p.MaQgNavigation).Include(c => c.NamPhatHanhNavigation).ToList().Take(5);
            var admintaikhoan = (from taikhoan in db.TaiKhoans select taikhoan).ToList().Take(5);

            var slphimle = (from phimlesl in db.DsphimLes select phimlesl).Count();
            var sltintuc = (from tintucsl in db.Tintucphims select tintucsl).Count();
            var slphimbo = (from phimbosl in db.DsphimBos select phimbosl).Count();
            var sltaikhoan = (from taikhoansl in db.TaiKhoans select taikhoansl).Count();

            ViewBag.phimle = adminphimle;
            ViewBag.phimbo = adminphimbo;
            ViewBag.tintuc = admintintuc;
            ViewBag.taikhoan = admintaikhoan;

            

            ViewBag.phimlesl = slphimle;
            ViewBag.phimbosl = slphimbo;
            ViewBag.tintucsl = sltintuc;
            ViewBag.taikhoansl = sltaikhoan;
            return View();
        }
    }
}
