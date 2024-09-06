using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsApp.Models;

namespace NewsApp.Controllers
{
    public class NewsController : Controller
    {
        NewsDBEntities db = new NewsDBEntities();
        // GET: News
        public ActionResult Index(int id)
        {
            var news = db.TinTucs.FirstOrDefault(n => n.IDTinTuc == id);
            ViewBag.TinTuc = db.TinTucs.FirstOrDefault(n => n.IDTinTuc == id);
            //int luotXem = (int)news.LuotXem;



            ViewBag.IndexOfDot = news.NoiDung.IndexOf(".");

            ViewBag.LstComment = db.BinhLuans.Where(n => n.IDTinTuc == id).ToList();
            ViewBag.LstComment.Reverse();
            ViewBag.RelatedNews = db.TinTucs.Where(n => n.IDDanhMuc == news.IDDanhMuc && n.IDTinTuc != news.IDTinTuc).ToList();


            news.LuotXem++;
            db.SaveChanges();
            ViewBag.QuangCao = db.QuangCaos.ToList();

            return View();
        }

        public ActionResult GetReadLaterList(int idNguoiDung)
        {
            ViewBag.XemSau = db.XemSaus.Where(n => n.IDNguoiDung == idNguoiDung).ToList();
            ViewBag.XemSau.Reverse();
            ViewBag.QuangCao = db.QuangCaos.ToList();

            return View();
        }
        
        [HttpPost]
        public ActionResult AddComment(BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            { 
                binhLuan.NgayBinhLuan =  DateTime.Now;
                db.BinhLuans.Add(binhLuan);
                db.SaveChanges();
            }
            return RedirectToAction("Index/"+binhLuan.IDTinTuc, "News");
        }
        public ActionResult AddReadLater(XemSau xemSau)
        {
            if (ModelState.IsValid)
            {
                var checkXemSau = db.XemSaus.FirstOrDefault(n => n.IDTinTuc == xemSau.IDTinTuc);
                if(checkXemSau == null)
                {
                    db.XemSaus.Add(xemSau);
                    db.SaveChanges();
                } 
            }
            return RedirectToAction("Index/" + xemSau.IDTinTuc, "News");
        }

        public ActionResult RemoveReadLater(int idReadLater)
        {
            var currentReadLater = db.XemSaus.FirstOrDefault(n => n.ID == idReadLater);
            int idUser = (int)currentReadLater.IDNguoiDung;

            db.XemSaus.Remove(currentReadLater);
            db.SaveChanges();
            return RedirectToAction("GetReadLaterList" , "News", new { idNguoiDung = idUser });
        }
    }
}