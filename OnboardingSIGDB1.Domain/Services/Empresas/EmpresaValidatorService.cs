using System;
using FluentValidation;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Validator;

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
                .WithMessage("O campo 'nome' é obrigatório.");
            
            RuleFor(r => r.Nome)
                .MaximumLength(150)
                .WithMessage("O campo 'nome' atingiu o limite máximo de caracteres (150).");
            #endregion Nome
            
            #region Cnpj
            RuleFor(r => r.Cnpj)
                .NotEmpty()
                .WithMessage("O campo 'CNPJ' é obrigatório.");
            
            RuleFor(r => r.Cnpj)
                .MaximumLength(14)
                .WithMessage("O campo 'CNPJ' atingiu o limite máximo de caracteres (14).");

            RuleFor(x => x.Cnpj)
                .Must(x => BaseValidations.IsCnpj(x))
                .When(x => x.Cnpj?.Length > 0)
                .WithMessage("A informação no campo 'CNPJ' é inválida.");
            
            RuleFor(x => x)
                .Must(ValidateCnpjAlreadyExists)
                .When(x => x.Cnpj?.Length > 0)
                .WithMessage("O CNPJ digitado já existe na base.");
            #endregion Cnpj
            
            RuleFor(r => r.DataFundacao)
                .Must(x => x > DateTime.MinValue)
                .WithMessage("A informação no campo 'Data da Fundação' é inválida.");
        }

        private bool ValidateCnpjAlreadyExists(Empresa entity)
        {
            return !_empresaRepository.GetCnpjAlreadyExists(entity.Id, entity.Cnpj);
        }
    }
}