using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookTracker.DAL;

namespace BookTracker.Controllers
{
    public class WishlistController : Controller
    {
        private BookContext db = new BookContext();

        // GET: Wishlist
        public ActionResult Index()
        {
            return View(db.Wishlists.ToList());
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
            if (ModelState.IsValid)
            {
                db.Wishlists.Add(wishlistEntity);
                db.SaveChanges();
                return RedirectToAction("Index");
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,WishName,WishFirstAuthor,WishLastAuthor,Price")] WishlistEntity wishlistEntity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wishlistEntity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wishlistEntity);
        }

        // GET: Wishlist/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Wishlist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WishlistEntity wishlistEntity = db.Wishlists.Find(id);
            db.Wishlists.Remove(wishlistEntity);
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
