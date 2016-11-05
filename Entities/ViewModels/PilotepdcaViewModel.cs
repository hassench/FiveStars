using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class PilotepdcaViewModel
    {
        public List<PDCA> pdcas { get; set; }
        public Zone       zone { get; set; }
        public Pilote     pilote { get; set; }
        public bool       certificationasked { get; set; }
    }
}
