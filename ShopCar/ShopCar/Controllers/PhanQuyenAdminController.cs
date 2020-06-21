using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopCar.Model;

namespace ShopCar.Controllers
{
    [Authorize(Roles = "QuanTriVien")]
    public class PhanQuyenAdminController : Controller
    {
        CarShopEntities db = new CarShopEntities();
        [HttpGet]
        public ActionResult Index()
        {
            var lstAdmin = db.Admins;
            ViewBag.lstQuyen = db.QuyenAds;
            return View(lstAdmin);
        }

        public ActionResult XoaTaiKhoaAdmin(string MaAdmin)
        {
            Admin ad = db.Admins.Single(x => x.MaAdmin == MaAdmin);
            if (ad == null)
            {
                return HttpNotFound();
            }
            var quyenAd = db.QuyenAds.Where(x => x.MaAdmin == MaAdmin);
            db.QuyenAds.RemoveRange(quyenAd);
            db.Admins.Remove(ad);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult SuaTaiKhoanAdmin(string MaAdmin)
        {
            Admin ad = db.Admins.SingleOrDefault(x => x.MaAdmin == MaAdmin);
            if (ad == null)
            {
                return HttpNotFound();
            }
            ViewBag.lstQuyen = db.Quyens;
            return View(ad);
        }
        [HttpPost]
        public ActionResult SuaTaiKhoanAdmin(Admin ad, string[] MaQuyen)
        {
            List<QuyenAd> quyenAD = db.QuyenAds.Where(x => x.MaAdmin == ad.MaAdmin).ToList();
            QuyenAd newQuyenAD = new QuyenAd();

            foreach (string item in MaQuyen)
            {
                if(XetQuyen(quyenAD,item)==true)
                {
                    newQuyenAD.MaAdmin = ad.MaAdmin;
                    newQuyenAD.MaQuyen = item;
                    db.QuyenAds.Add(newQuyenAD);
                    db.SaveChanges();
                }   
            }
            return RedirectToAction("Index");
        }

        private bool XetQuyen(List<QuyenAd> quyenAD, string maQuyen)
        {
            foreach (var quyen in quyenAD)
            {
                if (quyen.MaQuyen == maQuyen)
                    return false;
            }

            return true;
        }
    }
}