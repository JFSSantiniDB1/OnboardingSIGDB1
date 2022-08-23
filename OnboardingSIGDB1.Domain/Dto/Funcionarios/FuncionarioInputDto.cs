using System;
using System.Collections.Generic;
using OnboardingSIGDB1.Domain.Dto.Cargos;

namespace OnboardingSIGDB1.Domain.Dto.Funcionarios
{
    public class FuncionarioInputDto
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime? DataContratacao { get; set; }
        public int? IdEmpresa { get; set; }
    }
}