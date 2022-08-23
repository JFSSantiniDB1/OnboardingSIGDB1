using System;
using OnboardingSIGDB1.Domain.Interfaces;

namespace OnboardingSIGDB1.Domain.Dto.FuncionarioXCargos
{
    public class FuncionarioXCargoDto
    {
        public int Id { get; set; }
        
        public int IdFuncionario { get; set; }
        public string NomeFuncionario { get; set; }
        
        public int IdCargo { get; set; }
        public string DescricaoCargo { get; set; }
        
        public string DataVinculo { get; set; }
    }
}