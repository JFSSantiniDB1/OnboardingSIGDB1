using Bogus;
using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Domain.Interfaces.Validator;
using OnboardingSIGDB1.Domain.Utils;
using OnboardingSIGDB1.Test.Builder;
using OnboardingSIGDB1.Test.Util;
using Xunit;

namespace OnboardingSIGDB1.Test.Entities;

public class CargoTests : IClassFixture<InjectionFixture>
{
    private readonly ICargoValidatorService _validator;

    public CargoTests(InjectionFixture injection)
    {
        _validator = injection.ServiceProvider.GetService<ICargoValidatorService>()!;
    }

    [Fact]
    public void DeveCriarCargoValido()
    {
        var cargoEsperado = CargoBuilder.Novo().Build();
        var resultado = cargoEsperado.Validate(cargoEsperado, _validator);
        Assert.True(resultado);
    }

    [Fact]
    public void DeveValidarDescricaoObrigatoria()
    {
        var cargoEsperado = CargoBuilder.Novo().ComDescricao(new Faker().Random.String2(0)).Build();
        cargoEsperado.Validate(cargoEsperado, _validator);
        var retornoValidacao = cargoEsperado.ValidationResult.Errors.FirstOrDefault();

        retornoValidacao.ComMensagemEsperada(Messages.DescricaoObrigatoria);
    }

    [Fact]
    public void DeveValidarDescricaoLimiteMaximo250Caracteres()
    {
        var cargoEsperado = CargoBuilder.Novo().ComDescricao(new Faker().Random.String2(251)).Build();
        cargoEsperado.Validate(cargoEsperado, _validator);
        var retornoValidacao = cargoEsperado.ValidationResult.Errors.FirstOrDefault();

        retornoValidacao.ComMensagemEsperada(Messages.DescricaoLimiteMax250Caracteres);
    }
}