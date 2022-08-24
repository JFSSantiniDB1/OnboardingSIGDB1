﻿using Bogus;
using Moq;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Services.Cargos;
using OnboardingSIGDB1.Domain.Services.Empresas;
using OnboardingSIGDB1.Domain.Utils;
using OnboardingSIGDB1.Test.Builder;
using OnboardingSIGDB1.Test.Util;
using Xunit;

namespace OnboardingSIGDB1.Test.Entities;

public class EmpresaTests : IDisposable
{
    private IEmpresaRepository _repository;
    
    public EmpresaTests()
    {
        _repository = new Mock<IEmpresaRepository>().Object;
    }
    
    public void Dispose()
    {
        Console.WriteLine("Dispose");
    }
    
    [Fact]
    public void DeveCriarEmpresa()
    {
        var empresaEsperada = EmpresaBuilder.Novo().Build();
        var resultado = empresaEsperada.Validate(empresaEsperada, new EmpresaValidatorService(_repository));
        Assert.True(resultado);
    }

    [Fact]
    public void DeveValidarNomeObrigatorio()
    {
        var empresaEsperada = EmpresaBuilder.Novo().ComNome(new Faker().Random.String2(0)).Build();
        empresaEsperada.Validate(empresaEsperada, new EmpresaValidatorService(_repository));
        var retornoValidacao = empresaEsperada.ValidationResult.Errors.FirstOrDefault();
        
        retornoValidacao.ComMensagemEsperada(Messages.NomeObrigatorio);
    }    
    
    [Fact]
    public void DeveValidarNomeLimiteMaximo150Caracteres()
    {
        var empresaEsperada = EmpresaBuilder.Novo().ComNome(new Faker().Random.String2(151)).Build();
        empresaEsperada.Validate(empresaEsperada, new EmpresaValidatorService(_repository));
        var retornoValidacao = empresaEsperada.ValidationResult.Errors.FirstOrDefault();
        
        retornoValidacao.ComMensagemEsperada(Messages.NomeLimiteMax150Caracteres);
    }
    
    [Fact]
    public void DeveValidarCnpjObrigatorio()
    {
        var empresaEsperada = EmpresaBuilder.Novo().ComCnpj(new Faker().Random.String2(0)).Build();
        empresaEsperada.Validate(empresaEsperada, new EmpresaValidatorService(_repository));
        var retornoValidacao = empresaEsperada.ValidationResult.Errors.FirstOrDefault();
        
        retornoValidacao.ComMensagemEsperada(Messages.CnpjObrigatorio);
    }    
    
    [Fact]
    public void DeveValidarCnpjLimiteMaximo14Caracteres()
    {
        var empresaEsperada = EmpresaBuilder.Novo().ComCnpj(new Faker().Random.String2(20)).Build();
        empresaEsperada.Validate(empresaEsperada, new EmpresaValidatorService(_repository));
        var retornoValidacao = empresaEsperada.ValidationResult.Errors.FirstOrDefault();
        
        retornoValidacao.ComMensagemEsperada(Messages.CnpjLimiteMax14Caracteres);
    }
    
    [Theory]
    [InlineData("11111111111111")]
    [InlineData("22222222222222")]
    [InlineData("33333333333333")]
    [InlineData("44444444444444")]
    public void DeveValidarFormatoCnpj(string cnpj)
    {
        var empresaEsperada = EmpresaBuilder.Novo().ComCnpj(cnpj).Build();
        empresaEsperada.Validate(empresaEsperada, new EmpresaValidatorService(_repository));
        var retornoValidacao = empresaEsperada.ValidationResult.Errors.FirstOrDefault();
        
        retornoValidacao.ComMensagemEsperada(Messages.CnpjInvalido);
    }
    
    [Fact]
    public void DeveValidarDataFundacaoMinima()
    {
        var empresaEsperada = EmpresaBuilder.Novo().ComDataFundacao(DateTime.MinValue).Build();
        empresaEsperada.Validate(empresaEsperada, new EmpresaValidatorService(_repository));
        var retornoValidacao = empresaEsperada.ValidationResult.Errors.FirstOrDefault();
        
        retornoValidacao.ComMensagemEsperada(Messages.DataFundacaoInvalida);
    }
    
}