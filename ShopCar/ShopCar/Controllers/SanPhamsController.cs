using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Microsoft.Ajax.Utilities;
using ShopCar.Model;

namespace ShopCar.Controllers
{
    public class SanPhamsController : Controller
    {
        private CarShopEntities db = new CarShopEntities();

        // GET: SanPhams
        public ActionResult Index()
        {

            //  var sanPhams = db.SanPhams.Include(s => s.LoaiSP).Include(s => s.SanPham1).Include(s => s.SanPham2);
            var sanPhams = db.SanPhams.Include(s => s.LoaiSP);
               return View(sanPhams.ToList());
           // return View();
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoai");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,SoLuong,DonGia,MoTa,GiaKm,URLAnh,MaLoaiSP")] SanPham sanPham)//,HttpPostedFileBase anh)
        {
            if (ModelState.IsValid)
            {
                //string filename = System.IO.Path.GetFileName(anh.FileName);
                //anh.SaveAs(filename);
                //sanPham.URLAnh = filename;
                
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoai", sanPham.MaLoaiSP);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", sanPham.MaSP);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", sanPham.MaSP);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoai", sanPham.MaLoaiSP);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", sanPham.MaSP);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", sanPham.MaSP);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,SoLuong,DonGia,MoTa,GiaKm,URLAnh,MaLoaiSP")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoai", sanPham.MaLoaiSP);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", sanPham.MaSP);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", sanPham.MaSP);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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
