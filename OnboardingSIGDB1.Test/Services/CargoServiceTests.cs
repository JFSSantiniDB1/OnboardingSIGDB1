using Microsoft.Extensions.DependencyInjection;
using Moq;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Services.Cargos;
using OnboardingSIGDB1.Domain.Utils;
using OnboardingSIGDB1.Test.Builder;
using OnboardingSIGDB1.Test.Util;
using Xunit;

namespace OnboardingSIGDB1.Test.Services;

public class CargoServiceTests : IClassFixture<InjectionFixture>
{
    private Mock<ICargoRepository> _repositoryMock;
    private CargoService _service;
    public CargoServiceTests(InjectionFixture injection)
    {
        var notification = injection.ServiceProvider.GetService<INotificationContext>()!;
        notification.ClearNotifications();
        _repositoryMock = new Mock<ICargoRepository>();
        _service = new CargoService(_repositoryMock.Object, notification, new CargoValidatorService(_repositoryMock.Object));
    }

    [Fact]
    public void DeveAdicionarCargo()
    {
        var cargoEsperadoDto = CargoBuilder.Novo().BuildDto();
        
        _service.Add(cargoEsperadoDto);
        _repositoryMock.Verify(r => r.Add(
            It.Is<Cargo>(
                c => c.Descricao == cargoEsperadoDto.Descricao
            )
        ), Times.Exactly(1));
    }

    [Fact]
    public void NaoDeveAdicionarCargoComDescricaoJaExistente()
    {
        var cargoEsperadoDto = CargoBuilder.Novo().ComDescricao("Teste cargo").BuildDto();
        
        _repositoryMock.Setup(s => s.GetDescricaoAlreadyExists(cargoEsperadoDto.Id, cargoEsperadoDto.Descricao)).Returns(true);
        _service.Add(cargoEsperadoDto);
        var retornoValidacao = _service.GetNotifications().FirstOrDefault();
        
        retornoValidacao.ComMensagemEsperada(Messages.DescricaoRepetida);
    }
}