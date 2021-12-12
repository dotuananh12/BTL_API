using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using webapp.Models;

namespace webapp.Controllers
{
    public class sanphamsController : Controller
    {
        Model1 db = new Model1();
        string BASE_URI = "http://localhost:54888/api/sanpham";
        // GET: sanphams
        public ActionResult Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var getTask = client.GetAsync("");
                getTask.Wait();

                var result = getTask.Result;
                List<sanpham> sanpham = null;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    sanpham = JsonConvert.DeserializeObject<List<sanpham>>(data);
                }
                ViewBag.maloai = new SelectList(db.loaisanphams, "id", "tenloai");
                ViewBag.ncc = new SelectList(db.nhacungcaps, "id", "tenncc");
                return View(sanpham);

            }
        }

        // GET: sanphams/Details/5
        public ActionResult Details(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var getTask = client.GetAsync("http://localhost:54888/api/sanpham/Get?id=" + id);
                getTask.Wait();

                var result = getTask.Result;
                sanpham emp = null;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    emp = JsonConvert.DeserializeObject<sanpham>(data);
                }
                return View(emp);
            }
        }

        // GET: sanphams/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: sanphams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(sanpham sanpham, HttpPostedFileBase filename)
        {
            //HttpPostedFileBase them anh bag cau nay
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                Stream inputStream = filename.InputStream;//them anh
              //  MemoryStream memoryStream = inputStream as MemoryStream;

                ///
                var formContent = new MultipartFormDataContent//su dung form data thi dung cai nay thui,
                {
                    // Send form text values here
                    {new StringContent(sanpham.tensp),"tensp"},
                    {new StringContent(sanpham.loaisp.ToString()),"loaisp"},
                    {new StringContent(sanpham.ncc.ToString()),"ncc"},
                    {new StringContent(sanpham.soluong.ToString()),"soluong"},
                    {new StringContent(sanpham.giaban.ToString()),"giaban" },
                    {new StringContent(sanpham.gianhap.ToString()),"gianhap"},
                    {new StringContent(sanpham.ngaythem.ToString()),"ngaythem"},
                    // Send Image Here
                    {new StreamContent(inputStream),"UploadedImage",filename.FileName}//them ah
                };
                ///
                var postTask = client.PostAsync("", formContent);
                postTask.Wait();
                var result = postTask.Result;
                return RedirectToAction("index");
            }

        }

        // GET: sanphams/Edit/5
        public ActionResult Edit(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var getTask = client.GetAsync("http://localhost:54888/api/sanpham/Get?id=" + id);
                getTask.Wait();

                var result = getTask.Result;
                sanpham sp = null;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    sp = JsonConvert.DeserializeObject<sanpham>(data);
                }
                return View(sp);
            }
        }


        // POST: sanphams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(sanpham sanpham)
        {
            if (ModelState.IsValid)
            {

                string BASE_URI = "http://localhost:54888/api/sanpham";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BASE_URI);

                    string data = JsonConvert.SerializeObject(sanpham);//chuyen object về json thì mới sửa dc
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    var putTask = client.PutAsync("http://localhost:54888/api/sanpham/update", content);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(sanpham);
        }

        // GET: sanphams/Delete/5
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var getTask = client.GetAsync("http://localhost:54888/api/sanpham/Get?id=" + id);
                getTask.Wait();

                var result = getTask.Result;
                sanpham emp = null;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    emp = JsonConvert.DeserializeObject<sanpham>(data);
                }
                return View(emp);
            }
        }

        // POST: sanphams/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var deleteTask = client.DeleteAsync("http://localhost:54888/api/sanpham/delete" + id);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}