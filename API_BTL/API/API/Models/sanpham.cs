namespace API.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;

    [Table("sanpham")]
    public partial class sanpham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sanpham()
        {
            hoadons = new HashSet<hoadon>();
        }

        public int id { get; set; }

        [StringLength(50)]
        public string tensp { get; set; }

        public int? loaisp { get; set; }

        public double? soluong { get; set; }

        [StringLength(250)]
        public string img { get; set; }

        public double? gianhap { get; set; }

        public double? giaban { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ngaythem { get; set; }

        public int? ncc { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hoadon> hoadons { get; set; }

        public virtual loaisanpham loaisanpham { get; set; }

        public virtual nhacungcap nhacungcap { get; set; }
    }
}
