using System;

namespace OnboardingSIGDB1.Domain.Dto.Empresas
{
    public class FiltroEmpresaDto
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public DateTime? DataFundacaoInicio { get; set; }
        public DateTime? DataFundacaoFim { get; set; }
    }
}