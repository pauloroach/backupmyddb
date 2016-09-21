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
    public class DdbcollectionController : Controller
    {
        private DdbContext db = new DdbContext();

        // GET: Ddbcollection
        public ActionResult Index()
        {
            return View(db.Ddbcollections.ToList());
        }

        // GET: Ddbcollection/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ddbcollection/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ddbcollection/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ddbcollection ddbcollection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.Ddbcollections.Add(ddbcollection);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(ddbcollection);
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                return View(ddbcollection);
            }
        }

        // GET: Ddbcollection/Edit/5
        public ActionResult Edit(int? id)
        {
            //Return bad request if id is null
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var collectionToUpdate = db.Ddbcollections.Find(id);
            if (collectionToUpdate != null)
            {
                return View(collectionToUpdate);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
        }

        // POST: Ddbcollection/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, FormCollection collection)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var collectionToUpdate = db.Ddbcollections.Find(id);
            if (TryUpdateModel(collectionToUpdate, "", new string[] { "Name" }))
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
            return View(collectionToUpdate);
        }

        // GET: Ddbcollection/Delete/5
        public ActionResult Delete(int id)
        {
           
            return View();
        }

        // POST: Ddbcollection/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
