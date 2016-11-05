namespace DAL.Migrations
{
    using Entities.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DAL.Repo;
    using System.Diagnostics;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        public Theme GetSingle(string info, DAL.Models.ApplicationDbContext context)
        {

            var query = (from c in context.Themes select c).FirstOrDefault<Theme>(x => x.info == info);
            return query;
        }

        public Theme GetThemeByName(string Themename)
        {
            Theme Themetemp = null;
            using (ThemeRepository Themerepo = new ThemeRepository())
            {
                Themetemp = Themerepo.GetSingleByName(Themename);
                //  Themerepo.context.Entry(Themetemp).State = EntityState.Detached;
            }

            return Themetemp;
        }

        protected override void Seed(DAL.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Auditeurs.AddOrUpdate(
         new Auditeur { AuditeurId = 1, Nom = "foulen1", Prenom = "fouleni", Affectation = "UF", Telephone = "24671912", DateIntroBaseAuditeur = DateTime.Now },
         new Auditeur { AuditeurId = 2, Nom = "foulen2", Prenom = "fouleni", Affectation = "ATR", Telephone = "24671912", DateIntroBaseAuditeur = DateTime.Now },
         new Auditeur { AuditeurId = 3, Nom = "foulen3", Prenom = "fouleni", Affectation = "STB", Telephone = "24671912", DateIntroBaseAuditeur = DateTime.Now },
         new Auditeur { AuditeurId = 4, Nom = "foulen4", Prenom = "fouleni", Affectation = "STB2", Telephone = "24671912", DateIntroBaseAuditeur = DateTime.Now },
         new Auditeur { AuditeurId = 5, Nom = "foulen5", Prenom = "fouleni", Affectation = "ENEL", Telephone = "24671912", DateIntroBaseAuditeur = DateTime.Now },
         new Auditeur { AuditeurId = 6, Nom = "foulen6", Prenom = "fouleni", Affectation = "Precess", Telephone = "24671912", DateIntroBaseAuditeur = DateTime.Now },
         new Auditeur { AuditeurId = 7, Nom = "foulen7", Prenom = "fouleni", Affectation = "Maintenance", Telephone = "24671912", DateIntroBaseAuditeur = DateTime.Now },
         new Auditeur { AuditeurId = 8, Nom = "foulen8", Prenom = "fouleni", Affectation = "Magazin", Telephone = "24671912", DateIntroBaseAuditeur = DateTime.Now }

            );

            //
            //
            //remember to enable  notifications to pilote
            Random rnd = new Random();

            context.Zones.AddOrUpdate(
        new Zone { NomZone = "Zone1" + rnd.Next(1, 100), Appartenance = "UF", ResponsableHierarZone = "ResphierarZ1", PiloteZoneObli = new Pilote { NomPilote = "PiloteOb1" + rnd.Next(1, 100), DateIntroBasePilote = DateTime.Now }, TypeZone = "Typezone1", DateIntroBaseZone = DateTime.Now },
        new Zone { NomZone = "Zone2" + rnd.Next(1, 100), Appartenance = "ATR", ResponsableHierarZone = "ResphierarZ2", PiloteZoneObli = new Pilote { NomPilote = "PiloteOb2" + rnd.Next(1, 100), DateIntroBasePilote = DateTime.Now }, TypeZone = "Typezone2", DateIntroBaseZone = DateTime.Now },
        new Zone { NomZone = "Zone3" + rnd.Next(1, 100), Appartenance = "STB1", ResponsableHierarZone = "ResphierarZ3", PiloteZoneObli = new Pilote { NomPilote = "PiloteOb3" + rnd.Next(1, 100), DateIntroBasePilote = DateTime.Now }, TypeZone = "Typezone3", DateIntroBaseZone = DateTime.Now },
        new Zone { NomZone = "Zone4" + rnd.Next(1, 100), Appartenance = "STB2", ResponsableHierarZone = "ResphierarZ4", PiloteZoneObli = new Pilote { NomPilote = "PiloteOb4" + rnd.Next(1, 100), DateIntroBasePilote = DateTime.Now }, TypeZone = "Typezone4", DateIntroBaseZone = DateTime.Now },
        new Zone { NomZone = "Zone5" + rnd.Next(1, 100), Appartenance = "ENEL", ResponsableHierarZone = "ResphierarZ5", PiloteZoneObli = new Pilote { NomPilote = "PiloteOb5" + rnd.Next(1, 100), DateIntroBasePilote = DateTime.Now }, TypeZone = "Typezone5", DateIntroBaseZone = DateTime.Now },
        new Zone { NomZone = "Zone6" + rnd.Next(1, 100), Appartenance = "Process", ResponsableHierarZone = "ResphierarZ6", PiloteZoneObli = new Pilote { NomPilote = "PiloteOb6" + rnd.Next(1, 100), DateIntroBasePilote = DateTime.Now }, TypeZone = "Typezone6", DateIntroBaseZone = DateTime.Now },
        new Zone { NomZone = "Zone7" + rnd.Next(1, 100), Appartenance = "Maintenance", ResponsableHierarZone = "ResphierarZ7", PiloteZoneObli = new Pilote { NomPilote = "PiloteOb7" + rnd.Next(1, 100), DateIntroBasePilote = DateTime.Now }, TypeZone = "Typezone7", DateIntroBaseZone = DateTime.Now },
        new Zone { NomZone = "Zone8" + rnd.Next(1, 100), Appartenance = "Magazin", ResponsableHierarZone = "ResphierarZ8", PiloteZoneObli = new Pilote { NomPilote = "PiloteOb8" + rnd.Next(1, 100), DateIntroBasePilote = DateTime.Now }, TypeZone = "Typezone8", DateIntroBaseZone = DateTime.Now }

        );

            //for (int i = 1; i < 9; i++)
            //{
            //    context.Zones.AddOrUpdate(
            //        new Zone { NomZone = "Zone" + i.ToString(), Appartenance = "A" + i.ToString(), ResponsableHierarZone = "ResphierarZ" + i.ToString(), PiloteZoneObli = "PiloteOb" + i.ToString(), PiloteZoneOpti = "PiloteOp" + i.ToString(), TypeZone = "Typezone" + i.ToString(), DateIntroBaseZone = DateTime.Now }
            //        );
            //}

            List<Point> points1 = new List<Point>();
            points1.Add(new Point { Coef = 1, NumPoint = 1, NomPoint = "Pas d’objets / outils cassés, dégradés sur le poste" });
            points1.Add(new Point { Coef = 4, NumPoint = 2, NomPoint = "Pas d’objets hors contexte ou personnel dans la zone" });
            points1.Add(new Point { Coef = 1, NumPoint = 3, NomPoint = "Pas d’objets inutiles " });
            points1.Add(new Point { Coef = 2, NumPoint = 4, NomPoint = "Pas de documents non mis à jour / périmés" });
            points1.Add(new Point { Coef = 1, NumPoint = 5, NomPoint = "Pas de documents inutiles affichés" });
            points1.Add(new Point { Coef = 5, NumPoint = 6, NomPoint = "Les poubelles et boites à rebuts ne débordent pas" });
            points1.Add(new Point { Coef = 3, NumPoint = 7, NomPoint = "Les moyens de lutte incendie ne sont pas masqués et sont accessibles" });
            points1.Add(new Point { Coef = 3, NumPoint = 8, NomPoint = "Aucun carton/matière/bobine/outillage/matériel/ par terre" });

            List<Point> points2 = new List<Point>();
            points2.Add(new Point { Coef = 1, NumPoint = 9, NomPoint = "Tout Les objets/ Outils sur le bureau/poste sont identifiés et rangés" });
            points2.Add(new Point { Coef = 1, NumPoint = 10, NomPoint = "Il y a un emplacement dédié pour chaque chose" });
            points2.Add(new Point { Coef = 1, NumPoint = 11, NomPoint = "Les produits/cartes sont correctement rangé/posé" });
            points2.Add(new Point { Coef = 1, NumPoint = 12, NomPoint = "Chaque mobilier (armoire, tiroir, étagère) fait l'objet d'une identification sur son contenu" });
            points2.Add(new Point { Coef = 1, NumPoint = 13, NomPoint = "Les règles de rangement bureau de fin de journée /départ sont définis, affichées et connues" });
            points2.Add(new Point { Coef = 1, NumPoint = 14, NomPoint = "Rien sur les armoires (pas de stockage par-dessus les armoires)" });
            points2.Add(new Point { Coef = 1, NumPoint = 15, NomPoint = "Les poubelles ont un emplacement dédié et identifié" });
            points2.Add(new Point { Coef = 1, NumPoint = 16, NomPoint = "Les transpalettes ,chariots et roulis ont un emplacement dédié et identifié" });
            points2.Add(new Point { Coef = 1, NumPoint = 17, NomPoint = "Le marquage au sol est en bon état" });
            points2.Add(new Point { Coef = 3, NumPoint = 18, NomPoint = "Les câbles / fils électriques sont rangés et sécurisés" });
            points2.Add(new Point { Coef = 2, NumPoint = 19, NomPoint = "Les extincteurs moyens de lutte incendie et issus de secours sont  repérés" });
            points2.Add(new Point { Coef = 1, NumPoint = 20, NomPoint = "Les EPI sont présents et utilisés/bureau (port de blouses +badges)" });
            points2.Add(new Point { Coef = 5, NumPoint = 21, NomPoint = "Le mobilier /équipement /chariot convoyeurs sont alignés et ne débordent pas sur les allées" });

            List<Point> points3 = new List<Point>();
            points3.Add(new Point { Coef = 2, NumPoint = 22, NomPoint = "Le bureau ou le poste est-il propre (pas de poussière, tâches, copeaux, etc., hors activité / Les outils et objets sur le poste / bureau sont propres" });
            points3.Add(new Point { Coef = 2, NumPoint = 23, NomPoint = "La culture d'économie d'énergie existe et appliquée." });
            points3.Add(new Point { Coef = 2, NumPoint = 24, NomPoint = "Les documents sont présentables (ni déchirés, ni salis) " });
            points3.Add(new Point { Coef = 2, NumPoint = 25, NomPoint = "Les mobiliers sont propres" });
            points3.Add(new Point { Coef = 2, NumPoint = 26, NomPoint = "Les sols sont propres" });
            points3.Add(new Point { Coef = 2, NumPoint = 27, NomPoint = "Les murs, les piliers et leurs pourtours sont propres" });
            points3.Add(new Point { Coef = 2, NumPoint = 28, NomPoint = "Toutes les zones sont accessibles pour le nettoyage" });
            points3.Add(new Point { Coef = 2, NumPoint = 29, NomPoint = "Il existe un planning et une gamme de nettoyage pour chaque poste" });
            points3.Add(new Point { Coef = 2, NumPoint = 30, NomPoint = "Il y a un kit de nettoyage adapté à la zone, identifié, propre et complet" });

            List<Point> points4 = new List<Point>();
            points4.Add(new Point { Coef = 2, NumPoint = 31, NomPoint = "Les Standards sont affichés dans les bureaux et correspondent au référentiel Sagemcom (voir référentiel)" });
            points4.Add(new Point { Coef = 2, NumPoint = 32, NomPoint = "Il existe au moins 2 standards de vie (règle de vie, archivage, alertes, Appr fournitures, etc...) au bureau" });
            points4.Add(new Point { Coef = 2, NumPoint = 33, NomPoint = "Les documents et indicateurs sont à jour (y compris radar sécurité) et le responsable de mis à jour est identifié + Il existe au moins un indicateur de perf dans le bureau " });
            points4.Add(new Point { Coef = 2, NumPoint = 34, NomPoint = "Les règles d'escalade d'informations existent et sont connus par toutes les personnes affectées à la zone auditée" });
            points4.Add(new Point { Coef = 2, NumPoint = 35, NomPoint = "Un organigramme est affiché et les intérims en cas d'absence sont connus" });
            points4.Add(new Point { Coef = 2, NumPoint = 36, NomPoint = "Le marquage au sol et les identification correspond aux standards  Sagemcom" });
            points4.Add(new Point { Coef = 2, NumPoint = 37, NomPoint = "Les consignes de sécurité existent et sont affichés , les issues de sécurité sont connus par tous" });
            points4.Add(new Point { Coef = 2, NumPoint = 38, NomPoint = "Le système de suggestions est mis en place " });
            points4.Add(new Point { Coef = 2, NumPoint = 39, NomPoint = "Un indicateur de niveau five star existe et est à jour" });

            List<Point> points5 = new List<Point>();
            points5.Add(new Point { Coef = 3, NumPoint = 40, NomPoint = "Les objectifs/axes de progrès sont affichés/connus par tous" });
            points5.Add(new Point { Coef = 3, NumPoint = 41, NomPoint = "Le système de suggestion est fonctionnel, le Nbre de suggestions émise correspond à l'objectif  moyenne 1 suggestion/pers/an" });
            points5.Add(new Point { Coef = 3, NumPoint = 42, NomPoint = "Les Non-Conformités du dernier audit sont réglées ou sont en cours (Plan d'action five star )" });
            points5.Add(new Point { Coef = 3, NumPoint = 43, NomPoint = "Tout le personnel de la zone auditée est formé aux standards/Affichage et procédures en vigueur dans la zone" });
            points5.Add(new Point { Coef = 3, NumPoint = 44, NomPoint = "les 7 types de gaspillage sont connus par tous les employés de la zone" });
            points5.Add(new Point { Coef = 2, NumPoint = 45, NomPoint = "Le fichier best Practice existe et un plan d'action de suivi existe et est à jour" });
            points5.Add(new Point { Coef = 4, NumPoint = 46, NomPoint = "Tout le personnel de la zone est formé aux Five Star et aux règles et consignes de sécurité" });
            points5.Add(new Point { Coef = 3, NumPoint = 47, NomPoint = "Les enquêtes des accidents de travail sont menés et les plans d’action sécurité sont affichés" });


            //points1.Add(new Point { });
            context.Themes.AddOrUpdate(
                new Theme { ThemeId = 1, info = "Theme 1", Passinggrade = 95, points = points1 },
                new Theme { ThemeId = 2, info = "Theme 2", Passinggrade = 95, points = points2 },
                new Theme { ThemeId = 3, info = "Theme 3", Passinggrade = 92, points = points3 },
                new Theme { ThemeId = 4, info = "Theme 4", Passinggrade = 92, points = points4 },
                new Theme { ThemeId = 5, info = "Theme 5", Passinggrade = 90, points = points5 }
                );

        }
    }
}
