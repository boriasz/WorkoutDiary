using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication9.Models;

namespace WebApplication9.Controllers
{
    public class RunnersController : Controller
    {
        private WebApplication9Context db = new WebApplication9Context();

        // GET: Runners
        public ActionResult Index()
        {
            return View(db.Runners.ToList());
        }

        // GET: Runners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Runner runner = db.Runners.Find(id);
            
            if (runner == null)
            {
                return HttpNotFound();
            }
            return View(runner);
        }

        // GET: Runners/Create
        [Authorize()]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Runners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RunnerID,Name,Surname,YOB,Weight,Mileage,NumberOfTraining")] Runner runner)
        {
            
            if (ModelState.IsValid)
            {
                
                
                    db.Runners.Add(runner);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                
            }

            return View(runner);
        }

        // GET: Runners/Edit/5
        [Authorize()]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Runner runner = db.Runners.Find(id); 
            if (runner == null)
            {
                return HttpNotFound();
            }
            return View(runner);
        }

        // POST: Runners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RunnerID,Name,Surname,YOB,Weight,Mileage,NumberOfTraining")] Runner runner)
        {
            if (ModelState.IsValid)
            {
               
                
                    db.Entry(runner).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                
            }
            return View(runner);
        }

        // GET: Runners/Delete/5
        [Authorize()]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Runner runner = db.Runners.Find(id);
            if (runner == null)
            {
                return HttpNotFound();
            }
            return View(runner);
        }

        // POST: Runners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Runner runner = db.Runners.Find(id);
            db.Runners.Remove(runner);
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
