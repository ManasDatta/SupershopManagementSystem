using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SMSDataLayer;

namespace SupershopManagementSystem.Controllers
{
    public class ProductsController : Controller
    {
        private SMSDBContext db = new SMSDBContext();

        // GET: Products
      public ActionResult Index(string searchBy, string search)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
            {
                if (searchBy == "Pname")
                {
                    return View(db.Products.Include(p => p.Category).Where(x => x.ProductName.StartsWith(search) || search == null).ToList());
                }
                else
                {
                    return View(db.Products.Include(p => p.Category).Where(x => x.Category.CategoryName.ToString().StartsWith(search) || search == null).ToList());
                }
                //return View(db.Products.Include(p => p.Category).Where(x => x.ProductName.StartsWith(search) || search == null).ToList());
                //var products = db.Products.Include(p => p.Category);
                //return View(products.ToList());
            }
        }
        
        // GET: Products/Details/5
       public ActionResult Details(int? id)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
                return View();
            }
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,ProductPrice,ProductQuantity,CategoryId")] Product product)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
                return View(product);
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
                return View(product);
            }
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,ProductPrice,ProductQuantity,CategoryId")] Product product)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
                return View(product);
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
            {
                Product product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
