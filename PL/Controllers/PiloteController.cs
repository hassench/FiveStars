using BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class PiloteController : Controller
    {
        // GET: Pilote
        public ActionResult demandercertification(int? id)
        {
            ServiceAudit.demanderCertification((int)id);
            return RedirectToAction("Index","PDCAs");
        }
    }
}