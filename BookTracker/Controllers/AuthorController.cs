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
    public class AuthorController : Controller
    {
        private BookContext db = new BookContext();

        // GET: Author
        public ActionResult Index()
        {
            return View(db.Authors.ToList());
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
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName")] AuthorEntity authorEntity)
        {
            if (ModelState.IsValid)
            {
                db.Authors.Add(authorEntity);
                db.SaveChanges();
                return RedirectToAction("Index");
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName")] AuthorEntity authorEntity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(authorEntity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(authorEntity);
        }

        // GET: Author/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AuthorEntity authorEntity = db.Authors.Find(id);
            db.Authors.Remove(authorEntity);
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
