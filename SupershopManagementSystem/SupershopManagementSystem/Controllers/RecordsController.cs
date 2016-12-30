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
    public class RecordsController : Controller
    {
        private SMSDBContext db = new SMSDBContext();

        // GET: Records
        public ActionResult Index(string searchBy, string search)
        {
            if (Session["SALESMAN"] == null || Session["SALESMAN"].ToString() != "salesman")
                return RedirectToAction("Login", "SalesmenLogin");
            else
            {
                var x = Convert.ToInt32(Session["SALESMANID"]);
                //Salesman salesman = db.Salesmans.Find(x);
                return View(db.Records.Include(r => r.Salesman).Where(a => a.SalesmanId == x).ToList());
                //var records = db.Records.Include(r => r.Salesman);
                //return View(records.ToList());
            }
        }

        // GET: Records/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["SALESMAN"] == null || Session["SALESMAN"].ToString() != "salesman")
                return RedirectToAction("Login", "SalsemenLogin");
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Record record = db.Records.Find(id);
                if (record == null)
                {
                    return HttpNotFound();
                }
                return View(record);
            }
        }

        // GET: Records/Create
        public ActionResult Create()
        {
            if (Session["SALESMAN"] == null || Session["SALESMAN"].ToString() != "salesman")
                return RedirectToAction("Login", "SalesmenLogin");
            else
            {
                var x = Convert.ToInt32(Session["SALESMANID"]);
                Salesman salesman = db.Salesmans.Find(x);
                ViewBag.SalesmanId = new SelectList(db.Salesmans.Where(a => a.SalesmanId == x).ToList(), "SalesmanId", "SalesmanName");
                //ViewBag.SalesmanId = new SelectList(db.Salesmans, "SalesmanId", "SalesmanName");
                //Product product = db.Products.Find("<script>document.getElementById('productId').value;</script>");
                //ViewBag.ProductName = product.ProductName.ToString();
                //ViewBag.ProductPrice = product.ProductPrice;

                Record model = new Record();
                var records = db.Records.Include(r => r.Products);
                model.InvoiceDate = DateTime.Now.ToString();

                return View(model);
            }
        }

        // POST: Records/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceId,InvoiceDate,InvoiceTotal,SalesmanId")] Record record)
        {
            if (Session["SALESMAN"] == null || Session["SALESMAN"].ToString() != "salesman")
                return RedirectToAction("Login", "SalesmenLogin");
            else
            {
                if (ModelState.IsValid)
                {
                    db.Records.Add(record);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.SalesmanId = new SelectList(db.Salesmans, "SalesmanId", "SalesmanName", record.SalesmanId);
                return View(record);
            }
        }

        // GET: Records/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["SALESMAN"] == null || Session["SALESMAN"].ToString() != "salesman")
                return RedirectToAction("Login", "SalesmenLogin");
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Record record = db.Records.Find(id);
                if (record == null)
                {
                    return HttpNotFound();
                }
                ViewBag.SalesmanId = new SelectList(db.Salesmans, "SalesmanId", "SalesmanName", record.SalesmanId);
                return View(record);
            }
        }

        // POST: Records/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceId,InvoiceDate,InvoiceTotal,SalesmanId")] Record record)
        {
            if (Session["SALESMAN"] == null || Session["SALESMAN"].ToString() != "salesman")
                return RedirectToAction("Login", "SalesmenLogin");
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(record).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.SalesmanId = new SelectList(db.Salesmans, "SalesmanId", "SalesmanName", record.SalesmanId);
                return View(record);
            }
        }

        // GET: Records/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["SALESMAN"] == null || Session["SALESMAN"].ToString() != "salesman")
                return RedirectToAction("Login", "SalesmenLogin");
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Record record = db.Records.Find(id);
                if (record == null)
                {
                    return HttpNotFound();
                }
                return View(record);
            }
        }

        // POST: Records/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["SALESMAN"] == null || Session["SALESMAN"].ToString() != "salesman")
                return RedirectToAction("Login", "SalesmenLogin");
            else
            {
                Record record = db.Records.Find(id);
                db.Records.Remove(record);
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
