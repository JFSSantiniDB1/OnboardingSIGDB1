using FluentValidation;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Validator;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.Services.FuncionarioXCargos
{
    public class FuncionarioXCargoValidatorService : AbstractValidator<FuncionarioXCargo>, IFuncionarioXCargoValidatorService
    {
        private readonly IFuncionarioXCargoRepository _funcionarioXCargoRepository;
        private bool _funcionarioExiste;
        private bool _cargoExiste;
        
        public FuncionarioXCargoValidatorService(IFuncionarioXCargoRepository funcionarioXCargoRepository)
        {
            _funcionarioXCargoRepository = funcionarioXCargoRepository;
            
            RuleFor(x => x.IdFuncionario)
                .Must(ValidateHaveLinkWithCompany)
                .WithMessage(Messages.FuncionarioSoPodeTerCargoSeEstarEmEmpresa);
            
            RuleFor(x => x.IdFuncionario)
                .Must(ValidateFuncionarioExists)
                .WithMessage(Messages.FuncionarioNaoExiste);
            
            RuleFor(x => x.IdCargo)
                .Must(ValidateCargoExists)
                .WithMessage(Messages.CargoNaoExiste);            
            
            RuleFor(x => x)
                .Must(ValidateLinkAlreadyExists)
                .WithMessage(Messages.FuncionarioJaPoissuiCargoEscolhido);
        }

        private bool ValidateCargoExists(int idCargo)
        {
            _cargoExiste = _funcionarioXCargoRepository.GetCargoExists(idCargo);
            return _cargoExiste;
        }

        private bool ValidateFuncionarioExists(int idFuncionario)
        {
            _funcionarioExiste = _funcionarioXCargoRepository.GetFuncionarioExists(idFuncionario);
            return _funcionarioExiste;
        }

        private bool ValidateLinkAlreadyExists(FuncionarioXCargo entity)
        {
            if(_funcionarioExiste && _cargoExiste)
                return !_funcionarioXCargoRepository.GetLinkAlreadyExists(entity.Id, entity.IdFuncionario, entity.IdCargo);
            return true;
        }

        private bool ValidateHaveLinkWithCompany(int idFuncionario)
        {
            if(_funcionarioExiste && _cargoExiste)
                return _funcionarioXCargoRepository.GetHaveLinkWithCompany(idFuncionario);
            return true;
        }
    }
}