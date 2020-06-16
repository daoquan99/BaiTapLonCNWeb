using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopCar.Model
{
    [MetadataTypeAttribute(typeof(NhaCCMetaData))]
    public partial class NhaCC
    {
        internal sealed class NhaCCMetaData
        {
            public string MaNCC { get; set; }
            [Required(ErrorMessage = "Tên nhà cung cấp không được để trống !")]
            public string TenNCC { get; set; }
            [Required(ErrorMessage = "Địa Chỉ không được để trống !")]
            public string DiaChi { get; set; }
            [Required(ErrorMessage = "SĐT không được để trống !")]
            // [StringLength(11, MinimumLength = 6, ErrorMessage = "Số kí tự cho SĐT thuộc [6-11] ")]
            //[Range(0, int.MaxValue, ErrorMessage = "Số điện thoại không được chứa chữ")]
            [DataType(DataType.PhoneNumber)]
            //  [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại chứa 10 chữ số!")]
            [Phone]
            public string SDT { get; set; }

            [Required(ErrorMessage = "Email không được để trống")]
            [DataType(DataType.EmailAddress)]
            [MaxLength(30)]
            [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "vui lòng nhập email đúng!")]
            public string Email { get; set; }
        }
    }
}