using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Data.Mappings
{
    public class EmpresaMapping : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("EMPRESA");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Nome).HasMaxLength(150).IsRequired();
            builder.Property(p => p.Cnpj).HasMaxLength(14).IsRequired();
            builder.Property(p => p.DataFundacao);
            
            builder.HasMany(p => p.Funcionarios)
                .WithOne(p => p.Empresa)
                .IsRequired(false)
                .HasForeignKey(fk => fk.IdEmpresa)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}