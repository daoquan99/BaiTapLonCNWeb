using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopCar.Model;

namespace WebsiteBanHang.Controllers
{
    public class AdminController : Controller
    {
        CarShopEntities db = new CarShopEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        [HttpGet]
        public ActionResult ThongKe()
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();
            ViewBag.SoNguoiDangOnline = HttpContext.Application["SoNguoiDangOnline"].ToString();
            ViewBag.TongThanhVien = db.KhachHangs.Count();
            ViewBag.TongDonDatHang = db.HoaDons.Count();
            return View();
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