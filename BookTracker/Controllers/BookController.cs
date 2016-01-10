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
    public class BookController : Controller
    {
        private BookContext db = new BookContext();

        // GET: Book
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AuthorSortParm = sortOrder == "author" ? "author_desc" : "author";
            ViewBag.DateSortParm = sortOrder == "date" ? "date_desc" : "date";
            ViewBag.RatingSortParm = sortOrder == "rating" ? "rating_desc" : "rating";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var books = db.Books.Include(b => b.Author);
            books = from s in db.Books select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Name.Contains(searchString)
                               || s.Author.FirstName.Contains(searchString)
                               || s.Author.LastName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    books = books.OrderByDescending(s => s.Name);
                    break;
                case "author":
                    books = books.OrderBy(s => s.Author.LastName);
                    break;
                case "author_desc":
                    books = books.OrderByDescending(s => s.Author.LastName);
                    break;
                case "date":
                    books = books.OrderBy(s => s.DateRead);
                    break;
                case "date_desc":
                    books = books.OrderByDescending(s => s.DateRead);
                    break;
                case "rating":
                    books = books.OrderBy(s => s.Rating);
                    break;
                case "rating_desc":
                    books = books.OrderByDescending(s => s.Rating);
                    break;
                default:
                    books = books.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(books.ToPagedList(pageNumber, pageSize));
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
            ViewBag.AuthorID = new SelectList(db.Authors, "ID", "FullName");
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookEntityID,AuthorID,Name,FirstAuthor,LastAuthor,DateRead,Rating")] BookEntity bookEntity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Books.Add(bookEntity);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again.");
            }
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
            ViewBag.AuthorID = new SelectList(db.Authors, "ID", "FullName", bookEntity.AuthorID);
            return View(bookEntity);
        }

        // POST: Book/Edit/5
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
            var bookToUpdate = db.Books.Find(id);
            if (TryUpdateModel(bookToUpdate, "", new string[] { "AuthorID","Name","DateRead","Rating" }))
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
            return View(bookToUpdate);
        }

        // GET: Book/Delete/5
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
            BookEntity book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                BookEntity book = db.Books.Find(id);
                db.Books.Remove(book);
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
