using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")] // tune to your needs
    [RoutePrefix("")]
    public class hoadonController : ApiController
    {
        Model1 db = new Model1();
        // GET: api/hoadon
        public IEnumerable<HoaDonViewModel> Get()
        {
            //var get = db.hoadons.ToList();
            //return get;
            var query = from c in db.hoadons
                            // join cc in db.NewsCategory on c.CategoryID equals cc.ID
                        select new HoaDonViewModel()
                        {
                            id = c.id,
                            tenkh = c.khachhang.tenkh,
                            ngaytao = c.ngaytao,
                            tensp=c.sanpham.tensp,
                            soluong = c.soluong,
                            thanhtien = c.thanhtien,                       
                        };
            return query.OrderBy(x => x.id).ToList();
        }

        // GET: api/hoadon/5
        public HoaDonViewModel Get(int id)
        {
            //var get = db.hoadons.Where(x => x.id == id).SingleOrDefault();
            //return get;
            var query = from c in db.hoadons
                            // join cc in db.NewsCategory on c.CategoryID equals cc.ID
                        select new HoaDonViewModel()
                        {
                            id = c.id,
                            tenkh = c.khachhang.tenkh,
                            ngaytao = c.ngaytao,
                            tensp = c.sanpham.tensp,
                            soluong = c.soluong,
                            thanhtien = c.thanhtien,
                        };
            return query.Where(x => x.id==id).SingleOrDefault();
        }

        //public IHttpActionResult GetSearch(string search)
        //{
        //    var result = db.hoadons.Where(x => x.tenkh.Contains(search) || search == null).ToList();
        //    return Ok(result);
        //}


        // POST: api/hoadon
        public List<hoadon> Post([FromBody]hoadon add)
        {
            db.hoadons.Add(add);
            db.SaveChanges();
            return db.hoadons.ToList();
        }


        // PUT: api/hoadon/5
        [HttpPut]
        [Route("api/hoadon/update")]
        public IEnumerable<hoadon> Put([FromBody]hoadon sua)
        {
            hoadon sp = db.hoadons.SingleOrDefault(x => x.id == sua.id);
            sp.makh = sua.makh;
            sp.ngaytao = sua.ngaytao;
            sp.sanpham = sua.sanpham;
            sp.soluong = sua.soluong;
            sp.thanhtien = sua.thanhtien;

            db.SaveChanges();
            return db.hoadons.ToList();

        }

        // DELETE: api/hoadon/5
        public IEnumerable<hoadon> Delete(int id, hoadon de)
        {
            hoadon em = db.hoadons.FirstOrDefault(x => x.id == id);
            db.hoadons.Remove(em);
            db.SaveChanges();
            return db.hoadons.ToList();
        }
    }
}
