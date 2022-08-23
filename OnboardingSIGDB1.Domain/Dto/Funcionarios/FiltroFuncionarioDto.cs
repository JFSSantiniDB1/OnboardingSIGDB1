using System;

namespace OnboardingSIGDB1.Domain.Dto.Funcionarios
{
    public class FiltroFuncionarioDto
    {
        public int? IdEmpresa { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime? DataContratacaoInicio { get; set; }
        public DateTime? DataContratacaoFim { get; set; }
    }
}