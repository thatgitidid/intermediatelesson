

using IntermediateLessons.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IntermediateLessons.Data
{
    // : is used for inheritance.  DataContextEF is inheriting from DbContext.
    public class DataContextEF : DbContext
    {
        private IConfiguration _config;
        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }

        // Telling it which models to connect to.  Entity Framework will look for
        // table to match model.
        public DbSet<Computer>? Computer { get; set; }

        // Override OnConfiguring method, which is called when DbContext is created.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"), 
                    options => options.EnableRetryOnFailure());
            }
        }

        // Adds entity for computer model that hooks to DB set.
        // Automagic
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            modelBuilder.Entity<Computer>()
                .HasKey(c => c.ComputerId);
                // Can call .HasNoKey(); to execute without a key.
                // .HasKey(c => c.Motherboard); // Sets Motherboard as the key.

                // .ToTable("Computer", "TutorialAppSchema");
                // Would need this is did not set default schema.
        }

    }
}
