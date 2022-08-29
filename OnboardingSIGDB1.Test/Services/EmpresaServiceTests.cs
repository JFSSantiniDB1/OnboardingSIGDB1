using Microsoft.Extensions.DependencyInjection;
using Moq;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Services.Empresas;
using OnboardingSIGDB1.Domain.Utils;
using OnboardingSIGDB1.Test.Builder;
using OnboardingSIGDB1.Test.Util;
using Xunit;

namespace OnboardingSIGDB1.Test.Services;

public class EmpresaServiceTests: IClassFixture<InjectionFixture>
{
    private Mock<IEmpresaRepository> _repositoryMock;
    private EmpresaService _service;
    public EmpresaServiceTests(InjectionFixture injection)
    {
        var notification = injection.ServiceProvider.GetService<INotificationContext>()!;
        notification.ClearNotifications();
        _repositoryMock = new Mock<IEmpresaRepository>();
        _service = new EmpresaService(_repositoryMock.Object, notification,
            new EmpresaValidatorService(_repositoryMock.Object));
    }
    
    [Fact]
    public void DeveAdicionarEmpresa()
    {
        var empresaEsperadaDto = EmpresaBuilder.Novo().BuildDto();

        _service.Add(empresaEsperadaDto);
        _repositoryMock.Verify(r => r.Add(
            It.Is<Empresa>(
                c => c.Nome == empresaEsperadaDto.Nome &&
                     c.Cnpj == Convertions.GetCnpjSemMascara(empresaEsperadaDto.Cnpj) &&
                     c.DataFundacao == Convertions.GetDateTime(empresaEsperadaDto.DataFundacao)
            )
        ), Times.Exactly(1));
    } 
    
    [Fact]
    public void DeveAlterarEmpresaNomeDiferente()
    {
        var empresaEsperadaDto = EmpresaBuilder.Novo().ComId(1).ComNome("A").BuildDto();
        var empresaEsperada = EmpresaBuilder.Novo().ComId(1).ComNome("B").Build();
        
        _repositoryMock.Setup(s => s.GetEntityOnly(x => x.Id == empresaEsperadaDto.Id)).Returns(empresaEsperada);
        
        _service.Update(empresaEsperadaDto);

        _repositoryMock.Verify(r => r.Update(
            It.Is<Empresa>(
                c => c.Nome == empresaEsperadaDto.Nome
            )
        ), Times.Exactly(1));
    }
    
    [Fact]
    public void NaoDeveAdicionarEmpresaComCnpjJaExistente()
    {
        var empresaEsperadaDto = EmpresaBuilder.Novo().BuildDto();

        _repositoryMock.Setup(s => s.GetCnpjAlreadyExists(empresaEsperadaDto.Id, empresaEsperadaDto.Cnpj)).Returns(true);
        _service.Add(empresaEsperadaDto);
        var retornoValidacao = _service.GetNotifications().FirstOrDefault();
        
        retornoValidacao.ComMensagemEsperada(Messages.CnpjRepetido);
    }
}