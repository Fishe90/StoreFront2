using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using StoreFront2.DATA.EF;
using StoreFront2.UI.MVC.Models;
using StoreFront2.UI.MVC.Utilities;

namespace StoreFront2.UI.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        //public ActionResult CustomerView()
        //{
        //    var Products = db.Products.Include(p => p.Department).Include(p => p.Vendor);
        //    return View(Products.ToList());
        //}

        //For Search Bar
        public ViewResult CustomerView(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "description_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var products = from p in db.Products
                           select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.ProdName.Contains(searchString) || p.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.ProdName);
                    break;
                case "description_desc":
                    products = products.OrderByDescending(p => p.Description);
                    break;
                default:
                    products = products.OrderBy(p => p.ProdName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        // GET: Products
        //public ActionResult Index()
        //{
        //    var Products = db.Products.Include(p => p.Department).Include(p => p.Vendor);
        //    return View(Products.ToList());
        //}

        //For Filtering
        //public ActionResult Index(string sortOrder)
        //{
        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "description_desc" : "";
        //    var products = from p in db.Products
        //                   select p;
        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            products = products.OrderByDescending(p => p.ProdName);
        //            break;
        //        case "description_desc":
        //            products = products.OrderByDescending(p => p.Description);
        //            break;
        //        default:
        //            products = products.OrderBy(p => p.ProdName);
        //            break;
        //    }
        //    return View(products.ToList());
        //}

        //For Search Bar
        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "description_desc" : "";
            var products = from p in db.Products
                           select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.ProdName.Contains(searchString) || s.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.ProdName);
                    break;
                case "description_desc":
                    products = products.OrderByDescending(p => p.Description);
                    break;
                default:
                    products = products.OrderBy(p => p.ProdName);
                    break;
            }
            return View(products.ToList());
        }

        public ActionResult AddToCart(int qty, int productID)
        {
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            if (Session["cart"] != null)
            {
                shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];
            }
            else
            {
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }

            Product product = db.Products.Where(p => p.ProductID == productID).FirstOrDefault();

            if (product == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                CartItemViewModel item = new CartItemViewModel(qty, product);

                if (shoppingCart.ContainsKey(product.ProductID))
                {
                    shoppingCart[product.ProductID].Qty += qty;
                }
                else
                {
                    shoppingCart.Add(product.ProductID, item);
                }
                Session["cart"] = shoppingCart;
            }

            return RedirectToAction("Index", "ShoppingCart");
        }

        public ActionResult CustomerDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.DepID = new SelectList(db.Departments, "DepID", "DepName");
            ViewBag.VendorID = new SelectList(db.Vendors, "VendorID", "VenName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProdName,Description,UnitPrice,UnitCost,ImgURL,DepID,VendorID,SKU,UPC")] Product products, HttpPostedFileBase imgURL)
        {
            if (ModelState.IsValid)
            {
                #region File Upload Utility
                /*File Upbload completion includes the following :
                 * 1) Prepare the view
                 *      -Code the BeginForm()
                 *      -Add the File Input (replace the EditorFor()/ValidationMessageFor()
                 * 2) Prepare the Create Post Action
                 *      -Receive the HTTPPostedFileBase - It's name (variable) MUST match the name property of the input (casing does NOT matter)
                 *      -Process teh FileUpload using the ImageService Utility (Add the Using for the New Namespace created (Utilities folder)
                 * 3) Move to Edit Post and code.
                 */

                //process the file upload using the utility
                //Use Default image if one is not provided (in this case we have noImage.png)
                string imgName = "NoImage.PNG";

                //check HPFB to ensure it is not null
                if (imgURL != null)
                {
                    //if NOT null
                    //retrieve the image from the HPFB and assign to the img variable
                    imgName = imgURL.FileName;

                    //declare and assign the extension
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));//.ext

                    //for images, we want a good list of proper extension to be accepted
                    //for pdfs, no collection is needed.
                    string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };

                    //check the ext variable against the valid list AND make sure the file size is NOT too large
                    //(Max Default ASP.NET size is ~ 4mb - expressed in bytes 4194304
                    if (goodExts.Contains(ext.ToLower()) && (imgURL.ContentLength <= 4194304))
                    //code for pdf check
                    //if (ext.ToLower()) == ".pdf" && (bookCover.ContentLength <= 4194304))
                    {
                        //if both are good rename the file using a guid + the extension
                        //GUID - Global Unique IDentifier
                        //-other ways to create unique ids (Make sure your DB field accomodates the size)
                        //substring the book title (first 10/20 characters, concatonate teh current userID, datetimestamp)
                        //Guid works well but it is NOT the only option
                        imgName = Guid.NewGuid() + ext.ToLower();
                        //could us an alternate mthodology to rename
                        //imgName = book.BookTitle.Substring(0, 10) + User.Identity.Name + DateTime.Now;

                        //Resize the Image
                        //provide all requirements to call the ResizeImage() from utility. SavePath, Image, MaxImageSize, MaxThumbSize
                        string savePath = Server.MapPath("~/Content/img/");
                        Image convertedImage = Image.FromStream(imgURL.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;

                        //call the imageService.ResizeImage
                        ImageService.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);
                    }
                    //either file size or ext are bad
                    else
                    {
                        //default to the noImage value
                        imgName = "NoImage.PNG";
                    }
                }
                //no matter what - add the imageName property of the book object to send to the DB
                products.ImgURL = imgName;
                #endregion

                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepID = new SelectList(db.Departments, "DepID", "DepName", products.DepID);
            ViewBag.VendorID = new SelectList(db.Vendors, "VendorID", "VenName", products.VendorID);
            return View(products);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepID = new SelectList(db.Departments, "DepID", "DepName", products.DepID);
            ViewBag.VendorID = new SelectList(db.Vendors, "VendorID", "VenName", products.VendorID);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProdName,Description,UnitPrice,UnitCost,ImgURL,DepID,VendorID,SKU,UPC")] Product products, HttpPostedFileBase imgURL)
        {
            if (ModelState.IsValid)
            {
                #region File Upload Utility
                /*File Upbload completion includes the following :
                 * 1) Prepare the view
                 *      -Code the BeginForm()
                 *      -Add the File Input (replace the EditorFor()/ValidationMessageFor()
                 * 2) Prepare the Create Post Action
                 *      -Receive the HTTPPostedFileBase - It's name (variable) MUST match the name property of the input (casing does NOT matter)
                 *      -Process teh FileUpload using the ImageService Utility (Add the Using for the New Namespace created (Utilities folder)
                 * 3) Move to Edit Post and code.
                 */

                //process the file upload using the utility
                //Use Default image if one is not provided (in this case we have noImage.png)
                string imgName = "NoImage.PNG";

                //check HPFB to ensure it is not null
                if (imgURL != null)
                {
                    //if NOT null
                    //retrieve the image from the HPFB and assign to the img variable
                    imgName = imgURL.FileName;

                    //declare and assign the extension
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));//.ext

                    //for images, we want a good list of proper extension to be accepted
                    //for pdfs, no collection is needed.
                    string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };

                    //check the ext variable against the valid list AND make sure the file size is NOT too large
                    //(Max Default ASP.NET size is ~ 4mb - expressed in bytes 4194304
                    if (goodExts.Contains(ext.ToLower()) && (imgURL.ContentLength <= 4194304))
                    //code for pdf check
                    //if (ext.ToLower()) == ".pdf" && (bookCover.ContentLength <= 4194304))
                    {
                        //if both are good rename the file using a guid + the extension
                        //GUID - Global Unique IDentifier
                        //-other ways to create unique ids (Make sure your DB field accomodates the size)
                        //substring the book title (first 10/20 characters, concatonate teh current userID, datetimestamp)
                        //Guid works well but it is NOT the only option
                        imgName = Guid.NewGuid() + ext.ToLower();
                        //could us an alternate mthodology to rename
                        //imgName = book.BookTitle.Substring(0, 10) + User.Identity.Name + DateTime.Now;

                        //Resize the Image
                        //provide all requirements to call the ResizeImage() from utility. SavePath, Image, MaxImageSize, MaxThumbSize
                        string savePath = Server.MapPath("~/Content/img/");
                        Image convertedImage = Image.FromStream(imgURL.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;

                        //call the imageService.ResizeImage
                        ImageService.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);
                    }
                    //either file size or ext are bad
                    else
                    {
                        //default to the noImage value
                        imgName = "NoImage.PNG";
                    }
                }
                //no matter what - add the imageName property of the book object to send to the DB
                products.ImgURL = imgName;
                #endregion

                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepID = new SelectList(db.Departments, "DepID", "DepName", products.DepID);
            ViewBag.VendorID = new SelectList(db.Vendors, "VendorID", "VenName", products.VendorID);
            return View(products);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product products = db.Products.Find(id);
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
            Product products = db.Products.Find(id);
            db.Products.Remove(products);
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
