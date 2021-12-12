using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using webapp.Models;

namespace webapp.Controllers
{
    public class loaisanphamsController : Controller
    {
        private Model1 db = new Model1();
        string BASE_URI = "http://localhost:54888/api/loaisanpham";
        // GET: loaisanphams
        public ActionResult Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var getTask = client.GetAsync("");
                getTask.Wait();

                var result = getTask.Result;
                List<loaisanpham> loaisanpham = null;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    loaisanpham = JsonConvert.DeserializeObject<List<loaisanpham>>(data);
                }
                return View(loaisanpham);
            }
        }

        // GET: loaisanphams/Details/5
        public ActionResult Details(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var getTask = client.GetAsync("http://localhost:54888/api/loaisanpham/Get?id=" + id);
                getTask.Wait();

                var result = getTask.Result;
                loaisanpham emp = null;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    emp = JsonConvert.DeserializeObject<loaisanpham>(data);
                }
                return View(emp);
            }
        }

        // GET: loaisanphams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: loaisanphams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,tenloai,ghichu")] loaisanpham loaisanpham)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BASE_URI);
                    string data = JsonConvert.SerializeObject(loaisanpham);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                    var postTask = client.PostAsync("", content);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(loaisanpham);
        }

        // GET: loaisanphams/Edit/5
        public ActionResult Edit(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var getTask = client.GetAsync("http://localhost:54888/api/loaisanpham/get?id=" + id);
                getTask.Wait();

                var result = getTask.Result;
                loaisanpham sp = null;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    sp = JsonConvert.DeserializeObject<loaisanpham>(data);
                }
                return View(sp);
            }
        }


        // POST: loaisanphams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(loaisanpham l)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BASE_URI);

                    string data = JsonConvert.SerializeObject(l);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                    var putTask = client.PutAsync("http://localhost:54888/api/loaisanpham/update", content);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(l);
        }

        // GET: loaisanphams/Delete/5
        public ActionResult Delete(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var getTask = client.GetAsync("GetloaisanphamByID?id=" + id);
                getTask.Wait();

                var result = getTask.Result;
                loaisanpham emp = null;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    emp = JsonConvert.DeserializeObject<loaisanpham>(data);
                }
                return View(emp);
            }
        }

        // POST: loaisanphams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var deleteTask = client.DeleteAsync("Deleteloaisanpham?id=" + id);
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