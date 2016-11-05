using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PL
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Affectations", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PDCAZone",
                url: "Suivi/PDCAZone/{idAudit}/{idResultat}",
                defaults: new
                 {
                     controller = "Suivi",
                     action = "PDCAZone",
                     idAudit = UrlParameter.Optional,
                     idResultat = UrlParameter.Optional
                     // nothing optional 
                 }
);
        }
    }
}
