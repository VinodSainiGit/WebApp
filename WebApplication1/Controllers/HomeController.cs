using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
      

        List<Product> ProductList;
        public IActionResult Index()
        {
            createList();
            addToList(1, "Jack");
            addToList(2, "Rachel");
            addToList(3, "Joey");
            addToList(4, "Monica");
            addToList(5, "Chandler");
            addToList(6, "Pheobe");
            StoreInSession(ProductList);
         List<Product>result=   FetchFromSession();
           // ViewBag["List"] = result;
            return View(result);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void createList()
        {
            ProductList = new List<Product>();
        }
        public void addToList(int id, string Pname)
        {
            Product pobj = new Product();
            pobj.id = id;
            pobj.ProductName = Pname;
            ProductList.Add(pobj);


        }
        public void StoreInSession(List<Product> list)
        {
            string json;
            byte[] arry;
           
            bool isavailable = HttpContext.Session.TryGetValue("ProductList", out arry);
            if (isavailable == false)
            {
                json = JsonConvert.SerializeObject(list);
               HttpContext.Session.SetString("ProductList", json);

            }
        }
        public List<Product> FetchFromSession()
        {
            byte[] arry;
            List<Product> ProductList;
            string json;
            bool isavailable = HttpContext.Session.TryGetValue("ProductList", out arry);
            if (isavailable)
            {
                ProductList = new List<Product>();
                json = HttpContext.Session.GetString("ProductList");
                ProductList = JsonConvert.DeserializeObject<List<Product>>(json);
                return ProductList;

            }
            else
                return null;
        }
    }
}


       