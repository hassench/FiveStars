using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Zone
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ZoneId { get; set; }
        [Index(IsUnique = true)]
        [StringLength(200)]
        [Required]
        [Display(Name = "Nom de la zone")]
        public string NomZone { get; set; }
        [Required]
        public string Appartenance { get; set; }
        [Required]
        [Display(Name = "Nom de la Responsable Hiérarchique")]
        public string ResponsableHierarZone { get; set; }
        
        [Required]
        [Display(Name = "Type de la zone")]
        public string TypeZone { get; set; }

        
        [DataType(DataType.Date)]
        [Display(Name = "Date de création de la zone ")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateIntroBaseZone { get; set; }

        public virtual Pilote PiloteZoneObli { get; set; }
        public virtual Pilote PiloteZoneOpti { get; set; }

        public virtual List<Audit> audits { get; set; }
        public virtual List<Comment> comments { get; set; }
    }
}
