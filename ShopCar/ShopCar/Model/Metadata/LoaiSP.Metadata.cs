using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopCar.Model
{
    [MetadataTypeAttribute(typeof(LoaiSPMetaData))]
    public partial class LoaiSP
    {
        internal sealed class LoaiSPMetaData
        {
            [Key]
            public string MaLoaiSP { get; set; }

            [StringLength(30, MinimumLength = 2, ErrorMessage = "Tên loại SP không dưới 2 kí tự và không quá 30 kí tự! ")]
            [Required(ErrorMessage = "Tên loại SP không được để trống !")]
            public string TenLoai { get; set; }
            public string MoTa { get; set; }
        }
    }
}