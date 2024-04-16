using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ModificationHistoriesController : Controller
    {
        private abcdEntities1 db = new abcdEntities1();

        // GET: ModificationHistories
        public ActionResult Index()
        {
            var modificationHistories = db.ModificationHistories.Include(m => m.DocumentMetadata).Include(m => m.iCAREWorker);
            return View(modificationHistories.ToList());
        }

        // GET: ModificationHistories/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModificationHistory modificationHistory = db.ModificationHistories.Find(id);
            if (modificationHistory == null)
            {
                return HttpNotFound();
            }
            return View(modificationHistory);
        }

        // GET: ModificationHistories/Create
        public ActionResult Create()
        {
            ViewBag.docID = new SelectList(db.DocumentMetadatas, "docID", "docName");
            ViewBag.WorkerID = new SelectList(db.iCAREWorkers, "ID", "profession");
            return View();
        }

        // POST: ModificationHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dateOfModification,description,docID,WorkerID,MHID")] ModificationHistory modificationHistory)
        {
            if (ModelState.IsValid)
            {
                db.ModificationHistories.Add(modificationHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.docID = new SelectList(db.DocumentMetadatas, "docID", "docName", modificationHistory.docID);
            ViewBag.WorkerID = new SelectList(db.iCAREWorkers, "ID", "profession", modificationHistory.WorkerID);
            return View(modificationHistory);
        }

        // GET: ModificationHistories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModificationHistory modificationHistory = db.ModificationHistories.Find(id);
            if (modificationHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.docID = new SelectList(db.DocumentMetadatas, "docID", "docName", modificationHistory.docID);
            ViewBag.WorkerID = new SelectList(db.iCAREWorkers, "ID", "profession", modificationHistory.WorkerID);
            return View(modificationHistory);
        }

        // POST: ModificationHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dateOfModification,description,docID,WorkerID,MHID")] ModificationHistory modificationHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modificationHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.docID = new SelectList(db.DocumentMetadatas, "docID", "docName", modificationHistory.docID);
            ViewBag.WorkerID = new SelectList(db.iCAREWorkers, "ID", "profession", modificationHistory.WorkerID);
            return View(modificationHistory);
        }

        // GET: ModificationHistories/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModificationHistory modificationHistory = db.ModificationHistories.Find(id);
            if (modificationHistory == null)
            {
                return HttpNotFound();
            }
            return View(modificationHistory);
        }

        // POST: ModificationHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ModificationHistory modificationHistory = db.ModificationHistories.Find(id);
            db.ModificationHistories.Remove(modificationHistory);
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
