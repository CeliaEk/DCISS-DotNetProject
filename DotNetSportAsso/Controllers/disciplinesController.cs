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
    public class disciplineController : Controller
    {
        private dbAssoSportEntities _db = new dbAssoSportEntities();
        public static utilisateur FindUserByLogin(string login)
        {
            dbAssoSportEntities db = new dbAssoSportEntities();
            foreach (utilisateur u in db.utilisateur)
            {
                if (u.login == login)
                {
                    return u;
                }
            }
            return new utilisateur();
        }

        public static string GetUserFullNameByLogin(string login)
        {
            utilisateur u = FindUserByLogin(login);
            return u.prenom + ' ' + u.nom;
        }

        public static long GetIdByUserName(string login)
        {
            dbAssoSportEntities db = new dbAssoSportEntities();
            foreach (utilisateur u in db.utilisateur)
            {
                if (u.login == login)
                {
                    return u.utilisateur_id;
                }
            }
            return 0;
        }

        // GET: discipline
        public ActionResult Index()
        {
            if (User.Identity.Name != null && User.IsInRole("encadrant"))
            {
                long id = GetIdByUserName(User.Identity.Name);

                /*discipline*/
                IQueryable<discipline> discipline = from di in _db.discipline where di.encadrant_id == id select di;
                ViewBag.discipline = discipline.ToList<discipline>();
                if (!discipline.Any())
                {
                    ViewBag.hasDiscipline = "true";
                }
                else
                {
                    ViewBag.hasDiscipline = "false";
                }

                /*sections*/
                IQueryable<section> section = from di in _db.section where di.encadrant_id == id select di;
                ViewBag.section = section.ToList<section>();
                if (!section.Any())
                {
                    ViewBag.hasSection = "true";
                }
                else
                {
                    ViewBag.hasSection = "false";
                }

                /*seance*/
                IQueryable<seance> seance = from di in _db.seance where di.encadrant_id == id select di;
                ViewBag.seance = seance.ToList<seance>();
                if (!seance.Any())
                {
                    ViewBag.hasSeance = "true";
                }
                else
                {
                    ViewBag.hasSeance = "false";
                }
                return View(discipline.ToList());
            }
            else
            {
                if (User.Identity.Name != null && User.IsInRole("admin"))
                {

                    return View(_db.discipline.ToList());
                }
                else
                {
                    /*discipline*/
                    IQueryable<discipline> discipline = from di in _db.discipline select di;
                    ViewBag.disciplineAll = discipline.ToList<discipline>();
                    if (!discipline.Any())
                    {
                        ViewBag.hasDiscipline = "true";
                    }
                    else
                    {
                        ViewBag.hasDiscipline = "false";
                    }


                    return View(discipline.ToList());
                }
            }


        }

        // GET: discipline/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            discipline discipline = _db.discipline.Find(id);
            if (discipline == null)
            {
                return HttpNotFound();
            }
            return View(discipline);
        }

        // GET: discipline/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: discipline/Create
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "discipline_id")] discipline discipline)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _db.discipline.Add(discipline);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {

                return View();
            }


        }

        // GET: discipline/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            discipline discipline = _db.discipline.Find(id);
            if (discipline == null)
            {
                return HttpNotFound();
            }

            return View(discipline);
        }

        // POST: discipline/Edit/5
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "discipline_id,discipline_nom,encadrant_id,description")] discipline discipline)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(discipline).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(discipline);
        }

        // GET: disciplines/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            discipline discipline = _db.discipline.Find(id);
            if (discipline == null)
            {
                return HttpNotFound();
            }
            return View(discipline);
        }

        // POST: disciplines/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            discipline discipline = _db.discipline.Find(id);
            _db.discipline.Remove(discipline);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
