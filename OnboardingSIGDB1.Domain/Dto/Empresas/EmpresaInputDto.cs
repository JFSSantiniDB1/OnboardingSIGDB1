using System;
using System.Collections.Generic;
using OnboardingSIGDB1.Domain.Dto.Funcionarios;

namespace OnboardingSIGDB1.Domain.Dto.Empresas
{
    public class EmpresaInputDto
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public DateTime? DataFundacao { get; set; }
    }
}