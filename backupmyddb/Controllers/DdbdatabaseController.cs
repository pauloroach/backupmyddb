using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using backupmyddb.DAL;
using backupmyddb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Net;

namespace backupmyddb.Controllers
{
    [Authorize]
    public class DdbdatabaseController : Controller
    {
        private DdbContext db = new DdbContext();

        // GET: Ddbdatabase
        public ActionResult Index()
        {
            return View(db.Ddbdatabases.ToList());
        }

        // GET: Ddbdatabase/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ddbdatabase/Create
        public ActionResult Create()
        {
            return View();
            
        }

        // POST: Ddbdatabase/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ddbdatabase ddbdatabase)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.Ddbdatabases.Add(ddbdatabase);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(ddbdatabase);
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                return View(ddbdatabase);
            }
        }

        // GET: Ddbdatabase/Edit/5
        public ActionResult Edit(int? id)
        {
            //Return bad request if id is null
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var databaseToUpdate = db.Ddbdatabases.Find(id);
            if (databaseToUpdate != null)
            {
                return View(databaseToUpdate);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
        }

        // POST: Ddbdatabase/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, FormCollection collection)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var databaseToUpdate = db.Ddbdatabases.Find(id);
            if (TryUpdateModel(databaseToUpdate, "", new string[] { "Name", "Endpoint", "Authkey" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Unable to save changes.");
                    return View();
                }
            }
            return View(databaseToUpdate);
        }

        // GET: Ddbdatabase/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Ddbdatabase Ddbdatabase = db.Ddbdatabases.Find(id);
            if (Ddbdatabase == null)
            {
                return HttpNotFound();
            }
            return View(Ddbdatabase);
        }

        // POST: Ddbdatabase/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Ddbdatabase Ddbdatabase = db.Ddbdatabases.Find(id);
                db.Ddbdatabases.Remove(Ddbdatabase);
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
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
