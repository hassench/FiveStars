using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Audit
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuditId { get; set; }
       
        public int FiveStarsLevel  { get; set; }
        public int Note { get; set; }
        public String TypeAudit { get; set; }

        public bool isInProgress { get; set; }
        public bool isCompleted { get; set; }
        [Display(Name = "Date de l’Audit ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dddd dd MMMM yyyy}")]
        public DateTime? AuditDay { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfCompletion { get; set; }

        public virtual List<Resultat> resultats { get; set; }
        public virtual Semaine semaine { get; set; }
        public virtual Auditeur auditeur { get; set; }
        public virtual Zone zone { get; set; }

    }
}

