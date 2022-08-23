using System;
using OnboardingSIGDB1.Domain.Base;

namespace OnboardingSIGDB1.Domain.Entities
{
    public class FuncionarioXCargo : BaseEntityValidation
    {
        public int Id { get; set; }
        public Funcionario Funcionario { get; set; }
        public int IdFuncionario { get; set; }
        public Cargo Cargo { get; set; }
        public int IdCargo { get; set; }
        public DateTime DataVinculo { get; set; }
    }
}