using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entities.Models;
using BL.Services;
using DAL.Models;
using Entities.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace PL.Controllers
{
    public class ZonesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        public ZonesController()
        {
        }

        public ZonesController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Zones
        public ActionResult Index()
        {
            return View(db.Zones.ToList());
        }

        // GET: Zones/Details/5
        public ActionResult Details(int id)
        {

            Zone zone = db.Zones.Find(id);
            if (zone == null)
            {
                return HttpNotFound();
            }
            return View(zone);
        }

        // GET: Zones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Zones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] ZoneAddViewModel zonevm)
        {
            Zone zone = getZoneObject(zonevm);
            if (ModelState.IsValid)
            {

                Zone zonetemp = ServiceZone.GetZoneByName(zone.NomZone);

                if (zonetemp != null)
                {
                    ModelState.AddModelError("NomZone", "Cette Zone nommé " + zone.NomZone + " est deja inscrit dans la Base");
                    return View(zone);
                }


                try
                {
                    ServiceZone.addzone(zone);
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
                //when successfull it delivers a full list of zones
                return RedirectToAction("Index");
            }

            return View(zone);
        }
        public Zone getZoneObject(ZoneAddViewModel zonevm)
        {
            Zone zone = new Zone();
            zone.DateIntroBaseZone = DateTime.Now;
            zone.NomZone = zonevm.NomZone;
            zone.Appartenance = zonevm.Appartenance;
            zone.TypeZone = zonevm.TypeZone;
            zone.ResponsableHierarZone = zonevm.ResponsableHierarZone;

            zone.PiloteZoneObli = RegisterPilote(zonevm.EmailPObli, zonevm.PasswordPObli, zonevm.PiloteZoneObli);

            if (zonevm.PiloteZoneOpti != null)
            {
                 zone.PiloteZoneObli = RegisterPilote(zonevm.EmailPOpti, zonevm.PasswordPOpti, zonevm.PiloteZoneOpti);
            }
            return zone;
        }

        public Pilote RegisterPilote(String email, String password, String NomPilote)
        {

            Pilote formpilote = new Pilote { NomPilote = NomPilote, DateIntroBasePilote = DateTime.Now };

            var user = new ApplicationUser() { UserName = email, Email = email, pilote = formpilote };

            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            if (!roleManager.RoleExists("Pilote"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Pilote";
                roleManager.Create(role);
            }

            if (userManager.FindByName(user.UserName) == null)
            {

                userManager.Create(user, password);
            }
            ///////

            user = userManager.FindByName(user.UserName);
            ICollection<IdentityUserRole> roles = new List<IdentityUserRole>();
            if (user.Roles != null)
            {
                roles = user.Roles;
            }
            var roleExixtes = false;

            foreach (var item in roles)
            {
                if (roleManager.RoleExists("Pilote"))
                {
                    roleExixtes = true;
                }
            }
            if (!roleExixtes)
            {
                userManager.AddToRole(user.Id, "Pilote");
            }

            return formpilote;
        }


        // GET: Zones/Edit/5
        public ActionResult Edit(int id)
        {

            Zone zone = ServiceZone.GetZone(id);

            if (zone == null)
            {
                return HttpNotFound();
            }
            return View(zone);
        }

        // POST: Zones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ZoneId,NomZone,Appartenance,ResponsableHierarZone,PiloteZoneObli,PiloteZoneOpti,TypeZone,DateIntroBaseZone")] Zone zone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zone);
        }

        // GET: Zones/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zone zone = db.Zones.Find(id);
            if (zone == null)
            {
                return HttpNotFound();
            }
            return View(zone);
        }

        // POST: Zones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Zone zone = db.Zones.Find(id);
            db.Zones.Remove(zone);
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
