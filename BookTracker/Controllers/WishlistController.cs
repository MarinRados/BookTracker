using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookTracker.DAL;
using PagedList;

namespace BookTracker.Controllers
{
    public class WishlistController : Controller
    {
        private BookContext db = new BookContext();

        // GET: Wishlist
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.WishSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.FirstASortParm = sortOrder == "first" ? "first_desc" : "first";
            ViewBag.LastASortParm = sortOrder == "last" ? "last_desc" : "last";
            ViewBag.PriceSortParm = sortOrder == "price" ? "price_desc" : "price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var wishlists = from s in db.Wishlists select s;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                wishlists = wishlists.Where(s => s.WishName.Contains(searchString)
                               || s.WishFirstAuthor.Contains(searchString) 
                               || s.WishLastAuthor.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    wishlists = wishlists.OrderByDescending(s => s.WishName);
                    break;
                case "first":
                    wishlists = wishlists.OrderBy(s => s.WishFirstAuthor);
                    break;
                case "first_desc":
                    wishlists = wishlists.OrderByDescending(s => s.WishFirstAuthor);
                    break;
                case "last":
                    wishlists = wishlists.OrderBy(s => s.WishLastAuthor);
                    break;
                case "last_desc":
                    wishlists = wishlists.OrderByDescending(s => s.WishLastAuthor);
                    break;
                case "price":
                    wishlists = wishlists.OrderBy(s => s.Price);
                    break;
                case "price0_desc":
                    wishlists = wishlists.OrderByDescending(s => s.Price);
                    break;
                default:
                    wishlists = wishlists.OrderBy(s => s.WishName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(wishlists.ToPagedList(pageNumber, pageSize));
        }

        // GET: Wishlist/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishlistEntity wishlistEntity = db.Wishlists.Find(id);
            if (wishlistEntity == null)
            {
                return HttpNotFound();
            }
            return View(wishlistEntity);
        }

        // GET: Wishlist/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Wishlist/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,WishName,WishFirstAuthor,WishLastAuthor,Price")] WishlistEntity wishlistEntity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Wishlists.Add(wishlistEntity);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again.");
            }
            return View(wishlistEntity);
        }

        // GET: Wishlist/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishlistEntity wishlistEntity = db.Wishlists.Find(id);
            if (wishlistEntity == null)
            {
                return HttpNotFound();
            }
            return View(wishlistEntity);
        }

        // POST: Wishlist/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var wishlistToUpdate = db.Wishlists.Find(id);
            if (TryUpdateModel(wishlistToUpdate, "", new string[] { "WishName","WishFirstAuthor","WishLastAuthor","Price" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again.");
                }
            }
            return View(wishlistToUpdate);
        }

        // GET: Wishlist/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again.";
            }
            WishlistEntity wishlist = db.Wishlists.Find(id);
            if (wishlist == null)
            {
                return HttpNotFound();
            }
            return View(wishlist);
        }

        // POST: Wishlist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                WishlistEntity wishlist = db.Wishlists.Find(id);
                db.Wishlists.Remove(wishlist);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
