using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CaptchaMvc.HtmlHelpers;
using ShopCar.Model;

namespace ShopCar.Controllers
{
    public class HomeController : Controller
    {
        CarShopEntities db = new CarShopEntities();
        public ActionResult Index()
        {
            ViewBag.lstChanNang = db.SanPhams.Where(x => x.MaLoaiSP == "L0003");

            ViewBag.lstPhuKienKhac = db.SanPhams.Where(x => x.MaLoaiSP == "L0004");
            ViewBag.lstDongCo = db.SanPhams.Where(x => x.MaLoaiSP == "L0005");
            ViewBag.lstPhanh = db.SanPhams.Where(x => x.MaLoaiSP == "L0006");
            ViewBag.lstDauNhot= db.SanPhams.Where(x => x.MaLoaiSP == "L0007");
            ViewBag.lstSon = db.SanPhams.Where(x => x.MaLoaiSP == "L0008");

            return View();
        }
        
        [ChildActionOnly]
        [HttpGet]
        public ActionResult MenuPartial()
        {
            ViewBag.lstLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoai");
            return View();
        }
        //[ChildActionOnly]
        //[HttpPost]
        //public ActionResult MenuPartial(LoaiSP Lsp)
        //{
        //    ViewBag.lstLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoai");
        //    return View(Lsp);
        //}

        [HttpPost]
        public ActionResult DangNhap(FormCollection f /*string txtTenDangNhap, string txtMatKhau*/)
        {
            string TenTK = f["txtTenDangNhap"].ToString();
            string MatKhau = f["txtMatKhau"].ToString();
            KhachHang kh = db.KhachHangs.SingleOrDefault(x => x.UserName == TenTK && x.Pass == MatKhau);

            if (kh != null)
            {
                Session["TaiKhoan"] = kh;
                //return RedirectToAction("Index");
                return RedirectToAction("Index");
            }
            return RedirectToAction("ThongBao");

            //return RedirectToAction("Index");<script>alert('Sai tài khoản hoặc mật khẩu')</script>
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(KhachHang kh)
        {
            
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                if (ModelState.IsValid)
                {
                    var kh1 = db.KhachHangs.Where(x => x.UserName == kh.UserName);

                    if(kh1!=null)
                    {
                        ViewBag.ThongBao = "tài khoản đã tồn tại";
                        return View();
                    }    
                    ViewBag.ThongBao = "Thêm Thành công";
                    db.KhachHangs.Add(kh);
                    db.SaveChanges();
                }
                else
                    ViewBag.ThongBao = "Thêm Thất bại";
                return View();
            }
            ViewBag.ThongBao = "Sai mã captcha";
            return View();
        }
        public ActionResult ThongBao()
        {
            return View();
        }
    }
}