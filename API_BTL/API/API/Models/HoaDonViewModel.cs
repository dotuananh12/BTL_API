using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class HoaDonViewModel
    {
        public int id { get; set; }

        public string tenkh { get; set; }

        public DateTime? ngaytao { get; set; }

        public string tensp { get; set; }

        public double? soluong { get; set; }

        public string thanhtien { get; set; }
    }
}