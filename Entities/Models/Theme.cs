using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Theme
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ThemeId { get; set; }

        public int Passinggrade { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(200)]
        public string info { get; set; }

        public virtual List<Point> points { get; set; }
        public Theme()
        {
            points=new List<Point>();
        }
    }
}
