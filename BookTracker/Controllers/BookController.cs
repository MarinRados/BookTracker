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
    public class BookController : Controller
    {
        private BookContext db = new BookContext();

        // GET: Book
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Author);
            return View(books.ToList());
        }

        // GET: Book/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookEntity bookEntity = db.Books.Find(id);
            if (bookEntity == null)
            {
                return HttpNotFound();
            }
            return View(bookEntity);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            ViewBag.AuthorID = new SelectList(db.Authors, "ID", "LastName");
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookEntityID,AuthorID,Name,FirstAuthor,LastAuthor,DateRead,Rating")] BookEntity bookEntity)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(bookEntity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorID = new SelectList(db.Authors, "ID", "LastName", bookEntity.AuthorID);
            return View(bookEntity);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookEntity bookEntity = db.Books.Find(id);
            if (bookEntity == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "ID", "LastName", bookEntity.AuthorID);
            return View(bookEntity);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookEntityID,AuthorID,Name,FirstAuthor,LastAuthor,DateRead,Rating")] BookEntity bookEntity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookEntity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "ID", "LastName", bookEntity.AuthorID);
            return View(bookEntity);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookEntity bookEntity = db.Books.Find(id);
            if (bookEntity == null)
            {
                return HttpNotFound();
            }
            return View(bookEntity);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookEntity bookEntity = db.Books.Find(id);
            db.Books.Remove(bookEntity);
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
