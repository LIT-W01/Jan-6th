using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication4.Models;

namespace MvcApplication4.Controllers
{
    public class FoodController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Add(string name, string address)
        {
            var db = new FoodDb(@"Data Source=.\SQLEXPRESS;Initial Catalog=Food;Integrated Security=True");
            db.AddCustomer(name, address);
            return RedirectToAction("Index");
        }

    }
}
