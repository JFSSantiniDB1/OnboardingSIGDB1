using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Mappings;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Data
{
    public class SIGDB1DbContext : DbContext
    {
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        
        public DbSet<FuncionarioXCargo> FuncionarioXCargo { get; set; }

        public SIGDB1DbContext(DbContextOptions<SIGDB1DbContext> options) : base (options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmpresaMapping());
            modelBuilder.ApplyConfiguration(new FuncionarioMapping());
            modelBuilder.ApplyConfiguration(new CargoMapping());
            modelBuilder.ApplyConfiguration(new FuncionarioXCargoMapping());
        
            base.OnModelCreating(modelBuilder);
        }
    }
}