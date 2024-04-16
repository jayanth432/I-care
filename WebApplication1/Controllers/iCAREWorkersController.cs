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
    public class iCAREWorkersController : Controller
    {
        private abcdEntities1 db = new abcdEntities1();

        // GET: iCAREWorkers
        public ActionResult Index()
        {
            var iCAREWorkers = db.iCAREWorkers.Include(i => i.iCAREAdmin).Include(i => i.iCAREUser).Include(i => i.UserRole);
            return View(iCAREWorkers.ToList());
        }

        // GET: iCAREWorkers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iCAREWorker iCAREWorker = db.iCAREWorkers.Find(id);
            if (iCAREWorker == null)
            {
                return HttpNotFound();
            }
            return View(iCAREWorker);
        }

        // GET: iCAREWorkers/Create
        public ActionResult Create()
        {
            ViewBag.creatorID = new SelectList(db.iCAREAdmins, "ID", "adminEmail");
            ViewBag.ID = new SelectList(db.iCAREUsers, "ID", "Name");
            ViewBag.roleID = new SelectList(db.UserRoles, "ID", "roleName");
            return View();
        }

        // POST: iCAREWorkers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "profession,ID,roleID,creatorID")] iCAREWorker iCAREWorker)
        {
            if (ModelState.IsValid)
            {
                db.iCAREWorkers.Add(iCAREWorker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.creatorID = new SelectList(db.iCAREAdmins, "ID", "adminEmail", iCAREWorker.creatorID);
            ViewBag.ID = new SelectList(db.iCAREUsers, "ID", "Name", iCAREWorker.ID);
            ViewBag.roleID = new SelectList(db.UserRoles, "ID", "roleName", iCAREWorker.roleID);
            return View(iCAREWorker);
        }

        // GET: iCAREWorkers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iCAREWorker iCAREWorker = db.iCAREWorkers.Find(id);
            if (iCAREWorker == null)
            {
                return HttpNotFound();
            }
            ViewBag.creatorID = new SelectList(db.iCAREAdmins, "ID", "adminEmail", iCAREWorker.creatorID);
            ViewBag.ID = new SelectList(db.iCAREUsers, "ID", "Name", iCAREWorker.ID);
            ViewBag.roleID = new SelectList(db.UserRoles, "ID", "roleName", iCAREWorker.roleID);
            return View(iCAREWorker);
        }

        // POST: iCAREWorkers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "profession,ID,roleID,creatorID")] iCAREWorker iCAREWorker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iCAREWorker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.creatorID = new SelectList(db.iCAREAdmins, "ID", "adminEmail", iCAREWorker.creatorID);
            ViewBag.ID = new SelectList(db.iCAREUsers, "ID", "Name", iCAREWorker.ID);
            ViewBag.roleID = new SelectList(db.UserRoles, "ID", "roleName", iCAREWorker.roleID);
            return View(iCAREWorker);
        }

        // GET: iCAREWorkers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iCAREWorker iCAREWorker = db.iCAREWorkers.Find(id);
            if (iCAREWorker == null)
            {
                return HttpNotFound();
            }
            return View(iCAREWorker);
        }

        // POST: iCAREWorkers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            iCAREWorker iCAREWorker = db.iCAREWorkers.Find(id);
            db.iCAREWorkers.Remove(iCAREWorker);
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
