using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsApp.Models;
namespace NewsApp.Controllers
{
    public class HomeController : Controller
    {
        NewsDBEntities db = new NewsDBEntities();
        public ActionResult Index()
        {

            ViewBag.TinNoiBat = db.TinTucs.Where(n => n.NoiBat == true).ToList();

            DateTime today = DateTime.Today;
            DateTime firstDayOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime lastDayOfWeek = firstDayOfWeek.AddDays(6);
            ViewBag.ToWeek = db.TinTucs.Where(n =>  n.NgayDang >= firstDayOfWeek && n.NgayDang <= lastDayOfWeek).ToList().Take(4);
            //ViewBag.ToWeek.Reverse();


            var categoryLength = db.DanhMucs.Count();
            Random rand = new Random();
            int categoryIndex = rand.Next(categoryLength);
            var categoryID = db.DanhMucs.ToList()[categoryIndex].IDDanhMuc;
            
            while(db.TinTucs.Where(n=> n.IDDanhMuc == categoryID).ToList().Count()<= 0)
            {
                categoryIndex = rand.Next(categoryLength);
                categoryID = db.DanhMucs.ToList()[categoryIndex].IDDanhMuc;
            }
            ViewBag.CategoryName = db.DanhMucs.ToList()[categoryIndex].TenDanhMuc;
            ViewBag.DanhMuc = db.TinTucs.Where(n => n.IDDanhMuc == categoryID).ToList();
            ViewBag.DanhMucID = categoryID;

            ViewBag.Interesting = db.TinTucs.ToList().Take(4).Reverse();

            ViewBag.QuangCao = db.QuangCaos.ToList();
            

            return View();
        }
       
    }
}