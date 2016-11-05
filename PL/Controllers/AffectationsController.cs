using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BL.Services;
using Entities.Models;
using System.Web.Helpers;

namespace PL.Controllers
{
    public class AffectationsController : Controller
    {


        // GET: Affectations
        public ActionResult Index()
        {
            List<Audit> temp = ServiceAudit.Getthisweekaudits();
            return View(temp);
        }

        public ActionResult DrawChart()
        {
            List<Audit> temp = ServiceAudit.Getthisweekaudits();
            int Pending = temp.Where(a => !a.isInProgress && !a.isCompleted).ToList().Count;
            int InProgress = temp.Where(a => a.isInProgress && !a.isCompleted).ToList().Count;
            int Completed = temp.Where(a => !a.isInProgress && a.isCompleted).ToList().Count;


            var myTheme = @"
<Chart PaletteCustomColors=""255, 50,50;255,102, 0;0, 255, 0"" BackColor=""#CECECE"" BackGradientStyle=""TopBottom"" BackSecondaryColor=""White"" BorderColor=""26, 59, 105"" BorderlineDashStyle=""Solid"" BorderWidth=""2"" Palette=""None"">
   <Series>
      <series _Template_=""All"" BorderColor=""180, 26, 59, 105"" CustomProperties=""LabelStyle=Outside"" IsValueShownAsLabel=""False"" Label=""#VALY #VALX""  />
   </Series>
   <ChartAreas>
      <ChartArea Name=""Default"" _Template_=""All"" BackColor=""64, 165, 191, 228"" BackGradientStyle=""TopBottom"" BackSecondaryColor=""White"" BorderColor=""64, 64, 64, 64"" BorderDashStyle=""Solid"" ShadowColor=""Transparent"" >
         <AxisX>
            <LabelStyle Format=""{#VALY}"" />
         </AxisX>
         <Area3DStyle
                Enable3D=""True""
                Inclination=""20""
                LightStyle=""Realistic""
                Perspective=""20""
                IsRightAngleAxes=""True""
                IsClustered=""False"" />
      </ChartArea>
   </ChartAreas>
   <Legends>
      <Legend _Template_=""All"" BackColor=""Transparent"" Font=""Trebuchet MS, 8.25pt, style=Bold"" IsTextAutoFit=""False"" />
   </Legends>
   <BorderSkin SkinStyle=""None"" />
</Chart>";

            var chart = new Chart(width: 800, height: 400, theme: myTheme)
                .AddSeries(
                            chartType: "Pie",
                            xValue: new[] { "En attente", "En cours", "Terminé" },
                            yValues: new[] { Pending, InProgress, Completed })
                            //.AddLegend()
                            .AddTitle("Aperçu sur l'état des audits de cette semaine");

            return File(chart.GetBytes("png"), "image/png");
        }


    }
}
