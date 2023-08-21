using ApplicationCore.Domain.Entities.ChemincalAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class DbExtensions
    {
        public static void SeedData(this DatabaseContext context)
        {
            if (!context.Set<ChemicalType>().Any())
            {
                var chemicalType = new List<ChemicalType>() { 
                    new ChemicalType("Pesticide"),
                    new ChemicalType("Tree Nutrition Fungicide"),
                    new ChemicalType("Bactericides and fungicides")
                };

                context.Set<ChemicalType>().AddRange(chemicalType);
                context.SaveChanges();

                var chemicals = new List<Chemical>();

                for (int i = 0; i < 21; i++)
                {
                    var chemical = new Chemical($"Name {i}", chemicalType[0].Id, $"Active Ingredient {i}", $"Pre Harvest Interval In Days {i}")
                    {
                        CreationBy = "Sys",
                        CreationDate = DateTime.UtcNow,
                    };

                    chemical.TypeId = chemicalType[0].Id;
                    chemicals.Add(chemical);

                }

                context.Set<Chemical>().AddRange(chemicals);
                context.SaveChanges();
            }
        }
    }
}
