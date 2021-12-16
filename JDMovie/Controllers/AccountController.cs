using JDMovie.Models;
using JDMovie.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace JDMovie.Controllers
{
    public class AccountController : Controller
    {
        string appid = string.Empty;
        string appsecret = string.Empty;

        public AccountController()
        {
            var configuration = GetConfiguration();
            appid = configuration.GetSection("AppID").Value;
            appsecret = configuration.GetSection("AppSecret").Value;
        }

        public IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Headers["Referer"].ToString());
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        public IActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginurl = fb.GetLoginUrl(new
            {
                client_id = appid,
                client_secret = appsecret,
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });
            return Redirect(loginurl.AbsoluteUri);
        }

        public IActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = appid,
                client_secret = appsecret,
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });
            var accesstoken = result.access_token;
            fb.AccessToken = accesstoken;
            dynamic data = fb.Get("me?fields=link,first_name,currency,last_name,email,gender,locale,timezone,verified,picture,age_range");
            TempData["email"] = data.email;
            TempData["name"] = data.first_name + " " + data.last_name;
            TempData["picture"] = data.picture.data.url;
            return RedirectToAction("Index", "Home");


        }

        private dbDACNContext db = new dbDACNContext();

        public static TaiKhoan TaiKhoanDangNhap = new TaiKhoan();

        public IActionResult Dangnhap()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var data = db.TaiKhoans.Where(s => s.Email.Equals(user.Email) && (s.MatKhau).Equals(GetMD5(user.Password))).ToList();
                if (data.Count() > 0)
                {

                    HttpContext.Session.SetString("Email", data.FirstOrDefault().Email);
                    HttpContext.Session.SetString("HoTen", data.FirstOrDefault().HoTen);
                    HttpContext.Session.SetString("MatKhau", data.FirstOrDefault().MatKhau);
                    HttpContext.Session.SetString("Quyen", data.FirstOrDefault().Quyen.ToString());

                    //TempData["Email"] = data.FirstOrDefault().Email;
                    //TempData["HoTen"] = data.FirstOrDefault().HoTen;
                    //TempData["MatKhau"] = data.FirstOrDefault().MatKhau;
                    //TempData["Quyen"] = data.FirstOrDefault().Quyen;


                    if (HttpContext.Session.GetString("Quyen").ToString() == "False")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    if(HttpContext.Session.GetString("Quyen").ToString() == "True")
                    {
                        return RedirectToAction("Home", "Admin");
                    }

                }
                else
                {
                    ModelState.AddModelError("password", "Sai tên đăng nhập hoặc mật khẩu");
                    return View(user);
                }
            }
            return View(user);
        }

        [AllowAnonymous]
        public IActionResult Dangky()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Dangky(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TaiKhoan nameAlreadyExists = _context.TaiKhoans.Find(model.TenTaiKhoan);
                var getac = (from ac in db.TaiKhoans
                             where ac.Email == model.Email
                             select ac).FirstOrDefault();

                if (getac != null)
                {
                    ModelState.AddModelError("Email", "Email này đã được sử dụng");

                    return View(model);
                }


                TaiKhoan u1 = new TaiKhoan();
                u1.Email = model.Email;
                u1.HoTen = model.Name;
                u1.MatKhau = GetMD5(model.Password);
                u1.Quyen = false;
                db.Add(u1);
                await db.SaveChangesAsync();
                return RedirectToAction("DangNhap", "Account");

            }
            return View(model);
        }
        public ActionResult Logoff()
        {
            HttpContext.Session.Clear();//remove session
            TempData.Clear();
            return RedirectToAction("Index", "Home");
        }

        public static string GetMD5(string str)
        {

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            StringBuilder sbHash = new StringBuilder();

            foreach (byte b in bHash)
            {

                sbHash.Append(String.Format("{0:x2}", b));

            }

            return sbHash.ToString();

        }


    }
}