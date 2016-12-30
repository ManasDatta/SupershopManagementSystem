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
    public class CategoriesController : Controller
    {
        private SMSDBContext db = new SMSDBContext();

        // GET: Categories
        public ActionResult Index(string search)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
                return View(db.Categories.Where(x => x.CategoryName.StartsWith(search) || search == null).ToList());
                //return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
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
                Category category = db.Categories.Find(id);
                if (category == null)
                {
                    return HttpNotFound();
                }
                return View(category);
            }
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
                return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryName")] Category category)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
            {
                if (ModelState.IsValid)
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(category);
            }
        }

        // GET: Categories/Edit/5
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
                Category category = db.Categories.Find(id);
                if (category == null)
                {
                    return HttpNotFound();
                }
                return View(category);
            }
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName")] Category category)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(category);
            }
        }

        // GET: Categories/Delete/5
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
                Category category = db.Categories.Find(id);
                if (category == null)
                {
                    return HttpNotFound();
                }
                return View(category);
            }
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ADMIN"] == null || Session["ADMIN"].ToString() != "admin")
                return RedirectToAction("Login", "AdminLogin");
            else
            {
                Category category = db.Categories.Find(id);
                db.Categories.Remove(category);
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
