using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsApp.Models;

namespace NewsApp.Controllers
{
    public class LoginRegisterController : Controller
    {
        NewsDBEntities db = new NewsDBEntities();
        // GET: LoginRegister
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.ThongBao = "";
            return View();
        }
        [HttpPost]
        public ActionResult Login(NguoiDung nguoidung)
        {
            if (ModelState.IsValid)
            {
                var adminAccount = db.AdminAccounts.FirstOrDefault(k => k.Email == nguoidung.Email && k.Password == nguoidung.MatKhau);

                if (adminAccount != null)
                {
                    Session["Account"] = adminAccount;
                    return RedirectToAction("Index", "AdminAreas/TinTucs");
                }
                var account = db.NguoiDungs.FirstOrDefault(k => k.Email == nguoidung.Email && k.MatKhau == nguoidung.MatKhau);
                if (account != null)
                {
                    Session["Account"] = account;
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.ThongBao = "*Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.ErrorRegister = "";
            return View();
        }

        [HttpPost]
        public ActionResult Register(NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                var check = db.NguoiDungs.Where(s => s.Email == nguoiDung.Email).FirstOrDefault();
                if (check == null)
                {
                    if(nguoiDung.MatKhauXacNhan == nguoiDung.MatKhau)
                    {
                        nguoiDung.NgayDangKy = DateTime.Now;
                        db.NguoiDungs.Add(nguoiDung);
                        db.SaveChanges();
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.ErrorRegister = "Mật khẩu xác thực không đúng";
                    }

                }
                else
                {
                    ViewBag.ErrorRegister = "Tên tài khoản đã tồn tại";
                }
            }

            return View();
            
        }

        public ActionResult LogOut()
        {
            Session["Account"] = null;
            return RedirectToAction("Login","LoginRegister");
        }

    }
}