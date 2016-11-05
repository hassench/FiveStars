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
using Entities.ViewModels;

namespace PL.Controllers
{
    [Authorize(Roles = "Pilote")]
    public class PDCAsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PDCAs
        public ActionResult Index()
        {
            List<PDCA>      temp = ServicePDCA.GetPDCAByPilote(User.Identity.Name);
            ApplicationUser user = ServiceUser.getUserWD(User.Identity.Name);
            Zone zone = ServiceZone.GetZoneBypliote(user.pilote);
            //checking if pilote already asked for a certifcation
            Semaine semaine = ServiceSemaine.getLastSemaineWD();
            bool certificationasked = false;
            List<Audit> auditscertif=  semaine.audits.Where(a => a.TypeAudit.Equals("Certification")).ToList<Audit>();
            foreach (var item in auditscertif)
            {
                if (item.zone.PiloteZoneObli.PiloteId==user.pilote.PiloteId )
                {
                    certificationasked = true;
                }
                if (item.zone.PiloteZoneOpti!=null)
                {
                    if (item.zone.PiloteZoneOpti.PiloteId==user.pilote.PiloteId )
                {
                    certificationasked = true;
                }
                }
            }
            PilotepdcaViewModel PPVM = new PilotepdcaViewModel { pdcas = temp, zone = zone, pilote = user.pilote, certificationasked = certificationasked };
            return View(PPVM);
        }

        
        [AllowAnonymous]
        public ActionResult Zone(int? id)
        {
            if (id != null)
            {
                List<PDCA> temp = ServicePDCA.GetPDCAByZone(id.Value);

                ViewBag.NomZone = ServiceZone.GetZone(id.Value) != null ? ServiceZone.GetZone(id.Value).NomZone : "Cette zone n'existe pas";
                return View(temp);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

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
