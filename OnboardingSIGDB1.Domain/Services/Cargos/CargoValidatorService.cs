using FluentValidation;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Validator;

namespace OnboardingSIGDB1.Domain.Services.Cargos
{
    public class CargoValidatorService : AbstractValidator<Cargo>, ICargoValidatorService
    {
        private readonly ICargoRepository _cargoRepository;
        public CargoValidatorService(ICargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
            
            #region Descricao
            RuleFor(r => r.Descricao)
                .NotEmpty()
                .WithMessage("O campo 'Descrição' é obrigatório.");
            
            RuleFor(r => r.Descricao)
                .MaximumLength(250)
                .WithMessage("O campo 'Descrição' atingiu o limite máximo de caracteres (250).");
            
            RuleFor(x => x)
                .Must(ValidateDescricaoAlreadyExists)
                .WithMessage("A Descrição digitada já existe na base.");
            #endregion Descricao
        }

        private bool ValidateDescricaoAlreadyExists(Cargo entity)
        {
            return !_cargoRepository.GetDescricaoAlreadyExists(entity.Id, entity.Descricao);
        }
    }
}