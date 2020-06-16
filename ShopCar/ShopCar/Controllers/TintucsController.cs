using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopCar.Model;

namespace ShopCar.Controllers
{
    public class TintucsController : Controller
    {
        private CarShopEntities db = new CarShopEntities();

        // GET: Tintucs
        public ActionResult Index()
        {
            return View(db.Tintucs.ToList());
        }

        // GET: Tintucs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tintuc tintuc = db.Tintucs.Find(id);
            if (tintuc == null)
            {
                return HttpNotFound();
            }
            return View(tintuc);
        }
        private bool idHasExist(string id)
        {
            Tintuc temp = db.Tintucs.Find(id);
            if (temp == null)
            {
                return false;
            }

            return true;
        }
        private string autoID()
        {
            string id = "";
            int dem = db.Tintucs.ToList().Count();
            while (true)
            {
                if (dem < 10 && dem >= 1)
                {
                    id = "T000" + Convert.ToString(dem + 1);
                }
                else if (dem >= 10 && dem < 100)
                {
                    id = "T00" + Convert.ToString(dem + 1);
                }
                else if (dem >= 100)
                {
                    id = "T0" + Convert.ToString(dem + 1);
                }
                else if (dem == 0)
                {
                    id = "T0001";
                }
                if (idHasExist(id) == false)
                {
                    break;
                }
                else
                {
                    dem++;
                }
            }
            return id;
        }
        // GET: Tintucs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tintucs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTin,URLAnh,NoiDung")] Tintuc tintuc, HttpPostedFileBase anh)
        {
            if (ModelState.IsValid)
            {
                if (anh != null)
                {
                    var filename = Path.GetFileName(anh.FileName);
                    var path = Path.Combine(Server.MapPath("~/images"), filename);
                    anh.SaveAs(path);
                    tintuc.URLAnh = filename;
                }
                tintuc.MaTin = autoID();
                db.Tintucs.Add(tintuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tintuc);
        }

        // GET: Tintucs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tintuc tintuc = db.Tintucs.Find(id);
            if (tintuc == null)
            {
                return HttpNotFound();
            }
            return View(tintuc);
        }

        // POST: Tintucs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTin,URLAnh,NoiDung")] Tintuc tintuc, HttpPostedFileBase anh)
        {
            if (ModelState.IsValid)
            {
                if (anh != null)
                {
                    var filename = Path.GetFileName(anh.FileName);
                    var path = Path.Combine(Server.MapPath("~/images"), filename);
                    anh.SaveAs(path);
                    tintuc.URLAnh = filename;
                }
                db.Entry(tintuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tintuc);
        }

        // GET: Tintucs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tintuc tintuc = db.Tintucs.Find(id);
            if (tintuc == null)
            {
                return HttpNotFound();
            }
            return View(tintuc);
        }

        // POST: Tintucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Tintuc tintuc = db.Tintucs.Find(id);
            db.Tintucs.Remove(tintuc);
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
