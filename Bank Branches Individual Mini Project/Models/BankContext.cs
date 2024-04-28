
using Microsoft.EntityFrameworkCore;

namespace Bank_Branches_Individual_Mini_Project.Models
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options) 
        {
        }
        public DbSet<BankBranch> BankBranches { get; set; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlite("Data Source=Bank.db");
    
}
}
