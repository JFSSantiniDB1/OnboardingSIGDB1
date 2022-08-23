using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Data.Mappings
{
    public class FuncionarioXCargoMapping: IEntityTypeConfiguration<FuncionarioXCargo>
    {
        public void Configure(EntityTypeBuilder<FuncionarioXCargo> builder)
        {
            builder.ToTable("FUNCIONARIOXCARGO");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.IdFuncionario);
            builder.Property(p => p.IdCargo);
            
            builder.HasOne(p => p.Funcionario)
                .WithMany(p => p.Cargos)
                .HasForeignKey(fk => fk.IdFuncionario)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Cargo)
                .WithMany()
                .HasForeignKey(fk => fk.IdCargo);
        }
    }
}