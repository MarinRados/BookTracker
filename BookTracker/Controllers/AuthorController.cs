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
    public class AuthorController : Controller
    {
        private BookContext db = new BookContext();

        // GET: Author
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.LastSortParm = String.IsNullOrEmpty(sortOrder) ? "last_desc" : "";
            ViewBag.FirstSortParm = sortOrder == "first" ? "first_desc" : "first";
            
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            
            var authors = from s in db.Authors
                          select s;
           
            if (!String.IsNullOrEmpty(searchString))
            {
                authors = authors.Where(s => s.LastName.Contains(searchString)
                               || s.FirstName.Contains(searchString));
            }

            switch(sortOrder)
            {
                case "last_desc":
                    authors = authors.OrderByDescending(s => s.LastName);
                    break;
                case "first":
                    authors = authors.OrderBy(s => s.FirstName);
                    break;
                case "first_desc":
                    authors = authors.OrderByDescending(s => s.FirstName);
                    break;
                default:
                    authors = authors.OrderBy(s => s.LastName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(authors.ToPagedList(pageNumber, pageSize));
        }

        // GET: Author/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuthorEntity authorEntity = db.Authors.Find(id);
            if (authorEntity == null)
            {
                return HttpNotFound();
            }
            return View(authorEntity);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName")] AuthorEntity authorEntity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Authors.Add(authorEntity);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again.");
            }
            return View(authorEntity);
        }

        // GET: Author/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuthorEntity authorEntity = db.Authors.Find(id);
            if (authorEntity == null)
            {
                return HttpNotFound();
            }
            return View(authorEntity);
        }

        // POST: Author/Edit/5
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
            var authorToUpdate = db.Authors.Find(id);
            if (TryUpdateModel(authorToUpdate, "", new string[] { "FirstName", "LastName" }))
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
            return View(authorToUpdate);
        }

        // GET: Author/Delete/5
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
            AuthorEntity author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                AuthorEntity author = db.Authors.Find(id);
                db.Authors.Remove(author);
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
