using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Resultat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResultatId { get; set; }

        
        public int? Note { get; set; }

       
        public string req_comment { get; set; }


        public virtual Audit audit { get; set; }
        public virtual Theme theme { get; set; }
        public virtual Point point { get; set; }
    }
}
