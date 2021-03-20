using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFront2.DATA.EF;

namespace StoreFront2.UI.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products1 = db.Products1.Include(p => p.Department).Include(p => p.Vendor);
            return View(products1.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products1.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.DepID = new SelectList(db.Departments1, "DepID", "DepName");
            ViewBag.VendorID = new SelectList(db.Vendors1, "VendorID", "VenName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProdName,Description,UnitPrice,UnitCost,ImgURL,DepID,VendorID,SKU,UPC")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Products1.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepID = new SelectList(db.Departments1, "DepID", "DepName", products.DepID);
            ViewBag.VendorID = new SelectList(db.Vendors1, "VendorID", "VenName", products.VendorID);
            return View(products);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products1.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepID = new SelectList(db.Departments1, "DepID", "DepName", products.DepID);
            ViewBag.VendorID = new SelectList(db.Vendors1, "VendorID", "VenName", products.VendorID);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProdName,Description,UnitPrice,UnitCost,ImgURL,DepID,VendorID,SKU,UPC")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepID = new SelectList(db.Departments1, "DepID", "DepName", products.DepID);
            ViewBag.VendorID = new SelectList(db.Vendors1, "VendorID", "VenName", products.VendorID);
            return View(products);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products1.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products1.Find(id);
            db.Products1.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
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
