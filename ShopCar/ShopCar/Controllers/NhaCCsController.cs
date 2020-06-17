using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.SqlServer.Server;
using ShopCar.Model;

namespace ShopCar.Controllers
{
    public class NhaCCsController : Controller
    {
        private CarShopEntities db = new CarShopEntities();

        // GET: NhaCCs
        public ActionResult Index()
        {
            return View(db.NhaCCs.ToList());
        }

        // GET: NhaCCs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCC nhaCC = db.NhaCCs.Find(id);
            if (nhaCC == null)
            {
                return HttpNotFound();
            }
            return View(nhaCC);
        }

        // GET: NhaCCs/Create
        public ActionResult Create()
        {
            return View();
        }
        private bool idHasExist(string id)
        {
            NhaCC temp = db.NhaCCs.Find(id);
            if (temp==null)
            {
                return false;
            }
            
            return true;
        }
        private string autoID()
        {
            string id="";
            int dem = db.NhaCCs.ToList().Count();
            while (true)
            {
                if (dem < 10 && dem >= 1)
                {
                    id = "N000" + Convert.ToString(dem + 1);
                }
                else if (dem >= 10 && dem < 100)
                {
                    id = "N00" + Convert.ToString(dem + 1);
                }
                else if (dem >= 100 )
                {
                    id = "N0" + Convert.ToString(dem + 1);
                }
                else if (dem == 0)
                {
                    id = "N0001";
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
        // POST: NhaCCs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NhaCC nhaCC /*[Bind(Include = "MaNCC,TenNCC,DiaChi,SDT,Email")]*/ )
        {
            if (ModelState.IsValid)
            {       
                nhaCC.MaNCC = autoID();                   
                db.NhaCCs.Add(nhaCC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: NhaCCs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCC nhaCC = db.NhaCCs.Find(id);
            if (nhaCC == null)
            {
                return HttpNotFound();
            }
            return View(nhaCC);
        }

        // POST: NhaCCs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNCC,TenNCC,DiaChi,SDT,Email")] NhaCC nhaCC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhaCC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhaCC);
        }

        // GET: NhaCCs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCC nhaCC = db.NhaCCs.Find(id);
            if (nhaCC == null)
            {
                return HttpNotFound();
            }
            return View(nhaCC);
        }

        // POST: NhaCCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhaCC nhaCC = db.NhaCCs.Find(id);
            db.NhaCCs.Remove(nhaCC);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
