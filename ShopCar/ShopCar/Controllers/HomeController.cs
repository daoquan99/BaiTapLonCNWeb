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
        public ActionResult MenuPartial()
        {
            ViewBag.lstLoaiSP = db.LoaiSPs;
            return View();
        }
        
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
        public ActionResult DangKy(KhachHang kh,string NhapLaiMatKhau)
        {
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                if (ModelState.IsValid)
                {
                    var kh1 = db.KhachHangs.Where(x => x.UserName == kh.UserName).FirstOrDefault();

                    if(kh1!=null)
                    {
                        ViewBag.ThongBao = "tài khoản đã tồn tại";
                        return View();
                    }
                    else
                    {
                        if (kh.Pass == NhapLaiMatKhau)
                        {
                            int dem = db.KhachHangs.Count();
                            kh.MaKH = autoID(dem);
                            db.KhachHangs.Add(kh);
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
        private string autoID(int dem)
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
        public ActionResult ThongBao()
        {
            return View();
        }


        public ActionResult VeChungToi()
        {
            return View();
        }
        public ActionResult LienHe()
        {
            return View();
        }
        public ActionResult CamKetChatLuong()
        {
            return View();
        }
        public ActionResult HuongDanMuaHang()
        {
            return View();
        }
        public ActionResult VanChuyeVaThanhToan()
        {
            return View();
        }
        public ActionResult ChinhSachBaoHanh()
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