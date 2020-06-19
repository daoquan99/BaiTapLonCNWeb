using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using ShopCar.Model;
using PagedList;

namespace ShopCar.Controllers
{
    public class SanPhamController : Controller
    {
        CarShopEntities db = new CarShopEntities();
        // GET: SanPham
        [ChildActionOnly]
        public ActionResult SanPhamPartial()
        {
            return PartialView();
        }

        public ActionResult DanhSachSanPham(int? page)
        {
            var lstSanPham = db.SanPhams.OrderByDescending(x=>x.MaSP).ToPagedList(page ?? 1, 16);
            

            return View(lstSanPham);
        }
        [ChildActionOnly]
        public ActionResult DanhMucLoaiSP()
        {
            var lstLoaiSP = db.LoaiSPs;
            return PartialView(lstLoaiSP);
        }
        public ActionResult XemChiTietSanPham(string MaSP)
        {
            if (MaSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP );

            if (sp == null)
            {
                return HttpNotFound();
            }

            ViewBag.lstSP = db.SanPhams.Where(x => x.MaLoaiSP == sp.MaLoaiSP);

            return View(sp);
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