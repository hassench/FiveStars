using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class ResultsViewModel
    {
        public int auditid { get; set; }
        public Audit audit { get; set; }
        public List<Resultat> resultats { get; set; }
        public List<Theme> themes { get; set; }
    }
}
