using System;
using System.Collections.Generic;
using OnboardingSIGDB1.Domain.Dto.Funcionarios;

namespace OnboardingSIGDB1.Domain.Dto.Empresas
{
    public class EmpresaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string DataFundacao { get; set; }
        
        public int QuantidadeFuncionarios { get; set; }
        
        public int QuantidadeFuncionariosComCargo { get; set; }
    }
}