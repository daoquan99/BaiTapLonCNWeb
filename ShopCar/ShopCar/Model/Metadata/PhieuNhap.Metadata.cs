using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopCar.Model
{
    [MetadataTypeAttribute(typeof(PhieuNhapMetaData))]
    public partial class PhieuNhap
    {
        internal sealed class PhieuNhapMetaData
        {
            public string MaPN { get; set; }
            [Required(ErrorMessage = "Ngày cung cấp không được để trống !")]
            [DataType(DataType.Date)]
            public Nullable<System.DateTime> NgayNhap { get; set; }
            public string MaNCC { get; set; }
        }
    }
}