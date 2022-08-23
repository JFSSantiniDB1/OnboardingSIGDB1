using System;

namespace OnboardingSIGDB1.Domain.Dto.Funcionarios
{
    public class FuncionarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string DataContratacao { get; set; }
        public int? IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public int? IdCargoAtual { get; set; }
        public string CargoAtual { get; set; }
        public string DataVinculoCargo { get; set; }
        
    }
}