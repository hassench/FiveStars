using BL.Services;
using DAL.Models;
using DAL.Repo;
using Entities.Models;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoSharingWebApp.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult post(CommentViewModel commentvm)
        {

            commentvm.comment.user = ServiceUser.getUser(User.Identity.Name);


            commentvm.comment.zone = ServiceZone.GetZonedetached(commentvm.ZoneId);



            if (ModelState.IsValid)
            {


                ServiceComment.addComment(commentvm.comment);

                return Redirect("/Suivi/Zone/" + commentvm.ZoneId);


            }

            return View(commentvm.comment);
        }
    }
}