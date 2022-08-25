using Microsoft.Extensions.DependencyInjection;
using Moq;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Services.Funcionarios;
using OnboardingSIGDB1.Domain.Utils;
using OnboardingSIGDB1.Test.Builder;
using OnboardingSIGDB1.Test.Util;
using Xunit;

namespace OnboardingSIGDB1.Test.Services;

public class FuncionarioServiceTests : IClassFixture<InjectionFixture>
{
    private Mock<IFuncionarioRepository> _funcionarioRepositoryMock;
    private Mock<IEmpresaRepository> _empresaRepositoryMock;
    private FuncionarioService _service;

    public FuncionarioServiceTests(InjectionFixture injection)
    {
        var notification = injection.ServiceProvider.GetService<INotificationContext>()!;
        notification.ClearNotifications();
        _funcionarioRepositoryMock = new Mock<IFuncionarioRepository>();
        _empresaRepositoryMock = new Mock<IEmpresaRepository>();
        _service = new FuncionarioService(_funcionarioRepositoryMock.Object, notification,
            new FuncionarioValidatorService(_funcionarioRepositoryMock.Object, _empresaRepositoryMock.Object));
    }

    [Fact]
    public void DeveAdicionarFuncionario()
    {
        var funcionarioEsperadoDto = FuncionarioBuilder.Novo().BuildDto();
        
        _service.Add(funcionarioEsperadoDto);
        
        _funcionarioRepositoryMock.Verify(r => r.Add(
            It.Is<Funcionario>(
                c => c.Nome == funcionarioEsperadoDto.Nome &&
                     c.Cpf == Convertions.GetCpfSemMascara(funcionarioEsperadoDto.Cpf) &&
                     c.DataContratacao == Convertions.GetDateTime(funcionarioEsperadoDto.DataContratacao)
            )
        ), Times.Exactly(1));
    }

    [Fact]
    public void NaoDeveAdicionarFuncionarioComCpfJaExistente()
    {
        var funcionarioEsperadoDto = FuncionarioBuilder.Novo().BuildDto();
        
        _funcionarioRepositoryMock.Setup(s => s.GetCpfAlreadyExists(funcionarioEsperadoDto.Id, funcionarioEsperadoDto.Cpf)).Returns(true);
        _service.Add(funcionarioEsperadoDto);
        var retornoValidacao = _service.GetNotifications().FirstOrDefault();
        
        retornoValidacao.ComMensagemEsperada(Messages.CpfRepetido);
    }

    [Fact]
    public void NaoDeveAdicionarFuncionarioComEmpresaInexistente()
    {
        var funcionarioEsperadoDto = FuncionarioBuilder.Novo().ComIdEmpresa(1).BuildDto();
        
        _empresaRepositoryMock.Setup(s => s.Get(x => x.Id == funcionarioEsperadoDto.Id)).Returns((Empresa)null!);
        _service.Add(funcionarioEsperadoDto);
        var retornoValidacao = _service.GetNotifications().FirstOrDefault();

        retornoValidacao.ComMensagemEsperada(Messages.EmpresaNaoExiste);
    }
}