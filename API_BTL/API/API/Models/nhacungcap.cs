namespace API.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;

    [Table("nhacungcap")]
    public partial class nhacungcap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public nhacungcap()
        {
            sanphams = new HashSet<sanpham>();
        }

        public int id { get; set; }

        [StringLength(50)]
        public string tenncc { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(50)]
        public string phone { get; set; }

        [StringLength(50)]
        public string diachi { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sanpham> sanphams { get; set; }
    }
}
