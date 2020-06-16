using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ShopCar.Model
{
    [MetadataTypeAttribute(typeof(SanPhamMetaData))]
    public partial class SanPham
    {
        internal sealed class SanPhamMetaData
        {

            public string MaSP { get; set; }

            [MinLength(6, ErrorMessage = "Tên SP không dưới 6 kí tự ! ")]
            [Required(ErrorMessage = "Tên SP không được để trống !")]
            public string TenSP { get; set; }
            [Range(0, int.MaxValue, ErrorMessage = "Số lượng không được chứa chữ")]
            [Required(ErrorMessage = "Số lượng không được để trống !")]
            public Nullable<int> SoLuong { get; set; }
            [Range(0, int.MaxValue, ErrorMessage = "Đơn giá không được chứa chữ")]
            [Required(ErrorMessage = "Đơn giá không được để trống !")]

            public Nullable<decimal> DonGia { get; set; }

            public string MoTa { get; set; }
            [Range(0, int.MaxValue, ErrorMessage = "Giá KM không được chứa chữ")]

            public Nullable<decimal> GiaKm { get; set; }
            // [DataType(DataType.ImageUrl)]
            //[DataType(DataType.Url)]

            public string URLAnh { get; set; }

            public string MaLoaiSP { get; set; }
        }
    }
}