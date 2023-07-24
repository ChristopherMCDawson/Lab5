using Microsoft.EntityFrameworkCore;


namespace Lab5.Data
{
    public class PredictionDataContext : DbContext
    {
        public DbSet<Prediction> Predictions { get; set; }

        public PredictionDataContext(DbContextOptions<PredictionDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prediction>().ToTable(nameof(Prediction));

            modelBuilder.Entity<Prediction>()
                .HasKey(x => x.PredictionId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
