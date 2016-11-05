using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL.Models;
using Entities.Models;
using BL.Services;

namespace PL.Controllers
{
    public class PointsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Points
        public ActionResult Index()
        {
            BL.Services.ServicePoint.GetAllPoints();
            return View(db.Points.ToList().OrderBy(x=>x.NumPoint));
        }

        public ActionResult AddPointToTheme(Point point)
        {


            Theme theme;
            string idS = Request["theme.ThemeId"];
            int id = int.Parse(idS);
            theme=ServiceTheme.GetTheme(id);
            
            
                //if (ModelState.IsValid)
                //{
                    try
                    {

                        BL.Services.ServicePoint.addPoint(point);

                //        pointrepo.context.Entry(user).State = EntityState.Unchanged;
                //        pointrepo.context.Entry(theme).State = EntityState.Unchanged;
                //        pointrepo.Save();
                        return Redirect("/Themes/Details/" + id);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                //}

            

            return View(point);

        }

        // GET: Points/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Point point = db.Points.Find(id);
            if (point == null)
            {
                return HttpNotFound();
            }
            return View(point);
        }

        // GET: Points/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Points/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PointID,NumPoint,NomPoint,Coef")] Point point)
        {
            if (ModelState.IsValid)
            {
                db.Points.Add(point);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(point);
        }

        // GET: Points/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Point point = db.Points.Find(id);
            if (point == null)
            {
                return HttpNotFound();
            }
            return View(point);
        }

        // POST: Points/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PointID,NumPoint,NomPoint,Coef")] Point point)
        {
            if (ModelState.IsValid)
            {
                db.Entry(point).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(point);
        }

        // GET: Points/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Point point = db.Points.Find(id);
            if (point == null)
            {
                return HttpNotFound();
            }
            return View(point);
        }

        // POST: Points/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BL.Services.ServicePoint.deletePoint(id);
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
