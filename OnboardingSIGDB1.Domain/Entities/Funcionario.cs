using System;
using System.Collections.Generic;
using OnboardingSIGDB1.Domain.Base;

namespace OnboardingSIGDB1.Domain.Entities
{
    public class Funcionario : BaseEntityValidation
    {
        private Funcionario()
        {
            Cargos = new List<FuncionarioXCargo>();
        }
        public int Id { get; set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public DateTime? DataContratacao { get; private set; }
        public Empresa Empresa { get; set; }
        public int? IdEmpresa { get; set; }
        public IList<FuncionarioXCargo> Cargos { get; set; }
        
        public void SetCpf(string cpf) => Cpf = cpf;
        public void SetNome(string nome) => Nome = nome;
        public void SetDataContratacao(DateTime? dataContratacao) => DataContratacao = dataContratacao;
        public void VincularCargo(FuncionarioXCargo cargo) => Cargos.Add(cargo);
        public void DesvincularCargo(FuncionarioXCargo cargo) => Cargos.Remove(cargo);
    }
}