using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopCar.Model;

namespace ShopCar.Controllers
{
    public class PhieuNhapsController : Controller
    {
        private CarShopEntities db = new CarShopEntities();

        // GET: PhieuNhaps
        public ActionResult Index()
        {
            var phieuNhaps = db.PhieuNhaps.Include(p => p.NhaCC);
            return View(phieuNhaps.ToList());
        }

        // GET: PhieuNhaps/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuNhap phieuNhap = db.PhieuNhaps.Find(id);
          
            if (phieuNhap == null)
            {
                return HttpNotFound();
            }
            return View(phieuNhap);
        }

        // GET: PhieuNhaps/Create
        public ActionResult Create()
        {
            ViewBag.MaNCC = new SelectList(db.NhaCCs, "MaNCC", "TenNCC");
            return View();
        }

        // POST: PhieuNhaps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPN,NgayNhap,MaNCC")] PhieuNhap phieuNhap)
        {
            if (ModelState.IsValid)
            {
                string duoiID = "";
                if (db.PhieuNhaps.ToList().Count() < 10 && db.PhieuNhaps.ToList().Count() >= 1)
                {
                    duoiID = "000" + Convert.ToString(db.PhieuNhaps.ToList().Count() + 1);
                }
                else if (db.PhieuNhaps.ToList().Count() >= 10 && db.PhieuNhaps.ToList().Count() < 100)
                {
                    duoiID = "00" + Convert.ToString(db.PhieuNhaps.ToList().Count() + 1);
                }
                else if (db.PhieuNhaps.ToList().Count() == 0)
                {
                    duoiID = "0001";
                }
               phieuNhap.MaPN = "P" + duoiID;
                db.PhieuNhaps.Add(phieuNhap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNCC = new SelectList(db.NhaCCs, "MaNCC", "TenNCC", phieuNhap.MaNCC);
            return View(phieuNhap);
        }

        // GET: PhieuNhaps/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuNhap phieuNhap = db.PhieuNhaps.Find(id);
            if (phieuNhap == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNCC = new SelectList(db.NhaCCs, "MaNCC", "TenNCC", phieuNhap.MaNCC);
            return View(phieuNhap);
        }

        // POST: PhieuNhaps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPN,NgayNhap,MaNCC")] PhieuNhap phieuNhap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuNhap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNCC = new SelectList(db.NhaCCs, "MaNCC", "TenNCC", phieuNhap.MaNCC);
            return View(phieuNhap);
        }

        // GET: PhieuNhaps/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuNhap phieuNhap = db.PhieuNhaps.Find(id);
            if (phieuNhap == null)
            {
                return HttpNotFound();
            }
            return View(phieuNhap);
        }

        // POST: PhieuNhaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PhieuNhap phieuNhap = db.PhieuNhaps.Find(id);
            db.PhieuNhaps.Remove(phieuNhap);
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
