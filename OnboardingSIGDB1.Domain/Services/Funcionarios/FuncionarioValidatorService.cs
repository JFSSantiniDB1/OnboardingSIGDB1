using System;
using FluentValidation;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Validator;

namespace OnboardingSIGDB1.Domain.Services.Funcionarios
{
    public class FuncionarioValidatorService : AbstractValidator<Funcionario>, IFuncionarioValidatorService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IEmpresaRepository _empresaRepository;

        public FuncionarioValidatorService(IFuncionarioRepository funcionarioRepository,
            IEmpresaRepository empresaRepository)
        {
            _funcionarioRepository = funcionarioRepository;
            _empresaRepository = empresaRepository;

            #region Nome

            RuleFor(r => r.Nome)
                .NotEmpty()
                .WithMessage("O campo 'nome' é obrigatório.");

            RuleFor(r => r.Nome)
                .MaximumLength(150)
                .WithMessage("O campo 'nome' atingiu o limite máximo de caracteres (150).");

            #endregion Nome

            #region Cpf

            RuleFor(r => r.Cpf)
                .NotEmpty()
                .WithMessage("O campo 'CPF' é obrigatório.");

            RuleFor(r => r.Cpf)
                .MaximumLength(11)
                .WithMessage("O campo 'CPF' atingiu o limite máximo de caracteres (11).");

            RuleFor(x => x.Cpf)
                .Must(x => BaseValidations.IsCpf(x))
                .When(x => x.Cpf?.Length > 0)
                .WithMessage("A informação no campo 'CPF' é inválida.");

            RuleFor(x => x)
                .Must(ValidateCpfAlreadyExists)
                .When(x => x.Cpf?.Length > 0)
                .WithMessage("O CPF digitado já existe na base.");

            #endregion Cpf

            RuleFor(r => r.DataContratacao)
                .Must(x => x > DateTime.MinValue)
                .WithMessage("A informação no campo 'Data de contratação' é inválida.");

            RuleFor(x => x.IdEmpresa)
                .Must(ValidateEmpresaExists)
                .WithMessage("A empresa selecionada não existe.");
        }

        private bool ValidateEmpresaExists(int? idEmpresa)
        {
            if (!idEmpresa.HasValue)
                return false;
            return _empresaRepository.Get(x => x.Id == idEmpresa) != null;
        }

        private bool ValidateCpfAlreadyExists(Funcionario entity)
        {
            return !_funcionarioRepository.GetCpfAlreadyExists(entity.Id, entity.Cpf);
        }
    }
}