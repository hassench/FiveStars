using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Display(Name = "Objet")]
        public string Subject { get; set; }
        [Display(Name = "Corps")]
        public string Body { get; set; }

        

        public virtual Zone zone { get; set; }
        [Display(Name = "Utilisateur")]
        public virtual ApplicationUser user { get; set; }
        
    }
}