using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopCar.Model
{
    public class ItemGioHang
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public string URLAnh { get; set; }
        public decimal ThanhTien { get; set; }
        
       
        public ItemGioHang(string maSP)
        {
            using(CarShopEntities db=new CarShopEntities())
            {   
                this.MaSP = maSP;
                SanPham sp = db.SanPhams.Single(x => x.MaSP == maSP);
                this.TenSP = sp.TenSP;
                this.DonGia = sp.DonGia.Value;
                this.URLAnh = sp.URLAnh;
                this.SoLuong = 1;
                this.ThanhTien = this.SoLuong * this.DonGia;
            }
        }
        public ItemGioHang(string maSP,int soLuongSP)
        {
            using (CarShopEntities db = new CarShopEntities())
            {
                this.MaSP = maSP;
                SanPham sp = db.SanPhams.Single(x => x.MaSP == maSP);
                this.TenSP = sp.TenSP;
                this.DonGia = sp.DonGia.Value;
                this.URLAnh = sp.URLAnh;
                this.SoLuong = soLuongSP;
                this.ThanhTien = this.SoLuong * this.DonGia;
            }
        }
        public ItemGioHang() { }
    }
}