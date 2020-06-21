using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopCar.Model
{
    [MetadataTypeAttribute(typeof(KhachHangMetaData))]
    public partial class KhachHang
    {
        internal sealed class KhachHangMetaData
        {
            public string MaKH { get; set; }
            [Required(ErrorMessage = "Tên khách hàng không được để trống !")]
            public string TenKH { get; set; }
            [Required(ErrorMessage = "SĐT không được để trống !")]
            [DataType(DataType.PhoneNumber)]
            [Phone]
            public string SDT { get; set; }
            [Required(ErrorMessage = "Giới tính không được để trống !")]
            public string GioiTinh { get; set; }
            public Nullable<int> Tuoi { get; set; }
            [Required(ErrorMessage = "Địa Chỉ không được để trống !")]
            public string DiaChi { get; set; }
            [Required(ErrorMessage = "Tên tài khoản không được để trống !")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "Email không được để trống")]
            [DataType(DataType.EmailAddress)]
            [MaxLength(30)]
            [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "vui lòng nhập email đúng!")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Mật khẩu không được không được để trống !")]
            public string Pass { get; set; }
            public Nullable<bool> IsVip { get; set; }
        }
    }
}