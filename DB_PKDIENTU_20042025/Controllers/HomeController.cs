using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq.Mapping;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using DB_PKDIENTU_20042025.Models;

namespace DB_PKDIENTU_20042025.Controllers
{
    public class HomeController : Controller
    {
        DB_PKDIENTU_20042025DataContext dl = new DB_PKDIENTU_20042025DataContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            //var id = c["txtidUser"];
            //var pass = c["txtPass"];
            //ViewBag.Message = "Đăng nhập.";
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection c)
        {

            var username = c["txt_username"];
            var password = c["txt_password"];
            if (Session["kh"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            TempData["Username"] = username; // Giữ lại username
            // 1. Kiểm tra rỗng
            if (string.IsNullOrWhiteSpace(username))
            {
                TempData["Error"] = "Vui lòng nhập tên tài khoản";
                TempData["Focus"] = "txt_username";
                return RedirectToAction("Login", "Home");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                TempData["Error"] = "Vui lòng nhập mật khẩu";
                TempData["Focus"] = "txt_password";
                return RedirectToAction("Login", "Home");
            }
            Sys_user acc = dl.Sys_users.FirstOrDefault(tk => tk.ma_user == username && tk.password == password);

            if (acc == null)
            {
                TempData["Error"] = "Tên tài khoản hoặc mật khẩu không đúng";
                TempData["Focus"] = "txt_username";
                return RedirectToAction("Login", "Home");
            }
            // 3. Kiểm tra status
            if (acc.status != 1)
            {
                TempData["Error"] = "Tài khoản của bạn đang bị khóa";
                TempData["Focus"] = "txt_username";
                return RedirectToAction("Login", "Home");
            }

            // 4. Thành công
            Session["kh"] = acc;
            if (acc.admin == 1)
                return RedirectToAction("Contact", "Home");
            else
                return RedirectToAction("Index", "NhanVien");

        }
        //ok

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}