using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssignmentWebApi.Models;
using System.Net.Http;
using PagedList;

namespace AssignmentWebApi.Controllers
{
    public class FetchProductsController : Controller
    {
        ProductEntities db = new ProductEntities();
        // GET: FetchProducts
        public ActionResult Index(string sortOrder, int? page)
        {
            try
            {
                
                string Domain = Request.Url.Authority;
                //string domainName = HttpContext.CurrentHandler.ProcessRequest.Url.GetLeftPart(UriPartial.Authority);
                //string port = Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);

                IEnumerable<ProductView> product = null;
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(60);
                client.BaseAddress = new Uri("http://" + Request.Url.Authority + "/api/");
                var responseTask = client.GetAsync("product");
                responseTask.Wait();
                var result = responseTask.Result;


                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductView>>();
                    readTask.Wait();

                    product = readTask.Result;
                }
                else
                {

                    product = Enumerable.Empty<ProductView>();

                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                }

                ViewBag.pname = String.IsNullOrEmpty(sortOrder) ? "pname_desc" : "";
                ViewBag.price = sortOrder == "Price" ? "price_desc" : "Price";


                switch (sortOrder)
                {
                    case "pname_desc":
                        product = product.OrderByDescending(s => s.pname);
                        break;
                    case "Price":
                        product = product.OrderBy(s => s.price);
                        break;
                    case "price_desc":
                        product = product.OrderByDescending(s => s.price);
                        break;
                    default:
                        product = product.OrderBy(s => s.pname);
                        break;
                }

                int pageSize = 5;
                int pageNumber = (page ?? 1);

                return View(product.ToPagedList(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                Logger.CreateLog(ex);                
                return View("Error");
            }
        }
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Logger.CreateLog(ex);
                return View("Error");

            }
        }

        [HttpPost]
        public ActionResult Create(ProductView product)
        {
            try
            {
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(60);
                client.BaseAddress = new Uri("http://" + Request.Url.Authority + "/api/" + "product/");
                var postTask = client.PostAsJsonAsync<ProductView>("product", product);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                
                return View(product);
            }
            catch (Exception ex)
            {
                Logger.CreateLog(ex);
                return View("Error");

            }
        }


        public ActionResult Edit(int id)
        {
            try
            {
                ProductView prod = null;
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(60);
                client.BaseAddress = new Uri("http://" + Request.Url.Authority + "/api/");
                var getTask = client.GetAsync("product?id=" + id);
                getTask.Wait();
                var result = getTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<ProductView>();
                    read.Wait();
                    prod = read.Result;

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                }
                return View(prod);
            }
            catch (Exception ex)
            {
                Logger.CreateLog(ex);
                return View("Error");
            }
        }

        [HttpPost]

        public ActionResult Edit(ProductView product)
        {
            try
            {
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(60);
                client.BaseAddress = new Uri("http://" + Request.Url.Authority + "/api/");
                var putTask = client.PutAsJsonAsync<ProductView>("product", product);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                Logger.CreateLog(ex);
                return View("Error");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                ProductView product = null;
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(60);
                client.BaseAddress = new Uri("http://" + Request.Url.Authority + "/api/");
                var getTask = client.GetAsync("product?id=" + id);
                getTask.Wait();
                var result = getTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<ProductView>();
                    read.Wait();
                    product = read.Result;

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                Logger.CreateLog(ex);
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Delete(ProductView product)
        {
            try
            {
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(60);
                client.BaseAddress = new Uri("http://" + Request.Url.Authority + "/api/");
                var deleteTask = client.DeleteAsync("product/" + product.id);
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                Logger.CreateLog(ex);
                return View("Error");
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                ProductView product = null;
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(60);
                client.BaseAddress = new Uri("http://" + Request.Url.Authority + "/api/");
                var getIdTask = client.GetAsync("Product/" + id);
                var result = getIdTask.Result;
                getIdTask.Wait();
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<ProductView>();
                    read.Wait();
                    product = read.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                Logger.CreateLog(ex);
                return View("Error");
            }
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
