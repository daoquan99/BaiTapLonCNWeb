using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ShopCar.Model;

namespace WebsiteBanHang.Controllers
{
    
    public class AdminController : Controller
    {
        CarShopEntities db = new CarShopEntities();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"]==null)
            {
                FormsAuthentication.SignOut();
                return View("DangNhap");
            }    
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
            if(ad != null)
            {
                Session["TaiKhoanAdmin"] = ad;

                var lstQuyen = db.QuyenAds.Where(x => x.MaAdmin == ad.MaAdmin);
                string quyen = "";

                foreach (var item in lstQuyen)
                {
                    quyen += item.MaQuyen + ",";
                }
                if (quyen.Length>0)
                {
                    quyen = quyen.Substring(0, quyen.Length - 1);
                    PhanQuyen(txtTenDangNhap, quyen);
                }    

                return RedirectToAction("Index");
            }


            ViewBag.thongbao = "Tài khoản hoặc mật khẩu không chính xác";
            return View();
           
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoanAdmin"] = null;
            FormsAuthentication.SignOut();
            return View("DangNhap");
        }
        public void PhanQuyen(string txtTenDangNhap, string quyen)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1, txtTenDangNhap,
                                                        DateTime.Now, //time begin
                                                        DateTime.Now.AddHours(2), //timeout
                                                        false, quyen,
                                                        FormsAuthentication.FormsCookiePath);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent)
                cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);
        }
        //tạo trang ngăn đăng nhập
        public ActionResult LoiPhanQuyen()
        {
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