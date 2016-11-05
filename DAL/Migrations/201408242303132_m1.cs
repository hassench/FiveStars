namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auditeurs",
                c => new
                    {
                        AuditeurId = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false),
                        Prenom = c.String(nullable: false),
                        Affectation = c.String(nullable: false),
                        Telephone = c.String(nullable: false),
                        DateIntroBaseAuditeur = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AuditeurId);
            
            CreateTable(
                "dbo.Audits",
                c => new
                    {
                        AuditId = c.Int(nullable: false, identity: true),
                        FiveStarsLevel = c.Int(nullable: false),
                        Note = c.Int(nullable: false),
                        TypeAudit = c.String(),
                        isInProgress = c.Boolean(nullable: false),
                        isCompleted = c.Boolean(nullable: false),
                        AuditDay = c.DateTime(),
                        DateOfCompletion = c.DateTime(),
                        auditeur_AuditeurId = c.Int(),
                        semaine_SemaineId = c.Int(),
                        zone_ZoneId = c.Int(),
                    })
                .PrimaryKey(t => t.AuditId)
                .ForeignKey("dbo.Auditeurs", t => t.auditeur_AuditeurId)
                .ForeignKey("dbo.Semaines", t => t.semaine_SemaineId)
                .ForeignKey("dbo.Zones", t => t.zone_ZoneId)
                .Index(t => t.auditeur_AuditeurId)
                .Index(t => t.semaine_SemaineId)
                .Index(t => t.zone_ZoneId);
            
            CreateTable(
                "dbo.Resultats",
                c => new
                    {
                        ResultatId = c.Int(nullable: false, identity: true),
                        Note = c.Int(),
                        req_comment = c.String(),
                        audit_AuditId = c.Int(),
                        point_PointID = c.Int(),
                        theme_ThemeId = c.Int(),
                    })
                .PrimaryKey(t => t.ResultatId)
                .ForeignKey("dbo.Audits", t => t.audit_AuditId)
                .ForeignKey("dbo.Points", t => t.point_PointID)
                .ForeignKey("dbo.Themes", t => t.theme_ThemeId)
                .Index(t => t.audit_AuditId)
                .Index(t => t.point_PointID)
                .Index(t => t.theme_ThemeId);
            
            CreateTable(
                "dbo.Points",
                c => new
                    {
                        PointID = c.Int(nullable: false, identity: true),
                        NumPoint = c.Int(nullable: false),
                        NomPoint = c.String(nullable: false),
                        Coef = c.Int(nullable: false),
                        theme_ThemeId = c.Int(),
                    })
                .PrimaryKey(t => t.PointID)
                .ForeignKey("dbo.Themes", t => t.theme_ThemeId)
                .Index(t => t.theme_ThemeId);
            
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        ThemeId = c.Int(nullable: false, identity: true),
                        Passinggrade = c.Int(nullable: false),
                        info = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.ThemeId)
                .Index(t => t.info, unique: true);
            
            CreateTable(
                "dbo.Semaines",
                c => new
                    {
                        SemaineId = c.Int(nullable: false, identity: true),
                        Datedebut = c.DateTime(nullable: false),
                        Datefin = c.DateTime(nullable: false),
                        isCurrent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SemaineId);
            
            CreateTable(
                "dbo.Zones",
                c => new
                    {
                        ZoneId = c.Int(nullable: false, identity: true),
                        NomZone = c.String(nullable: false, maxLength: 200),
                        Appartenance = c.String(nullable: false),
                        ResponsableHierarZone = c.String(nullable: false),
                        TypeZone = c.String(nullable: false),
                        DateIntroBaseZone = c.DateTime(nullable: false),
                        PiloteZoneObli_PiloteId = c.Int(),
                        PiloteZoneOpti_PiloteId = c.Int(),
                    })
                .PrimaryKey(t => t.ZoneId)
                .ForeignKey("dbo.Pilotes", t => t.PiloteZoneObli_PiloteId)
                .ForeignKey("dbo.Pilotes", t => t.PiloteZoneOpti_PiloteId)
                .Index(t => t.NomZone, unique: true)
                .Index(t => t.PiloteZoneObli_PiloteId)
                .Index(t => t.PiloteZoneOpti_PiloteId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Body = c.String(),
                        user_Id = c.String(maxLength: 128),
                        zone_ZoneId = c.Int(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.AspNetUsers", t => t.user_Id)
                .ForeignKey("dbo.Zones", t => t.zone_ZoneId)
                .Index(t => t.user_Id)
                .Index(t => t.zone_ZoneId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        auditeur_AuditeurId = c.Int(),
                        pilote_PiloteId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Auditeurs", t => t.auditeur_AuditeurId)
                .ForeignKey("dbo.Pilotes", t => t.pilote_PiloteId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.auditeur_AuditeurId)
                .Index(t => t.pilote_PiloteId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Pilotes",
                c => new
                    {
                        PiloteId = c.Int(nullable: false, identity: true),
                        NomPilote = c.String(nullable: false),
                        DateIntroBasePilote = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PiloteId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Delegations",
                c => new
                    {
                        DelegationId = c.Int(nullable: false, identity: true),
                        accepted = c.Boolean(nullable: false),
                        treated = c.Boolean(nullable: false),
                        Concernedaudit_AuditId = c.Int(),
                        Delegate_AuditeurId = c.Int(),
                        Delegator_AuditeurId = c.Int(),
                        semaine_SemaineId = c.Int(),
                    })
                .PrimaryKey(t => t.DelegationId)
                .ForeignKey("dbo.Audits", t => t.Concernedaudit_AuditId)
                .ForeignKey("dbo.Auditeurs", t => t.Delegate_AuditeurId)
                .ForeignKey("dbo.Auditeurs", t => t.Delegator_AuditeurId)
                .ForeignKey("dbo.Semaines", t => t.semaine_SemaineId)
                .Index(t => t.Concernedaudit_AuditId)
                .Index(t => t.Delegate_AuditeurId)
                .Index(t => t.Delegator_AuditeurId)
                .Index(t => t.semaine_SemaineId);
            
            CreateTable(
                "dbo.PDCAs",
                c => new
                    {
                        PDCAId = c.Int(nullable: false, identity: true),
                        UF = c.String(),
                        NLigne = c.String(),
                        Machine = c.String(),
                        Theme = c.String(),
                        Date = c.DateTime(),
                        Anomalie = c.String(),
                        Criticite = c.String(),
                        Action = c.String(),
                        Pilote = c.String(),
                        Delai = c.DateTime(),
                        DateRealise = c.DateTime(),
                        VerifEfficacite = c.String(),
                        Commentaire = c.String(),
                        P = c.Boolean(nullable: false),
                        D = c.Boolean(nullable: false),
                        C = c.Boolean(nullable: false),
                        resultat_ResultatId = c.Int(),
                    })
                .PrimaryKey(t => t.PDCAId)
                .ForeignKey("dbo.Resultats", t => t.resultat_ResultatId)
                .Index(t => t.resultat_ResultatId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PDCAs", "resultat_ResultatId", "dbo.Resultats");
            DropForeignKey("dbo.Delegations", "semaine_SemaineId", "dbo.Semaines");
            DropForeignKey("dbo.Delegations", "Delegator_AuditeurId", "dbo.Auditeurs");
            DropForeignKey("dbo.Delegations", "Delegate_AuditeurId", "dbo.Auditeurs");
            DropForeignKey("dbo.Delegations", "Concernedaudit_AuditId", "dbo.Audits");
            DropForeignKey("dbo.Zones", "PiloteZoneOpti_PiloteId", "dbo.Pilotes");
            DropForeignKey("dbo.Zones", "PiloteZoneObli_PiloteId", "dbo.Pilotes");
            DropForeignKey("dbo.Comments", "zone_ZoneId", "dbo.Zones");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "pilote_PiloteId", "dbo.Pilotes");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "user_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "auditeur_AuditeurId", "dbo.Auditeurs");
            DropForeignKey("dbo.Audits", "zone_ZoneId", "dbo.Zones");
            DropForeignKey("dbo.Audits", "semaine_SemaineId", "dbo.Semaines");
            DropForeignKey("dbo.Resultats", "theme_ThemeId", "dbo.Themes");
            DropForeignKey("dbo.Resultats", "point_PointID", "dbo.Points");
            DropForeignKey("dbo.Points", "theme_ThemeId", "dbo.Themes");
            DropForeignKey("dbo.Resultats", "audit_AuditId", "dbo.Audits");
            DropForeignKey("dbo.Audits", "auditeur_AuditeurId", "dbo.Auditeurs");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PDCAs", new[] { "resultat_ResultatId" });
            DropIndex("dbo.Delegations", new[] { "semaine_SemaineId" });
            DropIndex("dbo.Delegations", new[] { "Delegator_AuditeurId" });
            DropIndex("dbo.Delegations", new[] { "Delegate_AuditeurId" });
            DropIndex("dbo.Delegations", new[] { "Concernedaudit_AuditId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "pilote_PiloteId" });
            DropIndex("dbo.AspNetUsers", new[] { "auditeur_AuditeurId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Comments", new[] { "zone_ZoneId" });
            DropIndex("dbo.Comments", new[] { "user_Id" });
            DropIndex("dbo.Zones", new[] { "PiloteZoneOpti_PiloteId" });
            DropIndex("dbo.Zones", new[] { "PiloteZoneObli_PiloteId" });
            DropIndex("dbo.Zones", new[] { "NomZone" });
            DropIndex("dbo.Themes", new[] { "info" });
            DropIndex("dbo.Points", new[] { "theme_ThemeId" });
            DropIndex("dbo.Resultats", new[] { "theme_ThemeId" });
            DropIndex("dbo.Resultats", new[] { "point_PointID" });
            DropIndex("dbo.Resultats", new[] { "audit_AuditId" });
            DropIndex("dbo.Audits", new[] { "zone_ZoneId" });
            DropIndex("dbo.Audits", new[] { "semaine_SemaineId" });
            DropIndex("dbo.Audits", new[] { "auditeur_AuditeurId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PDCAs");
            DropTable("dbo.Delegations");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Pilotes");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Comments");
            DropTable("dbo.Zones");
            DropTable("dbo.Semaines");
            DropTable("dbo.Themes");
            DropTable("dbo.Points");
            DropTable("dbo.Resultats");
            DropTable("dbo.Audits");
            DropTable("dbo.Auditeurs");
        }
    }
}
