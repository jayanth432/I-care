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
    public class PatientRecordsController : Controller
    {
        private abcdEntities1 db = new abcdEntities1();

        // GET: PatientRecords
        public ActionResult Index()
        {
            var patientRecords = db.PatientRecords.Include(p => p.GeoCode).Include(p => p.iCAREWorker);
            return View(patientRecords.ToList());
        }

        // GET: PatientRecords/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientRecord patientRecord = db.PatientRecords.Find(id);
            if (patientRecord == null)
            {
                return HttpNotFound();
            }
            return View(patientRecord);
        }

        // GET: PatientRecords/Create
        public ActionResult Create()
        {
            ViewBag.GeoID = new SelectList(db.GeoCodes, "ID", "description");
            ViewBag.WorkerID = new SelectList(db.iCAREWorkers, "ID", "profession");
            return View();
        }

        // POST: PatientRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name,address,dateOfBirth,height,weight,bloodGroup,bedID,treatmentArea,GeoID,WorkerID")] PatientRecord patientRecord)
        {
            if (ModelState.IsValid)
            {
                db.PatientRecords.Add(patientRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GeoID = new SelectList(db.GeoCodes, "ID", "description", patientRecord.GeoID);
            ViewBag.WorkerID = new SelectList(db.iCAREWorkers, "ID", "profession", patientRecord.WorkerID);
            return View(patientRecord);
        }

        // GET: PatientRecords/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientRecord patientRecord = db.PatientRecords.Find(id);
            if (patientRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.GeoID = new SelectList(db.GeoCodes, "ID", "description", patientRecord.GeoID);
            ViewBag.WorkerID = new SelectList(db.iCAREWorkers, "ID", "profession", patientRecord.WorkerID);
            return View(patientRecord);
        }

        // POST: PatientRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,address,dateOfBirth,height,weight,bloodGroup,bedID,treatmentArea,GeoID,WorkerID")] PatientRecord patientRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patientRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GeoID = new SelectList(db.GeoCodes, "ID", "description", patientRecord.GeoID);
            ViewBag.WorkerID = new SelectList(db.iCAREWorkers, "ID", "profession", patientRecord.WorkerID);
            return View(patientRecord);
        }

        // GET: PatientRecords/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientRecord patientRecord = db.PatientRecords.Find(id);
            if (patientRecord == null)
            {
                return HttpNotFound();
            }
            return View(patientRecord);
        }

        // POST: PatientRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PatientRecord patientRecord = db.PatientRecords.Find(id);
            db.PatientRecords.Remove(patientRecord);
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
