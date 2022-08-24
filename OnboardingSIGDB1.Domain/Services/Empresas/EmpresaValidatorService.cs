using System;
using FluentValidation;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Validator;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.Services.Empresas
{
    public class EmpresaValidatorService : AbstractValidator<Empresa>, IEmpresaValidatorService
    {
        private readonly IEmpresaRepository _empresaRepository;
        public EmpresaValidatorService(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;

            #region Nome
            RuleFor(r => r.Nome)
                .NotEmpty()
                .WithMessage(Messages.NomeObrigatorio);
            
            RuleFor(r => r.Nome)
                .MaximumLength(150)
                .WithMessage(Messages.NomeLimiteMax150Caracteres);
            #endregion Nome
            
            #region Cnpj
            RuleFor(r => r.Cnpj)
                .NotEmpty()
                .WithMessage(Messages.CnpjObrigatorio);
            
            RuleFor(r => r.Cnpj)
                .MaximumLength(14)
                .WithMessage(Messages.CnpjLimiteMax14Caracteres);

            RuleFor(x => x.Cnpj)
                .Must(x => BaseValidations.IsCnpj(x))
                .When(x => x.Cnpj?.Length > 0)
                .WithMessage(Messages.CnpjInvalido);
            
            RuleFor(x => x)
                .Must(ValidateCnpjAlreadyExists)
                .When(x => x.Cnpj?.Length > 0)
                .WithMessage(Messages.CnpjRepetido);
            #endregion Cnpj
            
            RuleFor(r => r.DataFundacao)
                .Must(x => x > DateTime.MinValue)
                .WithMessage(Messages.DataFundacaoInvalida);
        }

        private bool ValidateCnpjAlreadyExists(Empresa entity)
        {
            return !_empresaRepository.GetCnpjAlreadyExists(entity.Id, entity.Cnpj);
        }
    }
}