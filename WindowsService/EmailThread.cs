using BL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace WindowsService
{
    class EmailThread
    {
        public void DoWork()
        {
            while (Thread.CurrentThread.IsAlive)
            {
                if ((DateTime.Now.DayOfWeek == DayOfWeek.Tuesday) &&
                    (DateTime.Now.Hour == 00) &&
                    (DateTime.Now.Minute == 55))
                {
                    //Business b = new Business();
                    //b.Assign();
                    File.Create("D:\\Yourfile.txt");
                }
                Thread.Sleep(60000);
            }
        }
    }
}
