using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Pilote
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PiloteId { get; set; }
        [Required]
        public String NomPilote { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateIntroBasePilote { get; set; }
    }
}
