using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewsApp.Models;
using System.IO;

namespace NewsApp.Areas.AdminAreas.Controllers
{
    public class TinTucsController : Controller
    {
        private NewsDBEntities db = new NewsDBEntities();

        // GET: AdminAreas/TinTucs
        public ActionResult Index()
        {
            var tinTucs = db.TinTucs.Include(t => t.DanhMuc);
            return View(tinTucs.ToList());
        }

        // GET: AdminAreas/TinTucs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // GET: AdminAreas/TinTucs/Create
        public ActionResult Create()
        {
            ViewBag.IDDanhMuc = new SelectList(db.DanhMucs, "IDDanhMuc", "TenDanhMuc");
            return View();
        }

        // POST: AdminAreas/TinTucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TinTuc tinTuc, HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                if (HinhAnh != null)
                {
                    var fileName = Path.GetFileName(HinhAnh.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    tinTuc.HinhAnh = fileName;
                    HinhAnh.SaveAs(path);
                }

                tinTuc.NgayDang = DateTime.Now;
                tinTuc.NoiDung += ".";
                tinTuc.LuotXem = 0;
                db.TinTucs.Add(tinTuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDDanhMuc = new SelectList(db.DanhMucs, "IDDanhMuc", "TenDanhMuc", tinTuc.IDDanhMuc);
            return View(tinTuc);
        }

        // GET: AdminAreas/TinTucs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDDanhMuc = new SelectList(db.DanhMucs, "IDDanhMuc", "TenDanhMuc", tinTuc.IDDanhMuc);
            return View(tinTuc);
        }

        // POST: AdminAreas/TinTucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TinTuc tinTuc, HttpPostedFileBase HinhAnhTin)
        {
            if (ModelState.IsValid)
            {
                if (HinhAnhTin != null)
                {
                    var fileName = Path.GetFileName(HinhAnhTin.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    tinTuc.HinhAnh = fileName.ToString();
                    HinhAnhTin.SaveAs(path);
                }


                db.Entry(tinTuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDDanhMuc = new SelectList(db.DanhMucs, "IDDanhMuc", "TenDanhMuc", tinTuc.IDDanhMuc);
            return View(tinTuc);
        }

        // GET: AdminAreas/TinTucs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // POST: AdminAreas/TinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TinTuc tinTuc = db.TinTucs.Find(id);
            var lstCommentTinTuc = db.BinhLuans.Where(c => c.IDTinTuc == id).ToList();
            var lstLastSeenTinTuc = db.XemSaus.Where(c => c.IDTinTuc == id).ToList();
            foreach (var item in lstCommentTinTuc)
            {
                db.BinhLuans.Remove(item);
            }
            foreach (var item in lstLastSeenTinTuc)
            {
                db.XemSaus.Remove(item);
            }
            db.TinTucs.Remove(tinTuc);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
