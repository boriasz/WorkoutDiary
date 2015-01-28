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
    public class ShoesController : Controller
    {
        private WebApplication9Context db = new WebApplication9Context();

        // GET: Shoes
        public ActionResult Index()
        {
            var shoes = db.Shoes.Include(s => s.Runner);
            return View(shoes.ToList());
        }

        // GET: Shoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shoes shoes = db.Shoes.Find(id);
            if (shoes == null)
            {
                return HttpNotFound();
            }
            return View(shoes);
        }

        // GET: Shoes/Create
        [Authorize()]
        public ActionResult Create()
        {
            ViewBag.RunnerID = new SelectList(db.Runners, "RunnerID", "Name");
            return View();
        }

        // POST: Shoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShoesID,Brand,Model,Price,RunnersWeight,RunnerID")] Shoes shoes)
        {
            if (ModelState.IsValid)
            {
                var runners = db.Runners.ToList();
                foreach (Runner aRunner in runners)
                {
                    
                    
                    if (aRunner.RunnerID.Equals(shoes.RunnerID))
                    {
                        
                        if (aRunner.Weight > shoes.RunnersWeight)
                        {
                            return View("ToHeavy");
                        }
                    }
                }
                db.Shoes.Add(shoes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RunnerID = new SelectList(db.Runners, "RunnerID", "Name", shoes.RunnerID);
            return View(shoes);
        }

        // GET: Shoes/Edit/5
        [Authorize()]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shoes shoes = db.Shoes.Find(id);
            if (shoes == null)
            {
                return HttpNotFound();
            }
            ViewBag.RunnerID = new SelectList(db.Runners, "RunnerID", "Name", shoes.RunnerID);
            return View(shoes);
        }

        // POST: Shoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShoesID,Brand,Model,Price,RunnersWeight,RunnerID")] Shoes shoes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RunnerID = new SelectList(db.Runners, "RunnerID", "Name", shoes.RunnerID);
            return View(shoes);
        }

        // GET: Shoes/Delete/5
        [Authorize()]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shoes shoes = db.Shoes.Find(id);
            if (shoes == null)
            {
                return HttpNotFound();
            }
            return View(shoes);
        }

        // POST: Shoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shoes shoes = db.Shoes.Find(id);
            db.Shoes.Remove(shoes);
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
