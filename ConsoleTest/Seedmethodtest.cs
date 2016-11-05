using Entities.Models;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using DAL.Repo;
using System.Diagnostics;


namespace ConsoleTest
{
    public class Seedmethodtest
    {
        public Theme GetSingle(string info, ApplicationDbContext context)
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
                Themerepo.context.Entry(Themetemp).State = EntityState.Detached;
            }

            return Themetemp;
        }

        public  void Seed()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            

            List<Theme> _themes = context.Themes.ToList();
            Theme theme1 = new Theme();
            theme1 = GetThemeByName("Theme 1");
            //Console.WriteLine(theme1.info+theme1.Passinggrade+theme1.ThemeId);
            
            if (theme1 != null)
            {
                Console.WriteLine(theme1.info +" "+ theme1.Passinggrade +" "+ theme1.ThemeId);

            }
            else Console.WriteLine("Theme is null");
            Theme theme2 = GetThemeByName("Theme 2");
            
            context.Points.AddOrUpdate(
                new Point { Coef = 1, NumPoint = 1, NomPoint = "Pas d’objets / outils cassés, dégradés sur le poste" , theme = theme1},
                new Point { Coef = 4, NumPoint = 2, NomPoint = "Pas d’objets hors contexte ou personnel dans la zone", theme = theme1 },
                new Point { Coef = 1, NumPoint = 3, NomPoint = "Pas d’objets inutiles ", theme = theme1 },
                new Point { Coef = 2, NumPoint = 4, NomPoint = "Pas de documents non mis à jour / périmés", theme = theme1 },
                new Point { Coef = 1, NumPoint = 5, NomPoint = "Pas de documents inutiles affichés", theme = theme1 },
                new Point { Coef = 5, NumPoint = 6, NomPoint = "Les poubelles et boites à rebuts ne débordent pas", theme = theme1 },
                new Point { Coef = 3, NumPoint = 7, NomPoint = "Les moyens de lutte incendie ne sont pas masqués et sont accessibles", theme = theme1 },
                new Point { Coef = 3, NumPoint = 8, NomPoint = "Aucun carton/matière/bobine/outillage/matériel/ par terre", theme = theme1 },

                new Point { Coef = 1, NumPoint = 9, NomPoint = "Tout Les objets/ Outils sur le bureau/poste sont identifiés et rangés", theme = theme2 },
                new Point { Coef = 1, NumPoint = 10, NomPoint = "Il y a un emplacement dédié pour chaque chose", theme = theme2 },
                new Point { Coef = 1, NumPoint = 11, NomPoint = "Les produits/cartes sont correctement rangé/posé", theme = theme2 },
                new Point { Coef = 1, NumPoint = 12, NomPoint = "Chaque mobilier (armoire, tiroir, étagère) fait l'objet d'une identification sur son contenu", theme = theme2 },
                new Point { Coef = 1, NumPoint = 13, NomPoint = "Les règles de rangement bureau de fin de journée /départ sont définis, affichées et connues", theme = theme2 },
                new Point { Coef = 1, NumPoint = 14, NomPoint = "Rien sur les armoires (pas de stockage par-dessus les armoires)", theme = theme2 },
                new Point { Coef = 1, NumPoint = 15, NomPoint = "Les poubelles ont un emplacement dédié et identifié", theme = theme2 },
                new Point { Coef = 1, NumPoint = 16, NomPoint = "Les transpalettes ,chariots et roulis ont un emplacement dédié et identifié", theme = theme2 },
                new Point { Coef = 1, NumPoint = 17, NomPoint = "Le marquage au sol est en bon état", theme = theme2 },
                new Point { Coef = 1, NumPoint = 18, NomPoint = "Les câbles / fils électriques sont rangés et sécurisés", theme = theme2 },
                new Point { Coef = 1, NumPoint = 19, NomPoint = "Les extincteurs moyens de lutte incendie et issus de secours sont  repérés", theme = theme2 }

            );
            context.SaveChanges();
        }
    }
}
