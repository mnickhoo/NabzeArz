using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NabzeArz.Models;
using IdentitySample.Models;

namespace NabzeArz.Controllers
{
    public class CurrencyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Currency
        public ActionResult Index()
        {
            return View(db.Currencies.ToList());
        }

        // GET: Currency/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyModel currencyModel = db.Currencies.Find(id);
            if (currencyModel == null)
            {
                return HttpNotFound();
            }
            return View(currencyModel);
        }

        // GET: Currency/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Currency/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,symbol,Name,persianName")] CurrencyModel currencyModel)
        {
            if (ModelState.IsValid)
            {
                db.Currencies.Add(currencyModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(currencyModel);
        }

        // GET: Currency/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyModel currencyModel = db.Currencies.Find(id);
            if (currencyModel == null)
            {
                return HttpNotFound();
            }
            return View(currencyModel);
        }

        // POST: Currency/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,symbol,Name,persianName")] CurrencyModel currencyModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(currencyModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(currencyModel);
        }

        // GET: Currency/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyModel currencyModel = db.Currencies.Find(id);
            if (currencyModel == null)
            {
                return HttpNotFound();
            }
            return View(currencyModel);
        }

        // POST: Currency/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CurrencyModel currencyModel = db.Currencies.Find(id);
            db.Currencies.Remove(currencyModel);
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
