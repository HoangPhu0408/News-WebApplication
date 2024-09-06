using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewsApp.Models;

namespace NewsApp.Areas.AdminAreas.Controllers
{
    public class QuangCaosController : Controller
    {
        private NewsDBEntities db = new NewsDBEntities();

        // GET: AdminAreas/QuangCaos
        public ActionResult Index()
        {
            return View(db.QuangCaos.ToList());
        }

        // GET: AdminAreas/QuangCaos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuangCao quangCao = db.QuangCaos.Find(id);
            if (quangCao == null)
            {
                return HttpNotFound();
            }
            return View(quangCao);
        }

        // GET: AdminAreas/QuangCaos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminAreas/QuangCaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDQuangCao,Link,HinhAnhQuangCao")] QuangCao quangCao)
        {
            if (ModelState.IsValid)
            {
                db.QuangCaos.Add(quangCao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quangCao);
        }

        // GET: AdminAreas/QuangCaos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuangCao quangCao = db.QuangCaos.Find(id);
            if (quangCao == null)
            {
                return HttpNotFound();
            }
            return View(quangCao);
        }

        // POST: AdminAreas/QuangCaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( QuangCao quangCao, HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                if (HinhAnh != null)
                {
                    var fileName = Path.GetFileName(HinhAnh.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    quangCao.HinhAnhQuangCao = fileName;
                    HinhAnh.SaveAs(path);
                }
                db.Entry(quangCao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quangCao);
        }

        // GET: AdminAreas/QuangCaos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuangCao quangCao = db.QuangCaos.Find(id);
            if (quangCao == null)
            {
                return HttpNotFound();
            }
            return View(quangCao);
        }

        // POST: AdminAreas/QuangCaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuangCao quangCao = db.QuangCaos.Find(id);
            db.QuangCaos.Remove(quangCao);
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
