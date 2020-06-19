using PagedList;
using ShopCar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopCar.Controllers
{
    public class TimKiemController : Controller
    {
        CarShopEntities db = new CarShopEntities();
        // GET: TimKiem
        [HttpGet]
        public ActionResult KQTimKiem(string MaLoai,string sTuKhoa, int? page)
        {
            if(sTuKhoa==null)
            {
                var lstSP = db.SanPhams.OrderByDescending(x => x.MaSP).Where(x => x.MaLoaiSP == MaLoai).ToPagedList(page ?? 1, 16);
                ViewBag.TuKhoa = sTuKhoa;
                return View(lstSP);
            }
            var lstSanPham = db.SanPhams.OrderByDescending(x => x.MaSP).Where(x => x.MaLoaiSP==MaLoai && x.TenSP.Contains(sTuKhoa)).ToPagedList(page ?? 1, 16);

            ViewBag.TuKhoa = sTuKhoa;
            return View(lstSanPham);
        }
        [HttpPost]
        public ActionResult LayTuKhoaTimKiem(LoaiSP lsp, string sTuKhoa)
        {
            return RedirectToAction("KQTimKiem", new { @MaLoai=lsp.MaLoaiSP, @sTuKhoa = sTuKhoa });
        }
        [HttpPost]
        public ActionResult KQTimKiemPartial(string sTuKhoa, int? page)
        {

            var lstSanPham = db.SanPhams.OrderByDescending(x => x.MaSP).Where(x => x.TenSP.Contains(sTuKhoa));
            ViewBag.TuKhoa = sTuKhoa;
            return View(lstSanPham);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (disposing)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}