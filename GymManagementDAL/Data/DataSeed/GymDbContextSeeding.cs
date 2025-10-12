using GymManagementDAL.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.DataSeed
{
    internal static class GymDbContextSeeding
    {
        public static bool SeedData(GymDbContext dbContext)
        {
            try
            {
                var HasPlans = dbContext.Plans.Any();
                var HasCategories = dbContext.Categories.Any();
                if (HasCategories && HasPlans) return false;
                if (!HasCategories)
                {
                    var Categories = LoadDataFromJsonFile<Entities.Category>("categories.json");
                    if (Categories.Any())
                        dbContext.Categories.AddRange(Categories);

                }
                if (!HasPlans)
                {
                    var Plans = LoadDataFromJsonFile<Entities.Plan>("plans.json");
                    if (Plans.Any())
                        dbContext.Plans.AddRange(Plans);

                }
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding Failed : {ex}");
                return false;
            }
        }

        private static List<T> LoadDataFromJsonFile<T>(string fileName)
        {
            // wwwroot\Files\categories.json
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", fileName);
            if (!File.Exists(FilePath)) return [];
            string Data = File.ReadAllText(FilePath);
            var Options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<T>>(Data, Options) ?? new List<T> ();
        }
    }
}
