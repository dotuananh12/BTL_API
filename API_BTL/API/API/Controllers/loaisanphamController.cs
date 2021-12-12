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
    public class loaisanphamController : ApiController
    {
        
        Model1 db = new Model1();
        // GET: api/loaisanpham
        public IEnumerable<loaisanpham> Get()
        {
            var get = db.loaisanphams.ToList();
            return get;
        }

        // GET: api/loaisanpham/5
        public loaisanpham Get(int id)
        {
            var get = db.loaisanphams.Where(x => x.id == id).SingleOrDefault();
            return get;
        }
        public IHttpActionResult GetSearch(string search)
        {
            var result = db.loaisanphams.Where(x => x.tenloai.Contains(search) || search == null).ToList();
            return Ok(result);
        }

        // POST: api/loaisanpham
        public List<loaisanpham> Post([FromBody]loaisanpham add)
        {
            db.loaisanphams.Add(add);
            db.SaveChanges();
            return db.loaisanphams.ToList();
        }

        // PUT: api/loaisanpham/5
        [HttpPut]
        [Route("api/loaisanpham/update")]
        public IEnumerable<loaisanpham> Put([FromBody]loaisanpham sua)
        {
            loaisanpham sp = db.loaisanphams.SingleOrDefault(x => x.id == sua.id);
            sp.tenloai = sua.tenloai;
            sp.ghichu = sua.ghichu;
            
            db.SaveChanges();
            return db.loaisanphams.ToList();

        }

        // DELETE: api/loaisanpham/5
        public IEnumerable<loaisanpham> Delete(int id, loaisanpham de)
        {
            loaisanpham em = db.loaisanphams.FirstOrDefault(x => x.id == id);
            db.loaisanphams.Remove(em);
            db.SaveChanges();
            return db.loaisanphams.ToList();
        }
    }
}
