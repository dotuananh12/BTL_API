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
    public class nhacungcapController : ApiController
    {
        Model1 db = new Model1();
        // GET: api/nhacungcap
        public IEnumerable<nhacungcap> Get()
        {
            var get = db.nhacungcaps.ToList();
            return get;
        }

        // GET: api/nhacungcap/5
        public nhacungcap Get(int id)
        {
            var get = db.nhacungcaps.Where(x => x.id == id).SingleOrDefault();
            return get;
        }
        public IHttpActionResult GetSearch(string search)
        {
            var result = db.nhacungcaps.Where(x => x.tenncc.Contains(search) || search == null).ToList();
            return Ok(result);
        }


        // POST: api/nhacungcap
        public List<nhacungcap> Post([FromBody]nhacungcap add)
        {
            db.nhacungcaps.Add(add);
            db.SaveChanges();
            return db.nhacungcaps.ToList();
        }


        // PUT: api/nhacungcap/5
        [HttpPut]
        [Route("api/nhacungcap/update")]
        public IEnumerable<nhacungcap> Put([FromBody]nhacungcap sua)
        {
            nhacungcap sp = db.nhacungcaps.SingleOrDefault(x => x.id == sua.id);
            sp.tenncc = sua.tenncc;
            sp.email = sua.email;
            sp.phone = sua.phone;
            sp.diachi = sua.diachi;

            db.SaveChanges();
            return db.nhacungcaps.ToList();

        }

        // DELETE: api/nhacungcap/5
        public IEnumerable<nhacungcap> Delete(int id, nhacungcap de)
        {
            nhacungcap em = db.nhacungcaps.FirstOrDefault(x => x.id == id);
            db.nhacungcaps.Remove(em);
            db.SaveChanges();
            return db.nhacungcaps.ToList();
        }
    }
}
