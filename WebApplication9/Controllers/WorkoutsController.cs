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
    
    public class WorkoutsController : Controller
    {
        private WebApplication9Context db = new WebApplication9Context();

        // GET: Workouts
        public ActionResult Index()
        {
            var workouts = db.Workouts.Include(w => w.Runner);
            return View(workouts.ToList());
        }

        // GET: Workouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = db.Workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            return View(workout);
        }

        // GET: Workouts/Create
        [Authorize()]
        public ActionResult Create()
        {
            ViewBag.RunnerID = new SelectList(db.Runners, "RunnerID", "Name");
            return View();
        }

        // POST: Workouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkoutID,Date,Distance,Hours,Minutes,Seconds,Place,Note,RunnerID")] Workout workout)
        {
            if (ModelState.IsValid)
            {
                var runners = db.Runners.ToList();
                foreach (Runner aRunner in runners)
                {
                    if (aRunner.RunnerID.Equals(workout.RunnerID))
                    {
                        aRunner.Mileage = aRunner.Mileage + workout.Distance;
                        aRunner.NumberOfTraining++;
                        db.Entry(aRunner).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                db.Workouts.Add(workout);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RunnerID = new SelectList(db.Runners, "RunnerID", "Name", workout.RunnerID);
            return View(workout);
        }

        // GET: Workouts/Edit/5
        [Authorize()]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = db.Workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            ViewBag.RunnerID = new SelectList(db.Runners, "RunnerID", "Name", workout.RunnerID);
            return View(workout);
        }

        // POST: Workouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkoutID,Date,Distance,Hours,Minutes,Seconds,Place,Note,RunnerID")] Workout workout)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workout).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RunnerID = new SelectList(db.Runners, "RunnerID", "Name", workout.RunnerID);
            return View(workout);
        }

        // GET: Workouts/Delete/5
        [Authorize()]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = db.Workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            return View(workout);
        }

        // POST: Workouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Workout workout = db.Workouts.Find(id);
            var runners = db.Runners.ToList();
            foreach (Runner aRunner in runners)
            {
                if (aRunner.RunnerID.Equals(workout.RunnerID))
                {
                    aRunner.Mileage = aRunner.Mileage - workout.Distance;
                    aRunner.NumberOfTraining--;
                }
            }

            db.Workouts.Remove(workout);
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
