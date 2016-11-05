using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Collections.Generic;
using Entities.Models;
using DAL.Models;

namespace DAL.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("BDInterfaceFiveStars", throwIfV1Schema: false)
        {
            // the terrible hack
            var ensureDLLIsCopied =
                    System.Data.Entity.SqlServer.SqlProviderServices.Instance; 
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Auditeur>   Auditeurs       { get; set; }
        public DbSet<Zone>       Zones           { get; set; }
        public DbSet<Semaine>    Semaines        { get; set; }
        public DbSet<Audit>      Audits          { get; set; }
        public DbSet<Theme>      Themes          { get; set; }
        public DbSet<Point>      Points          { get; set; }
        public DbSet<Resultat>   Resultas        { get; set; }
        public DbSet<Comment>    Comments        { get; set; }
        public DbSet<Delegation> Delegations     { get; set; }
        public DbSet<Pilote>     Pilotes         { get; set; }
        public DbSet<PDCA>       Pdcas           { get; set; }
    }
}