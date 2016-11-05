using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Point
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PointID { get; set; }
        [Required]
        public int NumPoint { get; set; }

        [Required]
        public string NomPoint { get; set; }

        [Required]
        public int Coef { get; set; }
        
        public bool isDeleted { get; set; }

        public virtual Theme theme { get; set; }
    }
}
