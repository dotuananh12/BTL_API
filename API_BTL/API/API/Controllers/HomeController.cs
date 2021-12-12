using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    public class HomeController : Controller
    {
        Model1 db = new Model1();

        public ActionResult Index()
        {
            ViewBag.slsp = db.sanphams.Count();
            ViewBag.sllsp = db.loaisanphams.Count();
            ViewBag.slncc = db.nhacungcaps.Count();
            ViewBag.slkh = db.khachhangs.Count();
            ViewBag.slhd = db.hoadons.Count();
           
            return View();
        }
    }
}
