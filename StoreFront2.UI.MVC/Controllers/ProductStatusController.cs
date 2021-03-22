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
    public class ProductStatusController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: ProductStatus
        public ActionResult Index()
        {
            var productStatus1 = db.ProductStatus1.Include(p => p.Location).Include(p => p.Order).Include(p => p.Product);
            return View(productStatus1.ToList());
        }

        // GET: ProductStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductStatus productStatus = db.ProductStatus1.Find(id);
            if (productStatus == null)
            {
                return HttpNotFound();
            }
            return View(productStatus);
        }

        // GET: ProductStatus/Create
        public ActionResult Create()
        {
            ViewBag.LocationID = new SelectList(db.Locations1, "LocationID", "LocationName");
            ViewBag.OrderID = new SelectList(db.Orders1, "OrderID", "OrderID");
            ViewBag.ProductID = new SelectList(db.Products1, "ProductID", "ProdName");
            return View();
        }

        // POST: ProductStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PSID,ProductID,LocationID,InStock,OrderID,Discontinued,Section,Col,Row")] ProductStatus productStatus)
        {
            if (ModelState.IsValid)
            {
                db.ProductStatus1.Add(productStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(db.Locations1, "LocationID", "LocationName", productStatus.LocationID);
            ViewBag.OrderID = new SelectList(db.Orders1, "OrderID", "OrderID", productStatus.OrderID);
            ViewBag.ProductID = new SelectList(db.Products1, "ProductID", "ProdName", productStatus.ProductID);
            return View(productStatus);
        }

        // GET: ProductStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductStatus productStatus = db.ProductStatus1.Find(id);
            if (productStatus == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(db.Locations1, "LocationID", "LocationName", productStatus.LocationID);
            ViewBag.OrderID = new SelectList(db.Orders1, "OrderID", "OrderID", productStatus.OrderID);
            ViewBag.ProductID = new SelectList(db.Products1, "ProductID", "ProdName", productStatus.ProductID);
            return View(productStatus);
        }

        // POST: ProductStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PSID,ProductID,LocationID,InStock,OrderID,Discontinued,Section,Col,Row")] ProductStatus productStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Locations1, "LocationID", "LocationName", productStatus.LocationID);
            ViewBag.OrderID = new SelectList(db.Orders1, "OrderID", "OrderID", productStatus.OrderID);
            ViewBag.ProductID = new SelectList(db.Products1, "ProductID", "ProdName", productStatus.ProductID);
            return View(productStatus);
        }

        // GET: ProductStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductStatus productStatus = db.ProductStatus1.Find(id);
            if (productStatus == null)
            {
                return HttpNotFound();
            }
            return View(productStatus);
        }

        // POST: ProductStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductStatus productStatus = db.ProductStatus1.Find(id);
            db.ProductStatus1.Remove(productStatus);
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
