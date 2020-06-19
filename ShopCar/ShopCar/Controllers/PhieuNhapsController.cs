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
    [Authorize(Roles = "QuanTriVien,QuanLyPhieuNhap")]
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
            ViewBag.lstPN = db.CTPhieuNhaps.Where(x => x.MaPN == id );
            return View(phieuNhap);
        }
        private bool idHasExist(string id)
        {
            PhieuNhap temp = db.PhieuNhaps.Find(id);
            if (temp == null)
            {
                return false;
            }

            return true;
        }
        private string autoID()
        {
            string id = "";
            int dem = db.PhieuNhaps.ToList().Count()+1;
            while (true)
            {
                if (dem < 10 && dem > 1)
                {
                    id = "P000" + Convert.ToString(dem );
                }
                else if (dem >= 10 && dem < 100)
                {
                    id = "P00" + Convert.ToString(dem );
                }
                else if (dem >= 100)
                {
                    id = "P0" + Convert.ToString(dem );
                }
                else if (dem == 1)
                {
                    id = "P0001";
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


                phieuNhap.MaPN = autoID();
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
            var dsPN = db.CTPhieuNhaps.Where(x => x.MaPN == id);
            foreach (var i in dsPN)
            {
                db.CTPhieuNhaps.Remove(i);
            }
            db.PhieuNhaps.Remove(phieuNhap);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete1(string id, string id2)
        {
            if (id == null||id2==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dsPN = db.CTPhieuNhaps.Where(x => x.MaPN == id);
            CTPhieuNhap cTPhieuNhap = new CTPhieuNhap();
            foreach (var i in dsPN)
            {
                if (i.MaSP == id2)
                {
                    cTPhieuNhap = i;
                    break;
                }
            }
            if (cTPhieuNhap == null)
            {
                return HttpNotFound();
            }
            return View(cTPhieuNhap);
        }

        // POST: CTPhieuNhaps/Delete/5
        [HttpPost, ActionName("Delete1")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete1Confirmed(string id,string id2)
        {
            var dsPN= db.CTPhieuNhaps.Where(x => x.MaPN == id);
            CTPhieuNhap cTPhieuNhap = new CTPhieuNhap();
            foreach (var i in dsPN)
            {
                if (i.MaSP==id2)
                {
                    cTPhieuNhap = i;
                    break;
                }
            }        
            db.CTPhieuNhaps.Remove(cTPhieuNhap);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit1(string id, string id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPhieuNhap cTPhieuNhap = new CTPhieuNhap();
            var dsPN = db.CTPhieuNhaps.Where(x => x.MaPN == id);
            foreach (var i in dsPN)
            {
                if (i.MaSP == id2)
                {
                    cTPhieuNhap = i;
                    break;
                }
            }
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
        public ActionResult Edit1([Bind(Include = "MaPN,MaSP,SoLuong,DonGia")] CTPhieuNhap cTPhieuNhap)
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult NhapHang()
        {
            ViewBag.MaNCC = db.NhaCCs;
            ViewBag.ListSP = db.SanPhams;
            return View();
        }
        [HttpPost]
        public ActionResult NhapHang(PhieuNhap phieuNhap, IEnumerable<CTPhieuNhap> modelList)
        {
            ViewBag.MaNCC = db.NhaCCs;
            ViewBag.ListSP = db.SanPhams;
            AddTTPhieuNhap(phieuNhap, modelList);
            return View(phieuNhap);
        }
        void AddTTPhieuNhap(PhieuNhap PN, IEnumerable<CTPhieuNhap> modelList)
        {
            if (modelList != null)
            {
                PN.MaPN = autoID();
                PN.MaPN = autoID();
                db.PhieuNhaps.Add(PN);
                db.SaveChanges();
            }
            AddTTChiTietPhieuNhap(PN, modelList);
        }
        void AddTTChiTietPhieuNhap(PhieuNhap model, IEnumerable<CTPhieuNhap> modelList)
        {
            foreach (var item in modelList)
            {
                SanPham sp = db.SanPhams.Single(x => x.MaSP == item.MaSP);
                sp.SoLuong += item.SoLuong;
                item.MaPN = model.MaPN;
                db.CTPhieuNhaps.Add(item);
                db.SaveChanges();
            }
        }
       
    }
}
