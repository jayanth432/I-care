﻿using System;
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
    public class GeoCodesController : Controller
    {
        private abcdEntities1 db = new abcdEntities1();

        // GET: GeoCodes
        public ActionResult Index()
        {
            return View(db.GeoCodes.ToList());
        }

        // GET: GeoCodes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeoCode geoCode = db.GeoCodes.Find(id);
            if (geoCode == null)
            {
                return HttpNotFound();
            }
            return View(geoCode);
        }

        // GET: GeoCodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GeoCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,description")] GeoCode geoCode)
        {
            if (ModelState.IsValid)
            {
                db.GeoCodes.Add(geoCode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(geoCode);
        }

        // GET: GeoCodes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeoCode geoCode = db.GeoCodes.Find(id);
            if (geoCode == null)
            {
                return HttpNotFound();
            }
            return View(geoCode);
        }

        // POST: GeoCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,description")] GeoCode geoCode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(geoCode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(geoCode);
        }

        // GET: GeoCodes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeoCode geoCode = db.GeoCodes.Find(id);
            if (geoCode == null)
            {
                return HttpNotFound();
            }
            return View(geoCode);
        }

        // POST: GeoCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GeoCode geoCode = db.GeoCodes.Find(id);
            db.GeoCodes.Remove(geoCode);
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
