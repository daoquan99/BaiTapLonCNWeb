using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopCar.Model
{
    [MetadataTypeAttribute(typeof(KhachHangMetaData))]
    public partial class Admin
    {
        internal sealed class KhachHangMetaData
        {
            public string MaAdmin { get; set; }
            [Required(ErrorMessage = "Tên tài khoản không được để trống !")]
            public string UserAd { get; set; }
            [Required(ErrorMessage = "Email không được để trống !")]
            [DataType(DataType.EmailAddress)]
            [MaxLength(30)]
            [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "vui lòng nhập email đúng!")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Mật khẩu tài khoản không được để trống !")]
            public string Pass { get; set; }
        }
    }
}