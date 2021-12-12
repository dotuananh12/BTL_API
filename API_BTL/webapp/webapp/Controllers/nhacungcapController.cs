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
    public class nhacungcapsController : Controller
    {
        private Model1 db = new Model1();
        string BASE_URI = "http://localhost:54888/api/nhacungcap";
        // GET: nhacungcaps
        public ActionResult Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var getTask = client.GetAsync("");
                getTask.Wait();

                var result = getTask.Result;
                List<nhacungcap> nhacungcap = null;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    nhacungcap = JsonConvert.DeserializeObject<List<nhacungcap>>(data);
                }
                return View(nhacungcap);
            }
        }

        // GET: nhacungcaps/Details/5
        public ActionResult Details(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var getTask = client.GetAsync("http://localhost:54888/api/nhacungcap/Get?id=" + id);
                getTask.Wait();

                var result = getTask.Result;
                nhacungcap emp = null;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    emp = JsonConvert.DeserializeObject<nhacungcap>(data);
                }
                return View(emp);
            }
        }

        // GET: nhacungcaps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: nhacungcaps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,tenncc,email,phone,diachi")] nhacungcap nhacungcap)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BASE_URI);
                    string data = JsonConvert.SerializeObject(nhacungcap);
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

            return View(nhacungcap);
        }

        // GET: nhacungcaps/Edit/5
        public ActionResult Edit(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var getTask = client.GetAsync("http://localhost:54888/api/nhacungcap/get?id=" + id);
                getTask.Wait();

                var result = getTask.Result;
                nhacungcap sp = null;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    sp = JsonConvert.DeserializeObject<nhacungcap>(data);
                }
                return View(sp);
            }
        }


        // POST: nhacungcaps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(nhacungcap l)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BASE_URI);

                    string data = JsonConvert.SerializeObject(l);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                    var putTask = client.PutAsync("http://localhost:54888/api/nhacungcap/update", content);
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

        // GET: nhacungcaps/Delete/5
        public ActionResult Delete(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var getTask = client.GetAsync("GetnhacungcapByID?id=" + id);
                getTask.Wait();

                var result = getTask.Result;
                nhacungcap emp = null;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    emp = JsonConvert.DeserializeObject<nhacungcap>(data);
                }
                return View(emp);
            }
        }

        // POST: nhacungcaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var deleteTask = client.DeleteAsync("Deletenhacungcap?id=" + id);
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
