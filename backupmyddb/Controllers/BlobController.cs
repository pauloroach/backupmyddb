using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using backupmyddb.DAL;
using backupmyddb.Models;

namespace backupmyddb.Controllers
{
    public class BlobController : Controller
    {
        private DdbContext db = new DdbContext();

        // GET: Blob
        public ActionResult Index()
        {
            return View(db.Blobs.ToList());
        }

        // GET: Blob/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blob blobModel = db.Blobs.Find(id);
            if (blobModel == null)
            {
                return HttpNotFound();
            }
            return View(blobModel);
        }

        // GET: Blob/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blob/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AccountName,AccountKey")] Blob blobModel)
        {
            if (ModelState.IsValid)
            {
                db.Blobs.Add(blobModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blobModel);
        }

        // GET: Blob/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blob blobModel = db.Blobs.Find(id);
            if (blobModel == null)
            {
                return HttpNotFound();
            }
            return View(blobModel);
        }

        // POST: Blob/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AccountName,AccountKey")] Blob blobModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blobModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blobModel);
        }

        // GET: Blob/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blob blobModel = db.Blobs.Find(id);
            if (blobModel == null)
            {
                return HttpNotFound();
            }
            return View(blobModel);
        }

        // POST: Blob/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blob blobModel = db.Blobs.Find(id);
            db.Blobs.Remove(blobModel);
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
