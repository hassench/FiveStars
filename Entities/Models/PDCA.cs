using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class PDCA
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PDCAId { get; set; }
        public string UF { get; set; }
        public string NLigne { get; set; }
        public string Machine { get; set; }
        public string Theme { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }
        
        [Display(Name = "ANOMALIE / AMELIORATION")]
        public string Anomalie { get; set; }
        [RegularExpression("^(Importante|Critique|Vitale|Test)$")]
        public string Criticite { get; set; }
        [Display(Name = "Actions de Fond /  Progrès")]
        public string Action { get; set; }
        public string Pilote { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Delai { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Réalisé")]
        public DateTime? DateRealise { get; set; }
        
        [Display(Name = "Vérification de l'efficacité")]
        public string VerifEfficacite { get; set; }
        
        [Display(Name = "Commentaires / Suivi")]
        public string Commentaire { get; set; }
        public bool P { get; set; }
        public bool D { get; set; }
        public bool C { get; set; }

        public virtual Resultat resultat { get; set; }
    }
}
