using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using backupmyddb.DAL;
using backupmyddb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net;

using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types


namespace backupmyddb.Controllers
{
    [Authorize]
    public class DdbdatabaseController : Controller
    {
        private DdbContext db = new DdbContext();
        //Reusable instance of DocumentClient which represents the connection to a DocumentDB endpoint
        private static DocumentClient client;

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

        public async Task<ActionResult> Connect(int? id)
        {
            HttpContext.Server.ScriptTimeout = 300;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Get database info
            var databaseToUpdate = db.Ddbdatabases.Find(id);
            if(databaseToUpdate != null)
            {
                try
                {
                    List<Database> databases = await ListDatabasesAsync(databaseToUpdate.Endpoint, databaseToUpdate.Authkey);
                    //Console.WriteLine(databases);
                } catch (DocumentClientException de)
                {
                    Exception baseException = de.GetBaseException();
                    Console.WriteLine("{0} error ocurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
                }
            }
            return View();
        }

        public async Task<ActionResult> GetAllDocuments(int? id, string databaseName)
        {
            HttpContext.Server.ScriptTimeout = 300;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                
            }

            var databaseToUse = db.Ddbdatabases.Find(id);
            if(databaseToUse != null)
            {
                try
                {
                    List<Document> collections = await GetAllDocumentsAsync(databaseToUse.Endpoint, databaseToUse.Authkey, databaseName, "ticketsCollection");
                    //List<Database> databases = await ListDatabasesAsync(databaseToUpdate.Endpoint, databaseToUpdate.Authkey);
                    Console.WriteLine(collections);
                    ViewBag.Documents = collections;
                }
                catch (DocumentClientException de)
                {
                    Exception baseException = de.GetBaseException();
                    Console.WriteLine("{0} error ocurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
                }
            }
            return View();

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private static async Task runExample()
        {
            var databaseId = "dev";
            Database database = client.CreateDatabaseQuery().Where(db => db.Id == databaseId).AsEnumerable().FirstOrDefault();
            Console.WriteLine("1. Query for a database returned: {0}", database == null ? "no results" : database.Id);


            var colls = await client.ReadDocumentCollectionFeedAsync(UriFactory.CreateDatabaseUri(databaseId));
            Console.WriteLine("\n5. Reading all DocumentCollection resources for a database");
            foreach (var coll in colls)
            {
                Console.WriteLine(coll);
            }
        }

        private static async Task<List<Document>> GetAllDocumentsAsync(string Endpoint, string Authkey, string databaseId, string collectionId)
        {
            string continuation = null;
            var documents = new List<Document>();
            var collectionLink = UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);
            using (client = new DocumentClient(new Uri(Endpoint), Authkey))
            {
                do
                {
                    FeedOptions options = new FeedOptions
                    {
                        RequestContinuation = continuation,
                        MaxItemCount = 100
                    };

                    var docs = await client.ReadDocumentFeedAsync(collectionLink, options);
                    foreach (var d in docs)
                    {
                        documents.Add(d);
                    }
                    continuation = docs.ResponseContinuation;
                }
                while (!String.IsNullOrEmpty(continuation));
            }
            return documents;
        }

        /*

        private static async Task<List<DocumentCollection>> ListCollectionsAsync(string Endpoint, string Authkey, string databaseId)
        {
            
            List<Database> databases = new List<Database>();
            using (client = new DocumentClient(new Uri(Endpoint), Authkey))
            {
                var colls = await client.ReadDocumentCollectionFeedAsync(UriFactory.CreateDatabaseUri(databaseId));
            }
            return colls;
        }
        */
        private static async Task<List<Database>> ListDatabasesAsync(string Endpoint, string Authkey)
        {
            string continuation = null;
            List<Database> databases = new List<Database>();
            using (client = new DocumentClient(new Uri(Endpoint), Authkey))
            {
                do
                {
                    FeedOptions options = new FeedOptions
                    {
                        RequestContinuation = continuation,
                        MaxItemCount = 50
                    };

                    FeedResponse<Database> response = await client.ReadDatabaseFeedAsync(options);
                    foreach (Database db in response)
                    {
                        databases.Add(db);
                    }

                    continuation = response.ResponseContinuation;
                }
                while (!String.IsNullOrEmpty(continuation));
            }
            return databases;
        }
    }
}
