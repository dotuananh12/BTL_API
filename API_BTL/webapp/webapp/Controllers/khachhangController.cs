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
    public class khachhangsController : Controller
    {
        private Model1 db = new Model1();
        string BASE_URI = "http://localhost:54888/api/khachhang";
        // GET: khachhangs
        public ActionResult Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var getTask = client.GetAsync("");
                getTask.Wait();

                var result = getTask.Result;
                List<khachhang> khachhang = null;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    khachhang = JsonConvert.DeserializeObject<List<khachhang>>(data);
                }
                return View(khachhang);
            }
        }

        // GET: khachhangs/Details/5
        public ActionResult Details(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var getTask = client.GetAsync("http://localhost:54888/api/khachhang/Get?id=" + id);
                getTask.Wait();

                var result = getTask.Result;
                khachhang emp = null;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    emp = JsonConvert.DeserializeObject<khachhang>(data);
                }
                return View(emp);
            }
        }

        // GET: khachhangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: khachhangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,tenkh,email,phone,diachi")] khachhang khachhang)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BASE_URI);
                    string data = JsonConvert.SerializeObject(khachhang);
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

            return View(khachhang);
        }

        // GET: khachhangs/Edit/5
        public ActionResult Edit(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var getTask = client.GetAsync("http://localhost:54888/api/khachhang/get?id=" + id);
                getTask.Wait();

                var result = getTask.Result;
                khachhang sp = null;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    sp = JsonConvert.DeserializeObject<khachhang>(data);
                }
                return View(sp);
            }
        }


        // POST: khachhangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(khachhang l)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BASE_URI);

                    string data = JsonConvert.SerializeObject(l);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                    var putTask = client.PutAsync("http://localhost:54888/api/khachhang/update", content);
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

        // GET: khachhangs/Delete/5
        public ActionResult Delete(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var getTask = client.GetAsync("GetkhachhangByID?id=" + id);
                getTask.Wait();

                var result = getTask.Result;
                khachhang emp = null;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    emp = JsonConvert.DeserializeObject<khachhang>(data);
                }
                return View(emp);
            }
        }

        // POST: khachhangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var deleteTask = client.DeleteAsync("Deletekhachhang?id=" + id);
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
