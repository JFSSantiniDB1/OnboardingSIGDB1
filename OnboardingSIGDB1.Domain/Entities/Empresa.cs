using System;
using System.Collections.Generic;
using OnboardingSIGDB1.Domain.Base;

namespace OnboardingSIGDB1.Domain.Entities
{
    public class Empresa : BaseEntityValidation
    {
        public Empresa()
        {
            Funcionarios = new List<Funcionario>();
        }
        
        public int Id { get; set; }
        public string Nome { get; private set; }
        public string Cnpj { get; private set; }
        public DateTime? DataFundacao { get; private set; }
        public IList<Funcionario> Funcionarios { get; private set; }

        public void SetCnPj(string cnpj) => Cnpj = cnpj;
        public void SetNome(string nome) => Nome = nome;
        public void SetDataFundacao(DateTime? dataFundacao) => DataFundacao = dataFundacao;
        public void VincularFuncionario(Funcionario funcionario) => Funcionarios.Add(funcionario);
        public void DesvincularFuncionario(Funcionario funcionario) => Funcionarios.Remove(funcionario);
    }
}