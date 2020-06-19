using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ShopCar.Model;

namespace ShopCar.Controllers
{
    [Authorize(Roles =("QuanTriVien,DuyetDonHang"))]
    public class QuanLyDonHangController : Controller
    {
        CarShopEntities db = new CarShopEntities();
        public ActionResult DonHangChuaGiao()
        {
            var lstDonHangChuaGiao = db.HoaDons.Where(x => x.NgayShip == null);
            return View(lstDonHangChuaGiao);
        }
        public ActionResult DonHangDaGiao()
        {
            var lstDonHangDaGiao = db.HoaDons.Where(x => x.NgayShip != null);
            return View(lstDonHangDaGiao);
        }

        [HttpGet]
        public ActionResult DuyetDonHang(string id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            HoaDon ddh = db.HoaDons.Single(x => x.MaHD == id);
            if (ddh == null)
            {
                return HttpNotFound();
            }
            ViewBag.LstChiTietDDH = db.CTHoaDons.Where(x => x.MaHD == id);
            return View(ddh);
        }
        [HttpGet]
        public ActionResult XemDonHang(string MaHD)
        {
            if(MaHD == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            HoaDon ddh = db.HoaDons.Single(x => x.MaHD == MaHD);
            if (ddh == null)
            {
                return HttpNotFound();
            }
            ViewBag.LstChiTietDDH = db.CTHoaDons.Where(x => x.MaHD == MaHD);
            return View(ddh);
        }
        [HttpPost]
        public ActionResult XemDonHang(string MaHD,string str)
        {
            HoaDon updateDDH = db.HoaDons.SingleOrDefault(x => x.MaHD == MaHD);
            updateDDH.NgayShip = DateTime.Now;
            db.SaveChanges();
            ViewBag.LstChiTietDDH = db.CTHoaDons.Where(x => x.MaHD == MaHD);
            return View(updateDDH);
        }
    }
}