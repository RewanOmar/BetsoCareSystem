using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetsoCare.Core.Entities;

namespace BetsoCare.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Pet> Pets { get; set; }

       

        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<BiteReport> BiteReports { get; set; }
        public DbSet<DangerousAnimalReport> DangerousAnimalReports { get; set; }
        public DbSet<ComplaintReport> ComplaintReports { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<Vaccine> Vaccines => Set<Vaccine>();
        public DbSet<Location> Locations => Set<Location>();

    }

  
}
