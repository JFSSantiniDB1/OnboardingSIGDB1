using System;

namespace OnboardingSIGDB1.Domain.Dto.FuncionarioXCargos
{
    public class FuncionarioXCargoInputDto
    {
        public int IdFuncionario { get; set; }
        public int IdCargo { get; set; }
        public DateTime DataVinculo { get; set; }
    }
}