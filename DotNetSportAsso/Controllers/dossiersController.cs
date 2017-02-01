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
    public class dossierController : Controller
    {

        private dbAssoSportEntities db = new dbAssoSportEntities();
        // GET: dossier
        public ActionResult Index()
        {
            return View(db.dossier.ToList());
        }

        // GET: dossier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dossier document = db.dossier.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: dossier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: dossier/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dossier_id,utilisateur_id,certificat_medical,fiche_renseignement,assurance,paiement,dossier_complet")] dossier dossier)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    db.dossier.Add(dossier);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            ViewBag.utilisateur_id = new SelectList(db.utilisateur, "utilisateur_id", "login", dossier.utilisateur_id);
            return View(dossier);

        }

        // GET: dossier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dossier dossier = db.dossier.Find(id);
            if (dossier == null)
            {
                return HttpNotFound();
            }
            ViewBag.utilisateur_id = new SelectList(db.utilisateur, "utilisateur_id", "login", dossier.utilisateur_id);
            return View(dossier);
        }

        // POST: dossier/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dossier_id,utilisateur_id,certificat_medical,fiche_renseignement,assurance,paiement,dossier_complet")] dossier dossier)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(dossier).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            ViewBag.utilisateur_id = new SelectList(db.utilisateur, "utilisateur_id", "login", dossier.utilisateur_id);
            return View(dossier);

        }

        // GET: dossier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dossier dossier = db.dossier.Find(id);
            if (dossier == null)
            {
                return HttpNotFound();
            }
            ViewBag.utilisateur_id = new SelectList(db.utilisateur, "utilisateur_id", "login", dossier.utilisateur_id);
            return View(dossier);
        }

        // POST: dossier/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            dossier dossier = db.dossier.Find(id);
            db.dossier.Remove(dossier);
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
