using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backupmyddb.Controllers
{
    public class DdbstoredprocedureController : Controller
    {
        // GET: Ddbstoredprocedure
        public ActionResult Index()
        {
            return View();
        }

        // GET: Ddbstoredprocedure/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ddbstoredprocedure/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ddbstoredprocedure/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ddbstoredprocedure/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ddbstoredprocedure/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ddbstoredprocedure/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ddbstoredprocedure/Delete/5
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
