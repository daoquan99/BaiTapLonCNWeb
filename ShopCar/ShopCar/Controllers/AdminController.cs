using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CaptchaMvc.HtmlHelpers;
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
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(Admin ad,string NhapLaiMatKhau)
        {
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                if (ModelState.IsValid)
                {
                    var ad1 = db.Admins.Where(x => x.UserAd == ad.UserAd).FirstOrDefault();
                    if (ad1 != null)
                    {
                        ViewBag.ThongBao = "tài khoản đã tồn tại";
                        return View();
                    }
                    else
                    {
                        if (ad.Pass == NhapLaiMatKhau)
                        {
                            int dem = db.KhachHangs.Count();
                            ad.MaAdmin = autoIDAdmin(dem);
                            db.Admins.Add(ad);
                            db.SaveChanges();
                            ViewBag.ThongBao = "Thêm thành công!";
                        }
                        else ViewBag.ThongBao = "Mật khẩu không khớp";
                    }
                }
                else
                    ViewBag.ThongBao = "Thêm Thất bại";
                return View();
            }
            ViewBag.ThongBao = "Sai mã captcha";
            return View();
        }

        private bool idHasExist(string id)
        {
            LoaiSP temp = db.LoaiSPs.Find(id);
            if (temp == null)
            {
                return false;
            }
            return true;
        }
        private string autoIDAdmin(int dem)
        {
            string id = "";
            while (true)
            {
                if (dem < 10 && dem > 1)
                {
                    id = "L000" + Convert.ToString(dem);
                }
                else if (dem >= 10 && dem < 100)
                {
                    id = "L00" + Convert.ToString(dem);
                }
                else if (dem >= 100)
                {
                    id = "L0" + Convert.ToString(dem);
                }
                else if (dem == 1)
                {
                    id = "L0001";
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