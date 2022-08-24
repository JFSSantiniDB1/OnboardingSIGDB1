using Bogus;
using Moq;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Services.Empresas;
using OnboardingSIGDB1.Domain.Services.Funcionarios;
using OnboardingSIGDB1.Domain.Utils;
using OnboardingSIGDB1.Test.Builder;
using OnboardingSIGDB1.Test.Util;
using Xunit;

namespace OnboardingSIGDB1.Test.Entities;

public class FuncionarioTests : IDisposable
{
    private IFuncionarioRepository _repository;
    private IEmpresaRepository _repositoryEmpresa;

    public FuncionarioTests()
    {
        _repository = new Mock<IFuncionarioRepository>().Object;
        _repositoryEmpresa = new Mock<IEmpresaRepository>().Object;
    }

    public void Dispose()
    {
        Console.WriteLine("Dispose");
    }

    [Fact]
    public void DeveCriarFuncionario()
    {
        var funcionarioEsperado = FuncionarioBuilder.Novo().Build();
        var resultado = funcionarioEsperado.Validate(funcionarioEsperado, new FuncionarioValidatorService(_repository,_repositoryEmpresa));
        Assert.True(resultado);
    }

    [Fact]
    public void DeveValidarNomeObrigatorio()
    {
        var funcionarioEsperado = FuncionarioBuilder.Novo().ComNome(new Faker().Random.String2(0)).Build();
        funcionarioEsperado.Validate(funcionarioEsperado, new FuncionarioValidatorService(_repository, _repositoryEmpresa));
        var retornoValidacao = funcionarioEsperado.ValidationResult.Errors.FirstOrDefault();

        retornoValidacao.ComMensagemEsperada(Messages.NomeObrigatorio);
    }

    [Fact]
    public void DeveValidarNomeLimiteMaximo150Caracteres()
    {
        var funcionarioEsperado = FuncionarioBuilder.Novo().ComNome(new Faker().Random.String2(151)).Build();
        funcionarioEsperado.Validate(funcionarioEsperado, new FuncionarioValidatorService(_repository, _repositoryEmpresa));
        var retornoValidacao = funcionarioEsperado.ValidationResult.Errors.FirstOrDefault();

        retornoValidacao.ComMensagemEsperada(Messages.NomeLimiteMax150Caracteres);
    }

    [Fact]
    public void DeveValidarCpfObrigatorio()
    {
        var funcionarioEsperado = FuncionarioBuilder.Novo().ComCpf(new Faker().Random.String2(0)).Build();
        funcionarioEsperado.Validate(funcionarioEsperado, new FuncionarioValidatorService(_repository, _repositoryEmpresa));
        var retornoValidacao = funcionarioEsperado.ValidationResult.Errors.FirstOrDefault();

        retornoValidacao.ComMensagemEsperada(Messages.CpfObrigatorio);
    }

    [Fact]
    public void DeveValidarCpfLimiteMaximo11Caracteres()
    {
        var funcionarioEsperado = FuncionarioBuilder.Novo().ComCpf(new Faker().Random.String2(20)).Build();
        funcionarioEsperado.Validate(funcionarioEsperado, new FuncionarioValidatorService(_repository, _repositoryEmpresa));
        var retornoValidacao = funcionarioEsperado.ValidationResult.Errors.FirstOrDefault();

        retornoValidacao.ComMensagemEsperada(Messages.CpfLimiteMax11Caracteres);
    }

    [Theory]
    [InlineData("12345678910")]
    [InlineData("98765432199")]
    [InlineData("99999999911")]
    public void DeveValidarFormatoCpf(string cpf)
    {
        var funcionarioEsperado = FuncionarioBuilder.Novo().ComCpf(cpf).Build();
        funcionarioEsperado.Validate(funcionarioEsperado, new FuncionarioValidatorService(_repository, _repositoryEmpresa));
        var retornoValidacao = funcionarioEsperado.ValidationResult.Errors.FirstOrDefault();

        retornoValidacao.ComMensagemEsperada(Messages.CpfInvalido);
    }

    [Fact]
    public void DeveValidarDataContratacaoMinima()
    {
        var funcionarioEsperado = FuncionarioBuilder.Novo().ComDataContratacao(DateTime.MinValue).Build();
        funcionarioEsperado.Validate(funcionarioEsperado, new FuncionarioValidatorService(_repository, _repositoryEmpresa));
        var retornoValidacao = funcionarioEsperado.ValidationResult.Errors.FirstOrDefault();

        retornoValidacao.ComMensagemEsperada(Messages.DataContratacaoInvalida);
    }
}