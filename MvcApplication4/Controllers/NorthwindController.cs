using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication4.Models;

namespace MvcApplication4.Controllers
{
    public class NorthwindController : Controller
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NORTHWND;Integrated Security=True";

        public ActionResult Categories()
        {
            NorthwindDb db = new NorthwindDb(connectionString);
            IEnumerable<Category> categories = db.GetCategories();
            return View(categories);
        }

        public ActionResult Products(int categoryId)
        {
            NorthwindDb db = new NorthwindDb(connectionString);
            IEnumerable<Product> products = db.GetProductsByCategoryId(categoryId);
            return View(products);
        }

        public ActionResult Customers()
        {
            var db = new NorthwindDb(connectionString);
            var customers = db.GetCustomers();
            return View(customers);
        }

        public ActionResult CustomerDetails(string customerId)
        {
            var db = new NorthwindDb(connectionString);
            Customer customer = db.GetCustomerById(customerId);
            return View(customer);
        }

        public ActionResult Search()
        {
            return View();
        }


        public void SearchProducts(string searchTerm)
        {
            Response.Write("<h1>You searched for " + searchTerm + "</h1>");
        }

        public void Foo(string var1, int? var2, bool var3)
        {
            Response.Write("<h1>var1: " + var1 + " var2: " + var2 + " var3: " + var3);
        }

    }
}
