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
    public class iCAREAdminsController : Controller
    {
        private abcdEntities1 db = new abcdEntities1();

        // GET: iCAREAdmins
        public ActionResult Index()
        {
            var iCAREAdmins = db.iCAREAdmins.Include(i => i.iCAREUser);
            return View(iCAREAdmins.ToList());
        }

        // GET: iCAREAdmins/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iCAREAdmin iCAREAdmin = db.iCAREAdmins.Find(id);
            if (iCAREAdmin == null)
            {
                return HttpNotFound();
            }
            return View(iCAREAdmin);
        }

        // GET: iCAREAdmins/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.iCAREUsers, "ID", "Name");
            return View();
        }

        // POST: iCAREAdmins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "adminEmail,dateHired,dateFinished,ID")] iCAREAdmin iCAREAdmin)
        {
            if (ModelState.IsValid)
            {
                db.iCAREAdmins.Add(iCAREAdmin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.iCAREUsers, "ID", "Name", iCAREAdmin.ID);
            return View(iCAREAdmin);
        }

        // GET: iCAREAdmins/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iCAREAdmin iCAREAdmin = db.iCAREAdmins.Find(id);
            if (iCAREAdmin == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.iCAREUsers, "ID", "Name", iCAREAdmin.ID);
            return View(iCAREAdmin);
        }

        // POST: iCAREAdmins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "adminEmail,dateHired,dateFinished,ID")] iCAREAdmin iCAREAdmin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iCAREAdmin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.iCAREUsers, "ID", "Name", iCAREAdmin.ID);
            return View(iCAREAdmin);
        }

        // GET: iCAREAdmins/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iCAREAdmin iCAREAdmin = db.iCAREAdmins.Find(id);
            if (iCAREAdmin == null)
            {
                return HttpNotFound();
            }
            return View(iCAREAdmin);
        }

        // POST: iCAREAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            iCAREAdmin iCAREAdmin = db.iCAREAdmins.Find(id);
            db.iCAREAdmins.Remove(iCAREAdmin);
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
