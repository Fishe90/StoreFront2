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
    public class OrdersController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: Orders
        public ActionResult Index()
        {
            var orders1 = db.Orders1.Include(o => o.Location).Include(o => o.Product);
            return View(orders1.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders1.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.LocationID = new SelectList(db.Locations1, "LocationID", "LocationName");
            ViewBag.ProductID = new SelectList(db.Products1, "ProductID", "ProdName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,ProductID,Quantity,TotalCost,DateOrdered,DateShipped,DateReceived,LocationID")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Orders1.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(db.Locations1, "LocationID", "LocationName", orders.LocationID);
            ViewBag.ProductID = new SelectList(db.Products1, "ProductID", "ProdName", orders.ProductID);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders1.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(db.Locations1, "LocationID", "LocationName", orders.LocationID);
            ViewBag.ProductID = new SelectList(db.Products1, "ProductID", "ProdName", orders.ProductID);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,ProductID,Quantity,TotalCost,DateOrdered,DateShipped,DateReceived,LocationID")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Locations1, "LocationID", "LocationName", orders.LocationID);
            ViewBag.ProductID = new SelectList(db.Products1, "ProductID", "ProdName", orders.ProductID);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders1.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.Orders1.Find(id);
            db.Orders1.Remove(orders);
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
