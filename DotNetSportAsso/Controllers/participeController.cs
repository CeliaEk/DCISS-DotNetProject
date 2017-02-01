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
    public class participeController : Controller
    {
        private dbAssoSportEntities db = new dbAssoSportEntities();

        // GET: participe
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.participe.ToList());
        }

        // GET: participe/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            participe participe = db.participe.Find(id);
            if (participe == null)
            {
                return HttpNotFound();
            }
            return View(participe);
        }

        // GET: participe/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: participe/Create
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "participe_id")] participe participe)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.participe.Add(participe);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }


            return View(participe);
        }

        // GET: participe/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            participe participe = db.participe.Find(id);
            if (participe == null)
            {
                return HttpNotFound();
            }
            return View(participe);
        }

        // POST: participe/Edit/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "participe_id")] participe participe)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(participe).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(participe);
        }

        // GET: participe/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: participe/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
