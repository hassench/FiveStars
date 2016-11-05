using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BL.Services;
namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Business b = new Business();
            b.Assign();
            
            //Email sending test. it works like a charm 
            //ServiceEmail.sendHTMLEmail("ghd.abdallah@gmail.com", "ba33 ", @" <!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd""> <html xmlns=""http://www.w3.org/1999/xhtml""> <head> <title>Web Camps Training Kit - Home</title> <link rel=""Stylesheet"" type=""text/css"" media=""all"" href=""Assets/NavSite/styles/master.webcamps.css"" /> </head> <body> <a name=""top""></a> <div id=""outerWrapper""> <section class=""main""> <header> <span>Training Kit - </span> </header> <nav> <ul> <li class=""menu-link-selected""> <a href=""default.htm"">Home</a> </li> <li class=""menu-link""> <a href=""labs.htm"">Hands-on Labs</a> </li> <li class=""menu-link""> <a href=""presentations.htm"">Presentations</a> </li> <li class=""menu-link""> <a href=""agenda.htm"">Agenda</a> </li> </ul> </nav> </section> <section class=""content""> <h1>Web Camps Training Kit</h1> <p> Microsoft's Web Camps are training events that teach you how to build websites using the latest web technologies including ASP.NET MVC, ASP.NET 4.5, ASP.NET Web API, jQuery, HTML5 and Windows Azure. </p> <p> You can find out more about Web Camps happening near you and sign up here: <a href=""http://www.devcamps.ms/web"">www.devcamps.ms/web</a> </p> <p> This Web Camps Training Kit contains a set of training material like presentations and hands on labs you can use to learn and improve your web development skills. It aims at showing how to take advantage of the latest Microsoft Web Platform Technologies including Visual Studio 2012, ASP.NET 4.5, ASP.NET MVC 4 and ASP.NET Web API among others. </p> <p> These hands-on labs will guide you in creating Web applications which you can run on your own development machine. Instructions are also included for deploying your sites to Windows Azure Web Sites, which allows you to <a href=""http://aka.ms/aspnet-hol-azure"">host ten ASP.NET Web Sites for free</a>. </p> <p> We aim to update it every couple of months so you can always expect to have the latest learning material at your finger tips. If there are things that you would like us to include in the next version, then drop us a line: <a href=""mailto:webcamps@microsoft.com"">webcamps@microsoft.com</a>. </p> </section> <footer> <div class=""microsoftLink""> <a href=""http://www.microsoft.com/"" title=""Microsoft""> <img alt=""Microsoft"" src=""Assets/NavSite/images/ms-logo-footer.png"" /></a> </div> <div class=""leftSideLinks""> <span>Web Camps Training Kit</span>&nbsp;&nbsp;&nbsp; <a href=""mailto:webcamps@microsoft.com?subject=Web Camps Training Kit"">Contact Us</a>&nbsp;&nbsp;|&nbsp;&nbsp; <a href=""EULA.htm"" target=""_parent"">Terms of Use</a>&nbsp;&nbsp;|&nbsp;&nbsp; <a href="".\"">Browse Content</a>&nbsp;&nbsp;|&nbsp;&nbsp; &copy;&nbsp;2012 Microsoft</div> </footer> </div> </body> </html>");

            //Seedmethodtest seed = new Seedmethodtest();
            //seed.Seed();
            
            //Reporting.createReport();
        }
    }
}
