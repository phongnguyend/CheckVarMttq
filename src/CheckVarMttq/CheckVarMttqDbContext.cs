using Microsoft.EntityFrameworkCore;

namespace CheckVarMttq
{
    public class CheckVarMttqDbContext : DbContext
    {
        private const string _connectionString = "Server=.;Database=CheckVarMttq;User Id=sa;Password=sqladmin123!@#;Encrypt=False";

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
