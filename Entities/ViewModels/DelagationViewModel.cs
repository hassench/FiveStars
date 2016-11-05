using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class DelagationViewModel
    {
        public int AuditId { get; set; }
        //this gets the Auditeur delegate, the name is just a stupid workaround
        public int audit { get; set; }
    }
}
