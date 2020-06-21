using ShopCar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopCar.Controllers
{
    public class GioHangController : Controller
    {
        CarShopEntities db = new CarShopEntities();
        // GET: GioHang
        public List<ItemGioHang> LayGioHang()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if(Session["GioHang"]==null)
            {
                lstGioHang = new List<ItemGioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public double TinhTongSoLuong()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null) return 0;
            return lstGioHang.Sum(x => x.SoLuong);
        }
        public decimal TinhTongTien()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null) return 0;
            return lstGioHang.Sum(x => x.ThanhTien);
        }



        public ActionResult XemGioHang()
        {
            List<ItemGioHang> lstGH = LayGioHang();
             
            return View(lstGH);
        }
        [ChildActionOnly]
        public ActionResult GioHangPartial()
        {
            List<ItemGioHang> lstGioHang = LayGioHang();

            if (TinhTongTien() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView(lstGioHang);
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();
            return PartialView(lstGioHang);
        }
        public ActionResult ThemGioHangAjax(string MaSP, string strURL)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<ItemGioHang> lstGioHang = LayGioHang();

            ItemGioHang checkSP = lstGioHang.SingleOrDefault(x => x.MaSP == MaSP);

            if (checkSP != null)
            {
                if (sp.SoLuong <= checkSP.SoLuong)
                {
                    return Content("<script> alert(\"Sản phẩm đã hết hàng!\");</script>");
                }
                checkSP.SoLuong++;
                checkSP.ThanhTien = checkSP.SoLuong * checkSP.DonGia;
                ViewBag.TongSoLuong = TinhTongSoLuong();
                ViewBag.TongTien = TinhTongTien();

                return PartialView("GioHangPartial",lstGioHang);
            }

            ItemGioHang newItem = new ItemGioHang(MaSP);
            if (sp.SoLuong <= newItem.SoLuong)
            {
                return Content("<script> alert(\"Sản phẩm đã hết hàng!\");</script>");
            }

            lstGioHang.Add(newItem);
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();

            return PartialView("GioHangPartial",lstGioHang);
        }

        public ActionResult ThemGioHang(string MaSP, string strURL)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x =>x.MaSP==MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<ItemGioHang> lstGioHang = LayGioHang();
            ItemGioHang checkSP = lstGioHang.SingleOrDefault(x => x.MaSP == MaSP);
            if (checkSP != null)
            {
                if (sp.SoLuong <= checkSP.SoLuong)
                {
                    return Content("<script> alert(\"Sản phẩm đã hết hàng!\");</script>");
                }
                checkSP.SoLuong++;
                checkSP.ThanhTien = checkSP.SoLuong * checkSP.DonGia;
                return Redirect(strURL);
            }

            ItemGioHang newItem = new ItemGioHang(MaSP);
            if (sp.SoLuong <= newItem.SoLuong)
            {
                return Content("<script> alert(\"Sản phẩm đã hết hàng!\");</script>");
            }
            lstGioHang.Add(newItem);

            return Redirect(strURL);
        }
        public ActionResult XoaItemGH(string MaSP)
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == MaSP);
            if (sp == null)
            {
                //Đương dẫn không hơp lệ
                Response.StatusCode = 404;
                return null;
            }
            List<ItemGioHang> lstGioHang = LayGioHang();
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(x => x.MaSP == MaSP);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            lstGioHang.Remove(spCheck);
            return RedirectToAction("XemGioHang");
        }
        public ActionResult SuaGioHang(string MaSP)
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == MaSP);
            if (sp == null)
            {
                //Đương dẫn không hơp lệ
                Response.StatusCode = 404;
                return null;
            }
            List<ItemGioHang> lstGioHang = LayGioHang();
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(x => x.MaSP == MaSP);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.GioHang = lstGioHang;
            return View(spCheck);
        }

        [HttpPost]
        public ActionResult CapNhatGioHang(ItemGioHang itemGH)
        {
            SanPham spCheck = db.SanPhams.SingleOrDefault(x => x.MaSP == itemGH.MaSP);
            if (spCheck.SoLuong < itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            List<ItemGioHang> lstGioHang = LayGioHang();
            ItemGioHang itemGHUpdate = lstGioHang.Find(x => x.MaSP == itemGH.MaSP);
            itemGHUpdate.SoLuong = itemGH.SoLuong;
            itemGHUpdate.ThanhTien = itemGHUpdate.SoLuong * itemGHUpdate.DonGia;
            return RedirectToAction("XemGioHang");
        }

        public ActionResult ThongBaoHetSP()
        {
            return View();
        }

        public ActionResult DatHang(KhachHang kh)
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            KhachHang kh1 = new KhachHang();

            if (Session["TaiKhoan"] == null)
            {
                //Đối với khách hàng chưa có tài khoản 
                kh1 = kh;
                int demKH = db.KhachHangs.Count();
                kh1.MaKH = autoIDKH(demKH);
                db.KhachHangs.Add(kh1);
                db.SaveChanges();
            }
            else
            {
                kh1 = (KhachHang)Session["TaiKhoan"];
            }    
            HoaDon ddh = new HoaDon();
            TaoDonDatHang(ddh, kh1);
            Session["GioHang"] = null;
            return RedirectToAction("XemGioHang");
        }
        void TaoDonDatHang(HoaDon ddh, KhachHang kh1)
        {
            int demDDH = db.HoaDons.Count();
            ddh.MaHD = autoIDHD(demDDH);
            ddh.MaKH = kh1.MaKH;
            ddh.NgayDat = DateTime.Now;
            db.HoaDons.Add(ddh);
            db.SaveChanges();

            List<ItemGioHang> lstGioHang = LayGioHang();

            foreach (var item in lstGioHang)
            {
                CTHoaDon ctdh = new CTHoaDon();
                ctdh.MaHD = ddh.MaHD;
                ctdh.MaSP = item.MaSP;
                ctdh.SoLuong = item.SoLuong;
                SanPham sp = db.SanPhams.Single(x => x.MaSP == item.MaSP);
                sp.SoLuong -= item.SoLuong;
                db.CTHoaDons.Add(ctdh);
                db.SaveChanges();
            }
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

        private string autoIDKH(int dem)
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
        private string autoIDHD(int dem)
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