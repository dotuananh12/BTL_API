namespace API.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;

    [Table("hoadon")]
    public partial class hoadon
    {
        public int id { get; set; }

        public int? makh { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ngaytao { get; set; }

        public int? masp { get; set; }

        public double? soluong { get; set; }

        [StringLength(50)]
        public string thanhtien { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public virtual khachhang khachhang { get; set; }

        public virtual sanpham sanpham { get; set; }
    }
}
