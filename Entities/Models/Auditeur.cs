using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Auditeur
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Matricule")]
        public int AuditeurId { get; set; }
        [Required]
        public string Nom  { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        //[RegularExpression("^(Service|UF)$")] 
        //hedhi el ma9soud biha lezmou ikoun soit tebe3 Service (informatique, maintenance,...)
        //wela tebe3 une Unité de Fabrication 
        public string Affectation  { get; set; }
        [Required]
        public string Telephone  { get; set; }
        


        [Display(Name = "Date de l’introduction dans la base ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateIntroBaseAuditeur { get; set; }

        public virtual List<Audit> audits { get; set; }
    }
}
