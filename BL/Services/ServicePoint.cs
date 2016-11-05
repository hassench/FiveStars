using DAL.Repo;
using Entities.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace BL.Services
{
    public class ServicePoint
    {
        static public Point GetPointByName(string Pointname)
        {
            Point Pointtemp = null;
            using (PointRepository Pointrepo = new PointRepository())
            {
                Pointtemp = Pointrepo.GetSingleByName(Pointname);
            }

            return Pointtemp;
        }

        static public Point GetPoint(int Pointid)
        {
            Point Pointtemp = null;
            using (PointRepository Pointrepo = new PointRepository())
            {
                Pointtemp = Pointrepo.GetSingle(Pointid);
                Pointrepo.context.Entry(Pointtemp).State = EntityState.Detached;
            }

            return Pointtemp;
        }

        static public List<Point> GetAllPoints()
        {
            List<Point> Pointtemp = null;
            using (PointRepository Pointrepo = new PointRepository())
            {
                Pointtemp = (List<Point>)Pointrepo.GetAll();
                foreach (Point Point in Pointtemp)
                {
                    Pointrepo.context.Entry(Point).State = EntityState.Detached;
                }

            }

            return Pointtemp;
        }
        static public void updatePoint(Point point)
        {
            using (PointRepository Pointrepo = new PointRepository())
            {

                Pointrepo.Update(point);
                Pointrepo.Save();
            }

        }
        static public void addPoint(Point Point)
        {

            using (PointRepository Pointrepo = new PointRepository())
            {
                List<Point> Pointtemp = null;

                Pointtemp = (List<Point>)Pointrepo.GetAll();
                //foreach (Point point in Pointtemp)
                //{
                //    Pointrepo.context.Entry(Point).State = EntityState.Detached;
                //}

                foreach (var item in Pointtemp)
                {
                    if (item.NumPoint >= Point.NumPoint)
                    {
                        item.NumPoint++;
                        //Pointrepo.Update(item);
                        Pointrepo.Save();

                    }
                }
                Pointrepo.Add(Point);
                Pointrepo.Save();
            }


        }

        static public void deletePoint(int id)
        {
            using (PointRepository Pointrepo = new PointRepository())
            {
                Point temp = GetPoint(id);
                Pointrepo.Delete(temp);
                Pointrepo.Save();
            }
        }
        static public List<Point> GetAllPointsForAudit(Audit audit)
        {
            List<Point> ResultPoints = new List<Point>();
            using (PointRepository Pointrepo = new PointRepository())
            {
                
                foreach (var item in audit.resultats)
                {
                    ResultPoints.Add(item.point);
                } 
            }
            return ResultPoints;
        }
    }
}