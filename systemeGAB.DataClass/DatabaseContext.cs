namespace systemeGAB.DataClass
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Client> client { get; set; } = null!;
        public DbSet<CompteBancaire> compteBancaire { get; set; } = null!;
        public DbSet<CarteBancaire> carteBancaire { get; set; } = null!;
        public DbSet<Transaction> transaction { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasKey(c => new { c.idClient });
            modelBuilder.Entity<CompteBancaire>()
                .HasKey(cb => new { cb.idCompte });
            modelBuilder.Entity<CarteBancaire>()
                .HasKey(cb => new { cb.idCarte }); ;
            modelBuilder.Entity<Transaction>()
                .HasKey(t => new { t.idTransaction }); ;
        }
    }
}
