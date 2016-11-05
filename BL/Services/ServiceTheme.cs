using DAL.Repo;
using Entities.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace BL.Services
{
    public class ServiceTheme
    {
        static public Theme GetThemeByName(string Themename)
        {
            Theme Themetemp = null;
            using (ThemeRepository Themerepo = new ThemeRepository())
            {
                Themetemp = Themerepo.GetSingleByName(Themename);
            }

            return Themetemp;
        }

        static public Theme GetTheme(int Themeid)
        {
            Theme Themetemp = null;
            using (ThemeRepository Themerepo = new ThemeRepository())
            {
                Themetemp = Themerepo.GetSingle(Themeid);
            }

            return Themetemp;
        }

        static public List<Theme> GetAllThemes()
        {
            List<Theme> Themetemp = null;
            using (ThemeRepository Themerepo = new ThemeRepository())
            {
                Themetemp = (List<Theme>)Themerepo.GetAll();
                foreach (Theme theme in Themetemp)
                {
                    Themerepo.context.Entry(theme).State = EntityState.Detached;
                }

            }

            return Themetemp;
        }
        static public List<Theme> GetAllThemes(Audit audit)
        {
            List<Theme> Themetemp = null;
            using (ThemeRepository Themerepo = new ThemeRepository())
            {
                Themetemp = (List<Theme>)Themerepo.GetAll();
                foreach (Theme theme in Themetemp)
                {
                    Themerepo.context.Entry(theme).State = EntityState.Detached;
                }
                List<int> ResultPoints = new List<int>();
                foreach (var item in audit.resultats)
                {
                    ResultPoints.Add(item.point.PointID);
                }
                List<Point> PointTemp1 = new List<Point>();

                //foreach (var item in ServicePoint.GetAllPoints())
                //{

                //}

                foreach (var theme in Themetemp)
                {
                    PointTemp1.Clear();
                    foreach (var item1 in theme.points)
                    {
                        PointTemp1.Add(item1);
                    }
                    //for (int i = 0; i < PointTemp1.Count; i++)
                    //{
                    //    if (!ResultPoints.Contains(point.PointID))
                    //    {
                    //        theme.points.Remove(point);
                    //    }
                    //}
                    foreach (var point in PointTemp1)
                    {
                        if (!ResultPoints.Contains(point.PointID))
                        {
                            theme.points.Remove(point);
                        }
                    }
                }

            }

            return Themetemp;
        }

        static public List<Theme> GetAllThemeswithoutdetaching()
        {
            List<Theme> Themetemp = null;
            using (ThemeRepository Themerepo = new ThemeRepository())
            {
                Themetemp = (List<Theme>)Themerepo.GetAll();
        
            }

            return Themetemp;
        }
        static public List<Theme> GetAllThemeswithoutdetaching(Audit audit)
        {
            List<Theme> Themetemp = null;
            using (ThemeRepository Themerepo = new ThemeRepository())
            {
                Themetemp = (List<Theme>)Themerepo.GetAll();
                List<int> ResultPoints = new List<int>();
                foreach (var item in audit.resultats)
                {
                    ResultPoints.Add(item.point.PointID);
                }
                List<Point> PointTemp1= new List<Point>();

                //foreach (var item in ServicePoint.GetAllPoints())
                //{

                //}

                foreach (var theme in Themetemp)
                {
                    PointTemp1.Clear();
                    foreach (var item1 in theme.points)
                    {
                        PointTemp1.Add(item1);
                    }
                    //for (int i = 0; i < PointTemp1.Count; i++)
                    //{
                    //    if (!ResultPoints.Contains(point.PointID))
                    //    {
                    //        theme.points.Remove(point);
                    //    }
                    //}
                    foreach (var point in PointTemp1)
                    {
                        if (!ResultPoints.Contains(point.PointID))
                        {
                            theme.points.Remove(point);
                        }
                    }
                }

            }

            return Themetemp;
        }

        static public void addTheme(Theme Theme)
        {


            using (ThemeRepository Themerepo = new ThemeRepository())
            {
                Themerepo.Add(Theme);
                Themerepo.Save();
            }


        }
    }
}