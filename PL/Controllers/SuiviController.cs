using BL.Services;
using Entities.Models;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class SuiviController : Controller
    {
        // GET: Suivi
        public ActionResult Index()
        {
            SuiviViewModel sv = new SuiviViewModel();
            sv.zones = ServiceZone.GetAllZones();
            sv.appart = new List<string> { "UF", "ATR", "STB1", "STB2", "ENEL", "Process", "Maintenance", "Magazin" };
            return View(sv);
        }
        public ActionResult Zone(int id)
        {
            ZoneViewModel zn = new ZoneViewModel();
            zn.zone = ServiceZone.GetZone(id);
            zn.niveau = ServiceZone.GetZone5StarsLevel(zn.zone);
            zn.Note = ServiceZone.GetZoneNote(zn.zone);
            return View(zn);

        }
        public ActionResult PDCAZone(int? idAudit, int? idResultat) 
        {
            ResultsViewModel resultats = new ResultsViewModel();
            resultats.auditid = idAudit.Value;
            resultats.audit = ServiceAudit.getAudit(idAudit.Value);
            resultats.resultats = ServiceResultat.getAuditresults(resultats.audit);
            resultats.themes = ServiceTheme.GetAllThemeswithoutdetaching(resultats.audit);
            ViewBag.idResultat = idResultat;
            return View(resultats);
        }

        public ActionResult DernierResultats(int id)
        {
            ResultsViewModel resultats = new ResultsViewModel();

            Audit currentaudit = ServiceAudit.getlatestAuditforZone(id);
            resultats.audit = currentaudit;

            resultats.themes = ServiceTheme.GetAllThemeswithoutdetaching(currentaudit);

            resultats.resultats = ServiceResultat.getAuditresults(currentaudit);

            return View(resultats);

        }

        public ActionResult DrawChart(int zoneid)
        {
            List<Audit> temp = ServiceAudit.getalllatestAuditsforZone(zoneid);

            List<String> lastauditdates = new List<String>(temp.Count);
            List<String> lastlevels = new List<String>(temp.Count);
            DateTime workaroungtogetdate = new DateTime();
            foreach (var item in temp)
            {
                lastlevels.Insert(0, item.FiveStarsLevel.ToString());
                workaroungtogetdate = (DateTime)item.DateOfCompletion;
                lastauditdates.Insert(0, workaroungtogetdate.ToString("dd/MM/yyyy"));
            }
            var myTheme = @"
<Chart BackColor=""#CECECE"" BackGradientStyle=""TopBottom"" BackSecondaryColor=""White"" BorderColor=""26, 59, 105"" BorderlineDashStyle=""Solid"" BorderWidth=""2"" Palette=""BrightPastel"">
   <Series>
      <series _Template_=""All"" BorderColor=""180, 26, 59, 105"" BorderWidth=""5"" CustomProperties=""LabelStyle=Outside"" IsValueShownAsLabel=""False"" />
   </Series>
   <ChartAreas>
      <ChartArea Name=""Default"" _Template_=""All"" BackColor=""64, 165, 191, 228"" BackGradientStyle=""TopBottom"" BackSecondaryColor=""White"" BorderColor=""64, 64, 64, 64"" BorderDashStyle=""Solid"" ShadowColor=""Transparent"">
         <AxisY>
            <LabelStyle Format=""{#} ★"" />
         </AxisY>
      </ChartArea>
   </ChartAreas>
   <Legends>
      <Legend _Template_=""All"" BackColor=""Transparent"" Font=""Trebuchet MS, 8.25pt, style=Bold"" IsTextAutoFit=""False"" />
   </Legends>
   <BorderSkin SkinStyle=""None"" />
</Chart>";
            Chart chart;
            if (lastlevels.Count==1)
            {
                chart = new Chart(width: 800, height: 200, theme: myTheme)
                .AddTitle("la courbe de tendance depuis le 1er audit 5 stars ")
                .AddSeries(
                            chartType: "Bar",
                    //x  & yValues must be of the same type to sync properly
                            xValue: lastauditdates,
                            yValues: lastlevels)
                            .SetYAxis(max: 5);
            }
            else
            {
                chart = new Chart(width: 800, height: 200, theme: myTheme)
                .AddTitle("la courbe de tendance depuis le 1er audit 5 stars ")
                .AddSeries(
                            chartType: "Line",
                    //x  & yValues must be of the same type to sync properly
                            xValue: lastauditdates,
                            yValues: lastlevels)
                            .SetYAxis(max: 5);
            }




            return File(chart.GetBytes("png"), "image/png");
        }


    }
}