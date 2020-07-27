using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Models;
using NabzeArz.Models.Nerkh;
using NabzeArz.Service;

namespace NabzeArz.Areas.Admin.Controllers
{
    [Authorize]
    public class CurrencyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Currency
        public ActionResult Index()
        {
            return View(db.CurrencyRates.OrderBy(o => o.order_list).ToList());
        }

        // GET: Admin/Currency/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyRate currencyRate = db.CurrencyRates.Find(id);
            if (currencyRate == null)
            {
                return HttpNotFound();
            }
            return View(currencyRate);
        }

        // GET: Admin/Currency/Create
        public ActionResult Create()
        {
            return View();
        }
        public JsonResult ChangeOrder(string[] item)
        {
            NerkhAPIService.ChangeOrderList(item);
            return Json(new { response = "successfull" });
        }
        // POST: Admin/Currency/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,symbol,min,max,current,changePercent,changePrice,showIntoChannel,fa,en,order_list")] CurrencyRate currencyRate)
        {
            if (ModelState.IsValid)
            {
                db.CurrencyRates.Add(currencyRate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(currencyRate);
        }

        // GET: Admin/Currency/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyRate currencyRate = db.CurrencyRates.Find(id);
            if (currencyRate == null)
            {
                return HttpNotFound();
            }
            return View(currencyRate);
        }

        // POST: Admin/Currency/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,symbol,min,max,current,changePercent,changePrice,showIntoChannel,fa_name,en_name,order_list")] CurrencyRate currencyRate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(currencyRate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(currencyRate);
        }

        // GET: Admin/Currency/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyRate currencyRate = db.CurrencyRates.Find(id);
            if (currencyRate == null)
            {
                return HttpNotFound();
            }
            return View(currencyRate);
        }

        // POST: Admin/Currency/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CurrencyRate currencyRate = db.CurrencyRates.Find(id);
            db.CurrencyRates.Remove(currencyRate);
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
