using Microsoft.EntityFrameworkCore;
using RVA_Projekat.Model;

namespace RVA_Projekat.Infrastructure
{
    public class HonorarDbContext:DbContext
    {
        public DbSet<BrutoHonorar> BrutoHonorars { get; set; }
        public DbSet<NetoHonorar> NetoHonorars { get; set; }
        public DbSet<Zaposleni> Zaposlenis { get; set; }
        public DbSet<User> Users { get; set; }

        public HonorarDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Kazemo mu da pronadje sve konfiguracije u Assembliju i da ih primeni nad bazom
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HonorarDbContext).Assembly);
        }
    }
}
