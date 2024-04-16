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
    public class iCAREUsersController : Controller
    {
        private abcdEntities db = new abcdEntities();

        // GET: iCAREUsers
        public ActionResult Index()
        {
            var iCAREUsers = db.iCAREUsers.Include(i => i.iCAREAdmin).Include(i => i.iCAREWorker).Include(i => i.UserPassword);
            return View(iCAREUsers.ToList());
        }

        // GET: iCAREUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iCAREUser iCAREUser = db.iCAREUsers.Find(id);
            if (iCAREUser == null)
            {
                return HttpNotFound();
            }
            return View(iCAREUser);
        }

        // GET: iCAREUsers/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.iCAREAdmins, "ID", "adminEmail");
            ViewBag.ID = new SelectList(db.iCAREWorkers, "ID", "profession");
            ViewBag.ID = new SelectList(db.UserPasswords, "ID", "userName");
            return View();
        }

        // POST: iCAREUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] iCAREUser iCAREUser)
        {
            if (ModelState.IsValid)
            {
                db.iCAREUsers.Add(iCAREUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.iCAREAdmins, "ID", "adminEmail", iCAREUser.ID);
            ViewBag.ID = new SelectList(db.iCAREWorkers, "ID", "profession", iCAREUser.ID);
            ViewBag.ID = new SelectList(db.UserPasswords, "ID", "userName", iCAREUser.ID);
            return View(iCAREUser);
        }

        // GET: iCAREUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iCAREUser iCAREUser = db.iCAREUsers.Find(id);
            if (iCAREUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.iCAREAdmins, "ID", "adminEmail", iCAREUser.ID);
            ViewBag.ID = new SelectList(db.iCAREWorkers, "ID", "profession", iCAREUser.ID);
            ViewBag.ID = new SelectList(db.UserPasswords, "ID", "userName", iCAREUser.ID);
            return View(iCAREUser);
        }

        // POST: iCAREUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] iCAREUser iCAREUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iCAREUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.iCAREAdmins, "ID", "adminEmail", iCAREUser.ID);
            ViewBag.ID = new SelectList(db.iCAREWorkers, "ID", "profession", iCAREUser.ID);
            ViewBag.ID = new SelectList(db.UserPasswords, "ID", "userName", iCAREUser.ID);
            return View(iCAREUser);
        }

        // GET: iCAREUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iCAREUser iCAREUser = db.iCAREUsers.Find(id);
            if (iCAREUser == null)
            {
                return HttpNotFound();
            }
            return View(iCAREUser);
        }

        // POST: iCAREUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            iCAREUser iCAREUser = db.iCAREUsers.Find(id);
            db.iCAREUsers.Remove(iCAREUser);
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
