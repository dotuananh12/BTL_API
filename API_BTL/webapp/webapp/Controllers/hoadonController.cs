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
    public class hoadonsController : Controller
    {
        private Model1 db = new Model1();
        string BASE_URI = "http://localhost:54888/api/hoadon";
        // GET: hoadons
        public ActionResult Index()
        {
            using (var client = new HttpClient())// Gửi yêu cầu HTTP den may chu api
            {
                client.BaseAddress = new Uri(BASE_URI);//Gửi yêu cầu HTTP den may chu api

                var getTask = client.GetAsync("");// gửi yêu cầu HTTP GET một cách không đồng bộ,tom lai no gui request get cho api de no chay chuong trinh
                getTask.Wait();//

                var result = getTask.Result;
                List<HoaDonViewModel> hoadon = null;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;//đọc mảng json 
                    hoadon = JsonConvert.DeserializeObject<List<HoaDonViewModel>>(data);//DeserializeObject là chuyển json về kiểu object
                }
               
                return View(hoadon);
            }
        }

        // GET: hoadons/Details/5
        public ActionResult Details(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var getTask = client.GetAsync("http://localhost:54888/api/hoadon/Get?id=" + id);
                getTask.Wait();

                var result = getTask.Result;
                HoaDonViewModel emp = null;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    emp = JsonConvert.DeserializeObject<HoaDonViewModel>(data);//tuong tự
                }
                return View(emp);
            }
        }

        // GET: hoadons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: hoadons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,makh,ngaytao,masp,soluong,thanhtien")] hoadon hoadon)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BASE_URI);
                    string data = JsonConvert.SerializeObject(hoadon);//SerializeObject chuyển đối tượng sang chuỗi json
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");//them

                    var postTask = client.PostAsync("", content);//them
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
           
            return View(hoadon);
        }

        // GET: hoadons/Edit/5
        public ActionResult Edit(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var getTask = client.GetAsync("http://localhost:54888/api/hoadon/get?id=" + id);
                getTask.Wait();

                var result = getTask.Result;
                hoadon sp = null;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    sp = JsonConvert.DeserializeObject<hoadon>(data);
                }
                return View(sp);
            }
        }


        // POST: hoadons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(hoadon l)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BASE_URI);

                    string data = JsonConvert.SerializeObject(l);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                    var putTask = client.PutAsync("http://localhost:54888/api/hoadon/update", content);
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

        // GET: hoadons/Delete/5
        public ActionResult Delete(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var getTask = client.GetAsync("GethoadonByID?id=" + id);
                getTask.Wait();

                var result = getTask.Result;
                hoadon emp = null;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    emp = JsonConvert.DeserializeObject<hoadon>(data);
                }
                return View(emp);
            }
        }

        // POST: hoadons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);

                var deleteTask = client.DeleteAsync("Deletehoadon?id=" + id);
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
