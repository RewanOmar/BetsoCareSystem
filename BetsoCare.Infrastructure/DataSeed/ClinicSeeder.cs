using BetsoCare.Core.Entities;
using BetsoCare.Infrastructure.Data;
using System.Text.Json;

namespace BetsoCare.Infrastructure.DataSeed
{
    public class ClinicSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (context.Clinics.Any())
                return;

            var path = Path.Combine(
     Directory.GetCurrentDirectory(),
     "..",
     "BetsoCare.Infrastructure",
     "DataSeed",
     "clinics.json"
 );

            

            var json = await File.ReadAllTextAsync(path);

            var clinics = JsonSerializer.Deserialize<List<Clinic>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (clinics != null)
            {
                context.Clinics.AddRange(clinics);
                await context.SaveChangesAsync();
            }
        }
    }
}