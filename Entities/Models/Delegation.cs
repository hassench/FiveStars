using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Delegation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int       DelegationId { get; set; }
        public Audit     Concernedaudit { get; set; }
        public Auditeur  Delegator { get; set; }
        public Auditeur  Delegate { get; set; }
        public bool      accepted { get; set; }
        public bool      treated { get; set; }

        public virtual Semaine semaine { get; set; }
    }
}
