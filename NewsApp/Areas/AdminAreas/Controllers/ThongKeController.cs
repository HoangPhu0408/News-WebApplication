using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsApp.Models;

namespace NewsApp.Areas.AdminAreas.Controllers
{
    public class ThongKeController : Controller
    {
        NewsDBEntities db = new NewsDBEntities();
        // GET: AdminAreas/ThongKe
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.TinTuc = db.TinTucs.ToList();
            ViewBag.TinTuc.Reverse();

            var maxLuotXemTinTuc = db.TinTucs.FirstOrDefault();

            foreach (var item in ViewBag.TinTuc)
            {
                if(item.LuotXem > maxLuotXemTinTuc.LuotXem)
                {
                    maxLuotXemTinTuc = item; 
                }
            }
            ViewBag.MaxLuotXem = maxLuotXemTinTuc;



            var maxBinhLuanTinTuc = db.TinTucs.FirstOrDefault();
            foreach (var item in ViewBag.TinTuc)
            {
                if (item.GetBinhLuanCount() > maxBinhLuanTinTuc.GetBinhLuanCount())
                {
                    maxBinhLuanTinTuc = item;
                }
            }
            ViewBag.MaxBinhLuan = maxBinhLuanTinTuc;
            ViewBag.ThongBao = "";

            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection dayForm)
        {
            var maxLuotXemTinTuc = db.TinTucs.FirstOrDefault();
            var maxBinhLuanTinTuc = db.TinTucs.FirstOrDefault();

            

            DateTime parseDateTime;
            if (DateTime.TryParse(dayForm["dayStart"].ToString(), out parseDateTime) && DateTime.TryParse(dayForm["dayEnd"].ToString(), out parseDateTime))
            {
                var dayStart = DateTime.Parse(dayForm["dayStart"]);

                var dayEnd = DateTime.Parse(dayForm["dayEnd"]);
                ViewBag.TinTuc = db.TinTucs.Where(n => n.NgayDang >= dayStart && n.NgayDang <= dayEnd).ToList();
                ViewBag.TinTuc.Reverse();

                maxLuotXemTinTuc = db.TinTucs.FirstOrDefault(n => n.NgayDang >= dayStart && n.NgayDang <= dayEnd);

                foreach (var item in ViewBag.TinTuc)
                {
                    if (item.LuotXem > maxLuotXemTinTuc.LuotXem)
                    {
                        maxLuotXemTinTuc = item;
                    }
                }
                ViewBag.MaxLuotXem = maxLuotXemTinTuc;



                maxBinhLuanTinTuc = db.TinTucs.FirstOrDefault(n => n.NgayDang >= dayStart && n.NgayDang <= dayEnd);
                foreach (var item in ViewBag.TinTuc)
                {
                    if (item.GetBinhLuanCount() > maxBinhLuanTinTuc.GetBinhLuanCount())
                    {
                        maxBinhLuanTinTuc = item;
                    }
                }
                ViewBag.MaxBinhLuan = maxBinhLuanTinTuc;
            }
            else
            {
                ViewBag.TinTuc = db.TinTucs.ToList();
                ViewBag.TinTuc.Reverse();

                 maxLuotXemTinTuc = db.TinTucs.FirstOrDefault();

                foreach (var item in ViewBag.TinTuc)
                {
                    if (item.LuotXem > maxLuotXemTinTuc.LuotXem)
                    {
                        maxLuotXemTinTuc = item;
                    }
                }
                ViewBag.MaxLuotXem = maxLuotXemTinTuc;



                 maxBinhLuanTinTuc = db.TinTucs.FirstOrDefault();
                foreach (var item in ViewBag.TinTuc)
                {
                    if (item.GetBinhLuanCount() > maxBinhLuanTinTuc.GetBinhLuanCount())
                    {
                        maxBinhLuanTinTuc = item;
                    }
                }
                ViewBag.MaxBinhLuan = maxBinhLuanTinTuc;
                ViewBag.ThongBao = "Xin hãy nhập đủ ngày tháng năm";
            }
            return View();
        }
    }
}