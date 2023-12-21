using K_Burns_JewelleryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace K_Burns_JewelleryStore.Controllers
{
    public class ShopController : Controller
    {
        private JewelleryStoreDbContext context = new JewelleryStoreDbContext();
        // GET: Shop
        public ActionResult Index()
        {
            //getting all the products that are not discontinued
            var products = context.Products.Where(p => p.Discontinued==false).ToList();

            foreach (var item in products)
            {
                context.Entry(item).Reload();//refresh entities
            }
            //send all categories apart from Bespoke and Repairs to the Viewbag
            ViewBag.Categories = context.Categories.Where(c => !c.Name.Equals("Bespoke")).Where(c => !c.Name.Equals("Repairs"));

            //send the products to the Products view
            return View("Products", products);
        }

        public ActionResult Products(int? id)
        {
            //getting all the products that are not discontinued and are in a specific category
            var products = context.Products.Where(p => p.Discontinued == false).Where(p => p.CategoryId==id).ToList();

            //also don't forget to send all the categories in a viewbag
            ViewBag.Categories = context.Categories.Where(c => !c.Name.Equals("Bespoke")).Where(c => !c.Name.Equals("Repairs")).ToList();

            return View("Products", products);
        }

        public ActionResult Earrings()
        {
            //find the earrings category
            var earrings = context.Categories.Where(c => c.Name == "Earrings").SingleOrDefault();

            //getting all the products that are not discontinued and are earrings
            var products = context.Products.Where(p => p.Discontinued == false).Where(p => p.CategoryId == earrings.CategoryId).ToList();

            //also don't forget to send all the categories in a viewbag
            ViewBag.Categories = context.Categories.ToList();

            return View("Products", products);
        }

        public ActionResult Bracelets()
        {
            //find the bracelets category
            var bracelets = context.Categories.Where(c => c.Name == "Bracelets").SingleOrDefault();

            //getting all the products that are not discontinued and are bracelets
            var products = context.Products.Where(p => p.Discontinued == false).Where(p => p.CategoryId == bracelets.CategoryId).ToList();

            //also don't forget to send all the categories in a viewbag
            ViewBag.Categories = context.Categories.ToList();

            return View("Products", products);
        }

        public ActionResult Necklesses()
        {
            //find the Necklesses category
            var necklesses = context.Categories.Where(c => c.Name == "Necklesses").SingleOrDefault();

            //getting all the products that are not discontinued and are necklesses
            var products = context.Products.Where(p => p.Discontinued == false).Where(p => p.CategoryId == necklesses.CategoryId).ToList();

            //also don't forget to send all the categories in a viewbag
            ViewBag.Categories = context.Categories.ToList();

            return View("Products", products);
        }

        public ActionResult Rings()
        {
            //find the Rings category
            var rings = context.Categories.Where(c => c.Name == "Rings").SingleOrDefault();

            //getting all the products that are not discontinued and are bracelets
            var products = context.Products.Where(p => p.Discontinued == false).Where(p => p.CategoryId == rings.CategoryId).ToList();

            //also don't forget to send all the categories in a viewbag
            ViewBag.Categories = context.Categories.ToList();

            return View("Products", products);
        }

        public ActionResult Bespoke()
        {
            return View("Contact");
        }

        public ActionResult Repairs()
        {
            return View("Contact");
        }

        public ActionResult Product(int? id)
        {
            //if id is null return a bad request error
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //find product by id
            Product product = context.Products.Find(id);

            //if product is not in the database return not found error
            if (product == null)
            {
                return HttpNotFound();
            }

            //also don't forget t osend all the categories in a viewbag
            ViewBag.Categories = context.Categories.ToList();

            return View(product);
        }
    }
}