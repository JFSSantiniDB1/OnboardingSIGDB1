using Bogus;
using Moq;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Services.Cargos;
using OnboardingSIGDB1.Domain.Utils;
using OnboardingSIGDB1.Test.Builder;
using OnboardingSIGDB1.Test.Util;
using Xunit;

namespace OnboardingSIGDB1.Test.Entities;

public class CargoTests : IDisposable
{
    private ICargoRepository _repository;
    public CargoTests()
    {
        _repository = new Mock<ICargoRepository>().Object;
    }
    
    public void Dispose()
    {
        Console.WriteLine("Dispose");
    }

    [Fact]
    public void DeveCriarCargo()
    {
        var cargoEsperado = CargoBuilder.Novo().Build();
        var resultado = cargoEsperado.Validate(cargoEsperado, new CargoValidatorService(_repository));
        Assert.True(resultado);
    }

    [Fact]
    public void DeveValidarDescricaoObrigatoria()
    {
        var cargoEsperado = CargoBuilder.Novo().ComDescricao(new Faker().Random.String2(0)).Build();
        cargoEsperado.Validate(cargoEsperado, new CargoValidatorService(_repository));
        var retornoValidacao = cargoEsperado.ValidationResult.Errors.FirstOrDefault();
        
        retornoValidacao.ComMensagemEsperada(Messages.DescricaoObrigatoria);
    }    
    
    [Fact]
    public void DeveValidarDescricaoLimiteMaximo250Caracteres()
    {
        var cargoEsperado = CargoBuilder.Novo().ComDescricao(new Faker().Random.String2(251)).Build();
        cargoEsperado.Validate(cargoEsperado, new CargoValidatorService(_repository));
        var retornoValidacao = cargoEsperado.ValidationResult.Errors.FirstOrDefault();
        
        retornoValidacao.ComMensagemEsperada(Messages.DescricaoLimiteMax250Caracteres);
    }
}