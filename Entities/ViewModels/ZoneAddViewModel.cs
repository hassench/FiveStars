using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class ZoneAddViewModel
    {
        [Display(Name = "Nom de la zone")]
        public string NomZone { get; set; }
        [Required]
        public string Appartenance { get; set; }
        [Required]
        [Display(Name = "Nom de la Responsable Hiérarchique")]
        public string ResponsableHierarZone { get; set; }
        [Required]
        [Display(Name = "Nom du Pilote de la Zone")]
        public string PiloteZoneObli { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string EmailPObli { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PasswordPObli { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("PasswordPObli", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPasswordPObli { get; set; }

        [Display(Name = "Nom d'un autre Pilote de la Zone (Optionelle)")]
        public string PiloteZoneOpti { get; set; }
        
        [EmailAddress]
        [Display(Name = "Email (Optionelle)")]
        public string EmailPOpti { get; set; }


        
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password (Optionelle)")]
        public string PasswordPOpti { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password (Optionelle)")]
        [Compare("PasswordPOpti", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPasswordPOpti { get; set; }

        [Required]
        [Display(Name = "Type de la zone")]
        public string TypeZone { get; set; }
    }
}
