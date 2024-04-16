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
    public class TreatmentRecordsController : Controller
    {
        private abcdEntities1 db = new abcdEntities1();

        // GET: TreatmentRecords
        public ActionResult Index()
        {
            var treatmentRecords = db.TreatmentRecords.Include(t => t.iCAREWorker).Include(t => t.PatientRecord).Include(t => t.PatientRecord1);
            return View(treatmentRecords.ToList());
        }

        // GET: TreatmentRecords/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatmentRecord treatmentRecord = db.TreatmentRecords.Find(id);
            if (treatmentRecord == null)
            {
                return HttpNotFound();
            }
            return View(treatmentRecord);
        }

        // GET: TreatmentRecords/Create
        public ActionResult Create()
        {
            ViewBag.WorkerID = new SelectList(db.iCAREWorkers, "ID", "profession");
            ViewBag.PatientID = new SelectList(db.PatientRecords, "ID", "name");
            ViewBag.PatientRecordID = new SelectList(db.PatientRecords, "ID", "name");
            return View();
        }

        // POST: TreatmentRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "treatmentID,description,treatmentDate,WorkerID,PatientID,PatientRecordID")] TreatmentRecord treatmentRecord)
        {
            if (ModelState.IsValid)
            {
                db.TreatmentRecords.Add(treatmentRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WorkerID = new SelectList(db.iCAREWorkers, "ID", "profession", treatmentRecord.WorkerID);
            ViewBag.PatientID = new SelectList(db.PatientRecords, "ID", "name", treatmentRecord.PatientID);
            ViewBag.PatientRecordID = new SelectList(db.PatientRecords, "ID", "name", treatmentRecord.PatientRecordID);
            return View(treatmentRecord);
        }

        // GET: TreatmentRecords/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatmentRecord treatmentRecord = db.TreatmentRecords.Find(id);
            if (treatmentRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.WorkerID = new SelectList(db.iCAREWorkers, "ID", "profession", treatmentRecord.WorkerID);
            ViewBag.PatientID = new SelectList(db.PatientRecords, "ID", "name", treatmentRecord.PatientID);
            ViewBag.PatientRecordID = new SelectList(db.PatientRecords, "ID", "name", treatmentRecord.PatientRecordID);
            return View(treatmentRecord);
        }

        // POST: TreatmentRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "treatmentID,description,treatmentDate,WorkerID,PatientID,PatientRecordID")] TreatmentRecord treatmentRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treatmentRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WorkerID = new SelectList(db.iCAREWorkers, "ID", "profession", treatmentRecord.WorkerID);
            ViewBag.PatientID = new SelectList(db.PatientRecords, "ID", "name", treatmentRecord.PatientID);
            ViewBag.PatientRecordID = new SelectList(db.PatientRecords, "ID", "name", treatmentRecord.PatientRecordID);
            return View(treatmentRecord);
        }

        // GET: TreatmentRecords/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatmentRecord treatmentRecord = db.TreatmentRecords.Find(id);
            if (treatmentRecord == null)
            {
                return HttpNotFound();
            }
            return View(treatmentRecord);
        }

        // POST: TreatmentRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TreatmentRecord treatmentRecord = db.TreatmentRecords.Find(id);
            db.TreatmentRecords.Remove(treatmentRecord);
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
