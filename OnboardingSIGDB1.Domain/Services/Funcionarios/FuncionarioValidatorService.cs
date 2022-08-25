using System;
using FluentValidation;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Validator;
using OnboardingSIGDB1.Domain.Utils;

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
                .WithMessage(Messages.NomeObrigatorio);

            RuleFor(r => r.Nome)
                .MaximumLength(150)
                .WithMessage(Messages.NomeLimiteMax150Caracteres);

            #endregion Nome

            #region Cpf

            RuleFor(r => r.Cpf)
                .NotEmpty()
                .WithMessage(Messages.CpfObrigatorio);

            RuleFor(r => r.Cpf)
                .MaximumLength(11)
                .WithMessage(Messages.CpfLimiteMax11Caracteres);

            RuleFor(x => x.Cpf)
                .Must(x => BaseValidations.IsCpf(x))
                .When(x => x.Cpf?.Length > 0)
                .WithMessage(Messages.CpfInvalido);

            RuleFor(x => x)
                .Must(ValidateCpfAlreadyExists)
                .When(x => x.Cpf?.Length > 0)
                .WithMessage(Messages.CpfRepetido);

            #endregion Cpf

            RuleFor(r => r.DataContratacao)
                .Must(x => x > DateTime.MinValue)
                .WithMessage(Messages.DataContratacaoInvalida);

            RuleFor(x => x.IdEmpresa)
                .Must(ValidateEmpresaExists)
                .WithMessage(Messages.EmpresaNaoExiste);
        }

        private bool ValidateEmpresaExists(int? idEmpresa)
        {
            if (!idEmpresa.HasValue)
                return true;
            return _empresaRepository.Get(x => x.Id == idEmpresa) != null;
        }

        private bool ValidateCpfAlreadyExists(Funcionario entity)
        {
            return !_funcionarioRepository.GetCpfAlreadyExists(entity.Id, entity.Cpf);
        }
    }
}