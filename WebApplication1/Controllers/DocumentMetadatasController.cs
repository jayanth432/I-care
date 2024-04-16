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
    public class DocumentMetadatasController : Controller
    {
        private abcdEntities1 db = new abcdEntities1();

        // GET: DocumentMetadatas
        public ActionResult Index()
        {
            var documentMetadatas = db.DocumentMetadatas.Include(d => d.iCAREWorker).Include(d => d.PatientRecord).Include(d => d.PatientRecord1);
            return View(documentMetadatas.ToList());
        }

        // GET: DocumentMetadatas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentMetadata documentMetadata = db.DocumentMetadatas.Find(id);
            if (documentMetadata == null)
            {
                return HttpNotFound();
            }
            return View(documentMetadata);
        }

        // GET: DocumentMetadatas/Create
        public ActionResult Create()
        {
            ViewBag.WorkerID = new SelectList(db.iCAREWorkers, "ID", "profession");
            ViewBag.PatientID = new SelectList(db.PatientRecords, "ID", "name");
            ViewBag.PatientRecordID = new SelectList(db.PatientRecords, "ID", "name");
            return View();
        }

        // POST: DocumentMetadatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "docID,docName,dateOfCreation,PatientID,WorkerID,PatientRecordID")] DocumentMetadata documentMetadata)
        {
            if (ModelState.IsValid)
            {
                db.DocumentMetadatas.Add(documentMetadata);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WorkerID = new SelectList(db.iCAREWorkers, "ID", "profession", documentMetadata.WorkerID);
            ViewBag.PatientID = new SelectList(db.PatientRecords, "ID", "name", documentMetadata.PatientID);
            ViewBag.PatientRecordID = new SelectList(db.PatientRecords, "ID", "name", documentMetadata.PatientRecordID);
            return View(documentMetadata);
        }

        // GET: DocumentMetadatas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentMetadata documentMetadata = db.DocumentMetadatas.Find(id);
            if (documentMetadata == null)
            {
                return HttpNotFound();
            }
            ViewBag.WorkerID = new SelectList(db.iCAREWorkers, "ID", "profession", documentMetadata.WorkerID);
            ViewBag.PatientID = new SelectList(db.PatientRecords, "ID", "name", documentMetadata.PatientID);
            ViewBag.PatientRecordID = new SelectList(db.PatientRecords, "ID", "name", documentMetadata.PatientRecordID);
            return View(documentMetadata);
        }

        // POST: DocumentMetadatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "docID,docName,dateOfCreation,PatientID,WorkerID,PatientRecordID")] DocumentMetadata documentMetadata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentMetadata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WorkerID = new SelectList(db.iCAREWorkers, "ID", "profession", documentMetadata.WorkerID);
            ViewBag.PatientID = new SelectList(db.PatientRecords, "ID", "name", documentMetadata.PatientID);
            ViewBag.PatientRecordID = new SelectList(db.PatientRecords, "ID", "name", documentMetadata.PatientRecordID);
            return View(documentMetadata);
        }

        // GET: DocumentMetadatas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentMetadata documentMetadata = db.DocumentMetadatas.Find(id);
            if (documentMetadata == null)
            {
                return HttpNotFound();
            }
            return View(documentMetadata);
        }

        // POST: DocumentMetadatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DocumentMetadata documentMetadata = db.DocumentMetadatas.Find(id);
            db.DocumentMetadatas.Remove(documentMetadata);
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
