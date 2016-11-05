using DAL.Repo;
using Entities.Models;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ServiceResultat
    {

        static public void HandleResultats(ResultsViewModel resultvm)
        {

            resultvm.audit = ServiceAudit.getAuditdetached(resultvm.auditid);
            if (!resultvm.audit.isInProgress)
            {
                resultvm.audit.isInProgress = true;
                AddResultats(resultvm);
            }
            else
            {
                UpdateResultats(resultvm);
            }

        }

        static public void AddResultats(ResultsViewModel resultvm)
        {

            using (ResultatRepository resultatrepo = new ResultatRepository())
            {
                //geeting the audit and setting it to modified automatically 
                resultatrepo.context.Entry(resultvm.audit).State = EntityState.Modified;

                //geeting the themes and the points and setting it to unchanged so it wont be readded to the database that will be hell oh no no 
                resultvm.themes = ServiceTheme.GetAllThemes();
                foreach (var item in resultvm.themes)
                {
                    resultatrepo.context.Entry(item).State = EntityState.Unchanged;
                }

                //a workaround to reattach the relations the results you can say that this is MAGIC
                int compteur = -1;
                foreach (var item in resultvm.themes)
                {
                    foreach (var inneritem in item.points)
                    {
                        compteur++;
                        resultvm.resultats[compteur].audit = resultvm.audit;
                        resultvm.resultats[compteur].theme = item;
                        resultvm.resultats[compteur].point = inneritem;
                        resultatrepo.Add(resultvm.resultats[compteur]);
                    }
                }
                resultatrepo.Save();
            }
        }

        static public void UpdateResultats(ResultsViewModel resultvm)
        {

            using (ResultatRepository resultatrepo = new ResultatRepository())
            {
                //geeting the audit and setting it to unchanged so it wont be readded to the database that will be hell oh no no
                resultatrepo.context.Entry(resultvm.audit).State = EntityState.Unchanged;

                //geeting the themes and the points and setting it to unchanged so it wont be readded to the database that will be hell oh no no 
                resultvm.themes = ServiceTheme.GetAllThemes(resultvm.audit);
                foreach (var item in resultvm.themes)
                {
                    resultatrepo.context.Entry(item).State = EntityState.Unchanged;
                }



                List<int> ResultPoints = new List<int>();
                foreach (var item in resultvm.audit.resultats)
                {
                    ResultPoints.Add(item.point.PointID);
                }
                List<Point> PointTemp1 = new List<Point>();

                //foreach (var item in ServicePoint.GetAllPoints())
                //{

                //}

                foreach (var theme in resultvm.themes)
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

            




                //getting the results from the database and updating them
                Resultat tempresultat = null;
                int compteur = -1;
                foreach (var item in resultvm.themes)
                {
                    foreach (var inneritem in item.points)
                    {
                        compteur++;
                        tempresultat = ServiceResultat.getresultatbyCriteria(resultvm.audit, item, inneritem);
                        tempresultat.audit = resultvm.audit;
                        tempresultat.theme = item;
                        tempresultat.point = inneritem;
                        tempresultat.Note = resultvm.resultats[compteur].Note;
                        tempresultat.req_comment = resultvm.resultats[compteur].req_comment;

                        resultatrepo.Update(tempresultat);
                    }
                }
                resultatrepo.Save();
            }
        }

        static public Resultat getresultatbyCriteria(Audit audit, Theme theme, Point point)
        {
            Resultat temp;
            using (ResultatRepository resultatrepo = new ResultatRepository())
            {
                temp = resultatrepo.GetSingleByAuThPnt(audit, theme, point);
                resultatrepo.context.Entry(temp).State = EntityState.Detached;
            }
            return temp;
        }

        static public List<Resultat> getAuditResults(Audit audit)
        {
            List<Resultat> temp;
            using (ResultatRepository resultatrepo = new ResultatRepository())
            {
                temp = (List<Resultat>) resultatrepo.GetResultsforAudit(audit);
                foreach (var item in temp)
                {
                    resultatrepo.context.Entry(item).State = EntityState.Detached;
                }
            }
            return temp;
        }

        static public void UpdateoneResultat(Resultat res)
        {

            using (ResultatRepository resultatrepo = new ResultatRepository())
            {
                resultatrepo.Update(res);
            }

        }

        static public List<Resultat> getAuditresults(Audit audit)
        {
            List<Resultat> temp;
            using (ResultatRepository resultatrepo = new ResultatRepository())
            {
                temp = (List<Resultat>)resultatrepo.GetAuditResults(audit);
                foreach (var item in temp)
                {
                    resultatrepo.context.Entry(item).State = EntityState.Detached;
                }
                

            }
            return temp;
        }
    }
}
