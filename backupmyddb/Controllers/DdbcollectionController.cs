using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backupmyddb.Controllers
{
    public class DdbcollectionController : Controller
    {
        // GET: Ddbcollection
        public ActionResult Index()
        {
            return View();
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

        // GET: Ddbcollection/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ddbcollection/Edit/5
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
