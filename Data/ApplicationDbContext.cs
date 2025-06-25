using Microsoft.EntityFrameworkCore;
using WebApiDemo.Models;

namespace WebApiDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Contaminant> Contaminants { get; set; }
        public DbSet<SoilType> SoilTypes { get; set; }
        public DbSet<Pathway> Pathways { get; set; }
        public DbSet<GuidelineValue> GuidelineValues { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ...existing code...
            modelBuilder.Entity<GuidelineValue>()
                .HasIndex(g => new { g.ContaminantId, g.SoilTypeId, g.PathwayId })
                .IsUnique();
        }
    }
}