using DotNetSportAsso.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DotNetSportAsso.Controllers
{
    public class lieuController : Controller
    {

        private dbAssoSportEntities db = new dbAssoSportEntities();
        // GET: lieu
        public ActionResult Index()
        {
            return View(db.lieu.ToList());
        }

        // GET: lieu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lieu lieu = db.lieu.Find(id);
            if (lieu == null)
            {
                return HttpNotFound();
            }
            return View(lieu);
        }

        // GET: lieu/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: lieu/Create
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "lieu_id,lieu_nom,adresse_lieu")] lieu lieu)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    db.lieu.Add(lieu);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            return View(lieu);
           
        }

        // GET: lieu/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lieu lieu = db.lieu.Find(id);
            if (lieu == null)
            {
                return HttpNotFound();
            }
            return View(lieu);
        }

        // POST: lieu/Edit/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "lieu_id,lieu_nom,adresse_lieu")] lieu lieu)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here
                    db.Entry(lieu).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(lieu);
        }

        // GET: lieu/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lieu lieu = db.lieu.Find(id);
            if (lieu == null)
            {
                return HttpNotFound();
            }
            return View(lieu);
        }

        // POST: lieu/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            lieu lieu = db.lieu.Find(id);
            db.lieu.Remove(lieu);
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
