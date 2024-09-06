using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsApp.Models;

namespace NewsApp.Controllers
{
    public class CategoriesController : Controller
    {
        NewsDBEntities db = new NewsDBEntities();
        // GET: Category
        public ActionResult Index()
        {
            ViewBag.QuangCao = db.QuangCaos.ToList();

            return View();
        }

        public ActionResult CategoryPartial()
        {
            ViewBag.CategoryList = db.DanhMucs.ToList();
            return PartialView();
        }
        public ActionResult GetNewsCategories(int idCate)
        {
            ViewBag.CateName = db.DanhMucs.FirstOrDefault(c => c.IDDanhMuc == idCate).TenDanhMuc;  
            ViewBag.News = db.TinTucs.Where(n => n.IDDanhMuc == idCate).ToList();
            ViewBag.QuangCao = db.QuangCaos.ToList();

            return View();
        }
        public ActionResult SearchNews(string searchKey)
        {
            ViewBag.SearchKey = searchKey;
            ViewBag.SearchResult = db.TinTucs.Where(n => n.TieuDe.Contains(searchKey) || n.MoTa.Contains(searchKey)).ToList();
            ViewBag.QuangCao = db.QuangCaos.ToList();

            return View();
        }
        public ActionResult GetNewsToWeek()
        {
            DateTime today = DateTime.Today;

            DateTime firstDayOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime lastDayOfWeek = firstDayOfWeek.AddDays(6);

            ViewBag.ToWeek = db.TinTucs.Where(n => n.NgayDang >= firstDayOfWeek && n.NgayDang <= lastDayOfWeek).ToList();
            ViewBag.ToWeek.Reverse();
            ViewBag.QuangCao = db.QuangCaos.ToList();

            return View();
        }
    }
}