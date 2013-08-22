using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KnockoutSamples.Models;

namespace KnockoutSamples.Controllers
{
    public class LookupsController : Controller
    {
        private KnockoutSamplesContext db = new KnockoutSamplesContext();

        //
        // GET: /Lookups/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetIndex()
        {
            return Json(db.Lookups.ToList(), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Lookups/Details/5
        public ActionResult Details(Int32 id)
        {
            Lookup lookup = db.Lookups.Find(id);
            if (lookup == null)
            {
                return HttpNotFound();
            }
            return View(lookup);
        }

        //
        // GET: /Lookups/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Lookups/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Lookup lookup)
        {
            if (ModelState.IsValid)
            {
                db.Lookups.Add(lookup);
                db.SaveChanges();
                return Json(lookup.Id);
            }

            return View(lookup);
        }

        //
        // GET: /Lookups/Edit/5
        public ActionResult Edit(Int32 id)
        {
            Lookup lookup = db.Lookups.Find(id);
            if (lookup == null)
            {
                return HttpNotFound();
            }
            return View(lookup);
        }

        //
        // POST: /Lookups/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(Lookup lookup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lookup).State = EntityState.Modified;
                db.SaveChanges();
                return Json(lookup.Id);
            }
            return View(lookup);
        }

        //
        // GET: /Lookups/Delete/5
        public ActionResult Delete(Int32 id)
        {
            Lookup lookup = db.Lookups.Find(id);
            if (lookup == null)
            {
                return HttpNotFound();
            }
            return View(lookup);
        }

        //
        // POST: /Lookups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int32 id)
        {
            Lookup lookup = db.Lookups.Find(id);
            db.Lookups.Remove(lookup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
