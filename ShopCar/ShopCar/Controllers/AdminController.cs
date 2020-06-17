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
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string txtTenDangNhap,string txtMatKhau)
        {
            Admin ad = db.Admins.Where(x => x.UserAd == txtTenDangNhap && x.Pass == txtMatKhau).FirstOrDefault();
            if(ad!=null)
            {
                Session["TaiKhoanAdmin"] = ad;
                return RedirectToAction("Index");
            }
            ViewBag.thongbao = "Tài khoản hoặc mật khẩu không chính xác";
            return View();
           
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoanAdmin"] = null;
            return View("DangNhap");
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