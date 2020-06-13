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
    public class CTPhieuNhapsController : Controller
    {
        private CarShopEntities db = new CarShopEntities();

        // GET: CTPhieuNhaps
        public ActionResult Index()
        {
            var cTPhieuNhaps = db.CTPhieuNhaps.Include(c => c.PhieuNhap).Include(c => c.SanPham);
            return View(cTPhieuNhaps.ToList());
        }

        // GET: CTPhieuNhaps/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPhieuNhap cTPhieuNhap = db.CTPhieuNhaps.Find(id);
            if (cTPhieuNhap == null)
            {
                return HttpNotFound();
            }
            return View(cTPhieuNhap);
        }

        // GET: CTPhieuNhaps/Create
        public ActionResult Create()
        {
            ViewBag.MaPN = new SelectList(db.PhieuNhaps, "MaPN", "MaNCC");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: CTPhieuNhaps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPN,MaSP,SoLuong,DonGia")] CTPhieuNhap cTPhieuNhap)
        {
            if (ModelState.IsValid)
            {
                db.CTPhieuNhaps.Add(cTPhieuNhap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaPN = new SelectList(db.PhieuNhaps, "MaPN", "MaNCC", cTPhieuNhap.MaPN);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cTPhieuNhap.MaSP);
            return View(cTPhieuNhap);
        }

        // GET: CTPhieuNhaps/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPhieuNhap cTPhieuNhap = db.CTPhieuNhaps.Find(id);
           
            if (cTPhieuNhap == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaPN = new SelectList(db.PhieuNhaps, "MaPN", "MaNCC", cTPhieuNhap.MaPN);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cTPhieuNhap.MaSP);
            return View(cTPhieuNhap);
        }

        // POST: CTPhieuNhaps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPN,MaSP,SoLuong,DonGia")] CTPhieuNhap cTPhieuNhap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTPhieuNhap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaPN = new SelectList(db.PhieuNhaps, "MaPN", "MaNCC", cTPhieuNhap.MaPN);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cTPhieuNhap.MaSP);
            return View(cTPhieuNhap);
        }

        // GET: CTPhieuNhaps/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPhieuNhap cTPhieuNhap = db.CTPhieuNhaps.Find(id);
            if (cTPhieuNhap == null)
            {
                return HttpNotFound();
            }
            return View(cTPhieuNhap);
        }

        // POST: CTPhieuNhaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CTPhieuNhap cTPhieuNhap = db.CTPhieuNhaps.Find(id);
            db.CTPhieuNhaps.Remove(cTPhieuNhap);
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
