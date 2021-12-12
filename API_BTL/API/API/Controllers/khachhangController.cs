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
    public class khachhangController : ApiController
    {
        Model1 db = new Model1();
        // GET: api/khachhang
        public IEnumerable<khachhang> Get()
        {
            var get = db.khachhangs.ToList();
            return get;
        }

        // GET: api/khachhang/5
        public khachhang Get(int id)
        {
            var get = db.khachhangs.Where(x => x.id == id).SingleOrDefault();
            return get;
        }
        public IHttpActionResult GetSearch(string search)
        {
            var result = db.khachhangs.Where(x => x.tenkh.Contains(search) || search == null).ToList();
            return Ok(result);
        }


        // POST: api/khachhang
        public List<khachhang> Post([FromBody]khachhang add)
        {
            db.khachhangs.Add(add);
            db.SaveChanges();
            return db.khachhangs.ToList();
        }


        // PUT: api/khachhang/5
        [HttpPut]
        [Route("api/khachhang/update")]
        public IEnumerable<khachhang> Put([FromBody]khachhang sua)
        {
            khachhang sp = db.khachhangs.SingleOrDefault(x => x.id == sua.id);
            sp.tenkh = sua.tenkh;
            sp.email = sua.email;
            sp.phone = sua.phone;
            sp.diachi = sua.diachi;

            db.SaveChanges();
            return db.khachhangs.ToList();

        }

        // DELETE: api/khachhang/5
        public IEnumerable<khachhang> Delete(int id, khachhang de)
        {
            khachhang em = db.khachhangs.FirstOrDefault(x => x.id == id);
            db.khachhangs.Remove(em);
            db.SaveChanges();
            return db.khachhangs.ToList();
        }
    }
}
