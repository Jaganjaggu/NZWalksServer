using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext: DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> images { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Difficulties with specific GUIDs
            modelBuilder.Entity<Difficulty>().HasData(
                new Difficulty
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Easy"
                },
                new Difficulty
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Medium"
                },
                new Difficulty
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Hard"
                }
            );

            // Seed data for Regions (as before)
            modelBuilder.Entity<Region>().HasData(
                new Region
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Code = "AKL",
                    Name = "Auckland",
                    RegionImageUrl = "https://example.com/images/auckland.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Code = "WLG",
                    Name = "Wellington",
                    RegionImageUrl = "https://example.com/images/wellington.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    Code = "CHC",
                    Name = "Christchurch",
                    RegionImageUrl = null
                }
            );
        }
    }
}
