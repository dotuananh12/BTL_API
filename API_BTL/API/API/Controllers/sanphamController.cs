using API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")] // tune to your needs
    [RoutePrefix("")]
    public class sanphamController : ApiController
    {
        Model1 db = new Model1();
        // GET: api/sanpham
        public IEnumerable<sanpham> Get()
        {
            var get = db.sanphams.ToList();
            return get;
        }

        // GET: api/sanpham/5
        //[Route("get-by-id/{id}")]
        public sanpham Get(int id)
        {
            var get = db.sanphams.Where(x => x.id == id).SingleOrDefault();
            return get;
        }
        public IHttpActionResult GetSearch(string search)
        {
            var result = db.sanphams.Where(x => x.tensp.Contains(search) || search == null).ToList();
            return Ok(result);
        }

        // POST: api/sanpham
        public IHttpActionResult Post()
        {
            #region upload file
            if (HttpContext.Current.Request.Files.AllKeys.Any() && HttpContext.Current.Request.Params.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var request = HttpContext.Current.Request;

                var httpPostedFile = request.Files["UploadedImage"];

                if (httpPostedFile != null)
                {
                    // Validate the uploaded image(optional)

                    // Get the complete file path
                    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Image"), httpPostedFile.FileName);
                    // Save the uploaded file to "UploadedFiles" folder
                    httpPostedFile.SaveAs(fileSavePath);
                }
                sanpham sp = new sanpham()
                {
                    tensp = request.Params["tensp"].ToString(),
                    loaisp = string.IsNullOrEmpty(request.Params["loaisp"].ToString()) ? 0
                                    : Convert.ToInt32(request.Params["loaisp"]),
                    ncc = string.IsNullOrEmpty(request.Params["ncc"].ToString()) ? 0
                                    : Convert.ToInt32(request.Params["ncc"]),
                    soluong = string.IsNullOrEmpty(request.Params["soluong"].ToString()) ? 0
                                    : Convert.ToInt32(request.Params["soluong"]),
                    giaban = string.IsNullOrEmpty(request.Params["giaban"].ToString()) ? 0
                                    : Convert.ToInt32(request.Params["giaban"]),
                    gianhap = string.IsNullOrEmpty(request.Params["gianhap"].ToString()) ? 0
                                    : Convert.ToInt32(request.Params["gianhap"]),
                    ngaythem = DateTime.Now,//lay ngày giờ hiện tại,thì đỡ phải điền
                    img = Path.Combine("Image", httpPostedFile.FileName)
                };

                #region lưu dữ liuej
                try
                {
                    db.sanphams.Add(sp);
                    db.SaveChanges();

                    return Ok(sp);
                }
                catch { throw; }
                #endregion
            }
            return Ok();
           
            #endregion

          
        }

        // PUT: api/sanpham/5
        //[HttpPut]
        //[Route("api/sanpham/update")]
        public IEnumerable<sanpham> Put([FromBody]sanpham sua)
        {
            sanpham sp = db.sanphams.SingleOrDefault(x => x.id == sua.id);
            sp.tensp = sua.tensp;
            sp.loaisp = sua.loaisp;
            sp.ncc = sua.ncc;
            sp.soluong = sua.soluong;
            sp.img = sua.img;
            db.Entry(sp).Property(x => x.img).IsModified = false;
            sp.gianhap = sua.gianhap;
            sp.giaban = sua.giaban;
            db.SaveChanges();
            return db.sanphams.ToList();

        }

        //thu nghiem
        //[HttpPut]
        //[Route("api/sanpham/update")]
        //public IHttpActionResult Put(int id)
        //{
        //    #region upload file
        //    if (HttpContext.Current.Request.Files.AllKeys.Any() && HttpContext.Current.Request.Params.AllKeys.Any())
        //    {
        //        // Get the uploaded image from the Files collection
        //        var request = HttpContext.Current.Request;

        //        var httpPostedFile = request.Files["UploadedImage"];

        //        if (httpPostedFile != null)
        //        {
        //            // Validate the uploaded image(optional)

        //            // Get the complete file path
        //            var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Image"), httpPostedFile.FileName);
        //            // Save the uploaded file to "UploadedFiles" folder
        //            httpPostedFile.SaveAs(fileSavePath);
        //        }
        //        sanpham sp = db.sanphams.SingleOrDefault(x => x.id == id);
        //        {
        //            sp.tensp = request.Params["tensp"].ToString();
        //            sp.loaisp = string.IsNullOrEmpty(request.Params["loaisp"].ToString()) ? 0
        //                            : Convert.ToInt32(request.Params["loaisp"]);
        //            sp.ncc = string.IsNullOrEmpty(request.Params["ncc"].ToString()) ? 0
        //                            : Convert.ToInt32(request.Params["ncc"]);
        //            sp.soluong = string.IsNullOrEmpty(request.Params["soluong"].ToString()) ? 0
        //                            : Convert.ToInt32(request.Params["soluong"]);
        //            sp.giaban = string.IsNullOrEmpty(request.Params["giaban"].ToString()) ? 0
        //                            : Convert.ToInt32(request.Params["giaban"]);
        //            sp.gianhap = string.IsNullOrEmpty(request.Params["gianhap"].ToString()) ? 0
        //                             : Convert.ToInt32(request.Params["gianhap"]);
        //            sp.ngaythem = /*string.IsNullOrEmpty(request.Params["ngaythem "].ToString()) ? 0*/
        //                             Convert.ToDateTime(request.Params["ngaythem"]);
        //            sp.img = Path.Combine("Image", httpPostedFile.FileName);
        //        }

        //        #region lưu dữ liuej
        //        try
        //        {
        //            //db.sanphams.Add(sp);
        //            db.SaveChanges();

        //            return Ok(new DTO.ApiResult() { succes = true, message = "sửa thành công ", data = sp });
        //        }
        //        catch { throw; }
        //        #endregion
        //    }
        //    else
        //        return Ok();}
        //    #endregion
        //
        // DELETE: api/sanpham/5


        public IHttpActionResult Delete(int id)
        {
            sanpham em = db.sanphams.FirstOrDefault(x => x.id == id);
            db.sanphams.Remove(em);
            db.SaveChanges();
            return Ok(em);
        }
   
        
    }
}
