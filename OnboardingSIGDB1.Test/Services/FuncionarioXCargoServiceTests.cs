using Microsoft.Extensions.DependencyInjection;
using Moq;
using OnboardingSIGDB1.Domain.Dto.FuncionarioXCargos;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Services.FuncionarioXCargos;
using OnboardingSIGDB1.Domain.Utils;
using OnboardingSIGDB1.Test.Builder;
using OnboardingSIGDB1.Test.Util;
using Xunit;

namespace OnboardingSIGDB1.Test.Services;

public class FuncionarioXCargoServiceTests : IClassFixture<InjectionFixture>
{
    private Mock<IFuncionarioXCargoRepository> _repositoryMock;
    private FuncionarioXCargoDto _dtoEsperada;
    private FuncionarioXCargoService _service;
    
    public FuncionarioXCargoServiceTests(InjectionFixture injection)
    {
        _repositoryMock = new Mock<IFuncionarioXCargoRepository>();
        _dtoEsperada = FuncionarioXCargoBuilder.Novo().ComIdCargo(1).ComIdFuncionario(1).BuildDto();
        var notification = injection.ServiceProvider.GetService<INotificationContext>()!;
        notification.ClearNotifications();
        _service = new FuncionarioXCargoService(_repositoryMock.Object, notification,
            new FuncionarioXCargoValidatorService(_repositoryMock.Object));
    }

    [Fact]
    public void NaoDeveAdicionarVinculoFuncionarioCargoComCargoInexistente()
    {
        _repositoryMock.Setup(s => s.GetCargoExists(_dtoEsperada.IdCargo)).Returns(false);
        _repositoryMock.Setup(s => s.GetFuncionarioExists(_dtoEsperada.IdFuncionario)).Returns(true);
        _repositoryMock.Setup(s => s.GetHaveLinkWithCompany(_dtoEsperada.IdFuncionario)).Returns(true);
        _repositoryMock.Setup(s => s.GetLinkAlreadyExists(_dtoEsperada.Id, _dtoEsperada.IdFuncionario, _dtoEsperada.IdCargo)).Returns(false);
        
        _service.Add(_dtoEsperada);
        var retornoValidacao = _service.GetNotifications().FirstOrDefault();
        
        retornoValidacao.ComMensagemEsperada(Messages.CargoNaoExiste);
    }
    
    [Fact]
    public void NaoDeveAdicionarVinculoFuncionarioCargoComFuncionarioInexistente()
    {
        _repositoryMock.Setup(s => s.GetCargoExists(_dtoEsperada.IdCargo)).Returns(true);
        _repositoryMock.Setup(s => s.GetFuncionarioExists(_dtoEsperada.IdFuncionario)).Returns(false);
        _repositoryMock.Setup(s => s.GetHaveLinkWithCompany(_dtoEsperada.IdFuncionario)).Returns(true);
        _repositoryMock.Setup(s => s.GetLinkAlreadyExists(_dtoEsperada.Id, _dtoEsperada.IdFuncionario, _dtoEsperada.IdCargo)).Returns(false);
        
        _service.Add(_dtoEsperada);
        var retornoValidacao = _service.GetNotifications().FirstOrDefault();
        
        retornoValidacao.ComMensagemEsperada(Messages.FuncionarioNaoExiste);
    }
    
    [Fact]
    public void NaoDeveAdicionarVinculoFuncionarioCargoComFuncionarioSemEmpresa()
    {
        _repositoryMock.Setup(s => s.GetCargoExists(_dtoEsperada.IdCargo)).Returns(true);
        _repositoryMock.Setup(s => s.GetFuncionarioExists(_dtoEsperada.IdFuncionario)).Returns(true);
        _repositoryMock.Setup(s => s.GetHaveLinkWithCompany(_dtoEsperada.IdFuncionario)).Returns(false);
        
        _service.Add(_dtoEsperada);
        var retornoValidacao = _service.GetNotifications();
        
        retornoValidacao.FirstOrDefault().ComMensagemEsperada(Messages.FuncionarioSoPodeTerCargoSeEstarEmEmpresa);
    }
    
    [Fact]
    public void NaoDeveAdicionarVinculoFuncionarioCargoRepetido()
    {
        _repositoryMock.Setup(s => s.GetCargoExists(_dtoEsperada.IdCargo)).Returns(true);
        _repositoryMock.Setup(s => s.GetFuncionarioExists(_dtoEsperada.IdFuncionario)).Returns(true);
        _repositoryMock.Setup(s => s.GetHaveLinkWithCompany(_dtoEsperada.IdFuncionario)).Returns(true);
        _repositoryMock.Setup(s => s.GetLinkAlreadyExists(_dtoEsperada.Id, _dtoEsperada.IdFuncionario, _dtoEsperada.IdCargo)).Returns(true);
        
        _service.Add(_dtoEsperada);
        var retornoValidacao = _service.GetNotifications();
        
        retornoValidacao.FirstOrDefault().ComMensagemEsperada(Messages.FuncionarioJaPoissuiCargoEscolhido);
    }
}