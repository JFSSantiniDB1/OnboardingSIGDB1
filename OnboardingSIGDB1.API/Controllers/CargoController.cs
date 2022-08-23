using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.AutoMapper;
using OnboardingSIGDB1.Domain.Dto.Cargos;
using OnboardingSIGDB1.Domain.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace OnboardingSIGDB1.API.Controllers;

/// <summary>
/// Requisições da entidade Cargo
/// </summary>
[Route("api/cargo")]
[ApiController]
public class CargoController : ControllerBase
{
    private readonly ICargoService _cargoService;
    /// <summary>
    /// Construtor da controller
    /// </summary>
    /// <param name="cargoService"></param>
    public CargoController(ICargoService cargoService)
    {
        _cargoService = cargoService;
    }

    /// <summary>
    /// Este serviço permite visualizar todos os cargos.
    /// </summary>
    /// <returns>Retorna status 200 e todos os cargos </returns>
    [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter todos os cargos")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var retorno = _cargoService.GetAll(new FiltroCargoDto());
        return Ok(retorno);
    }

    /// <summary>
    /// Este serviço permite visualizar as informações de um determinado cargo.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna status 200 e dados do cargo escolhido</returns>
    [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter os dados do cargo")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var retorno = _cargoService.Get(id);
        if(retorno != null)
            return Ok(retorno);
        return BadRequest(_cargoService.GetNotifications());
    }

    /// <summary>
    /// Este serviço permite visualizar todos ao cargos de acordo com o filtro.
    /// </summary>
    /// <param name="filtro"> filtro da requisição</param>
    /// <returns>Retorna status 200 e todos os cargos de acordo com o filtro</returns>
    [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter todos os cargos filtrados")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpGet("pesquisar")]
    public async Task<IActionResult> GetAll([FromQuery] FiltroCargoDto filtro)
    {
        var retorno = _cargoService.GetAll(filtro);
        return Ok(retorno);
    }

    /// <summary>
    /// Este serviço permite cadastrar um novo cargo.
    /// </summary>
    /// <param name="cargoInput"> informações da requisição</param>
    /// <returns>Retorna status 201 e dados do cargo cadastrado</returns>
    [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar um cargo")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CargoInputDto cargoInput)
    {
        var cargoDto = BaseMapper.Mapper.Map<CargoDto>(cargoInput);
        var id = _cargoService.Add(cargoDto);
        if (id > 0)
            return Created("", id);
        return BadRequest(_cargoService.GetNotifications());
    }

    /// <summary>
    /// Este serviço permite alterar um cargo.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cargoInput"></param>
    /// <returns>Retorna status 202 e dados do cargo alterado</returns>
    [SwaggerResponse(statusCode: 202, description: "Sucesso ao alterar um cargo")]
    [SwaggerResponse(statusCode: 204, description: "Informação não encontrada")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] CargoInputDto cargoInput)
    {
        var cargoDto = BaseMapper.Mapper.Map<CargoDto>(cargoInput);
        cargoDto.Id = id;
        id = _cargoService.Update(cargoDto);
        if(id > 0)
            return Accepted("", id);
        return BadRequest(_cargoService.GetNotifications());
    }

    /// <summary>
    /// Este serviço permite excluir um cargo.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna status 202 e id do cargo excluído</returns>
    [SwaggerResponse(statusCode: 202, description: "Sucesso ao excluir um cargo")]
    [SwaggerResponse(statusCode: 204, description: "Informação não encontrada")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        id = _cargoService.Delete(id);
        if(id > 0)
            return Accepted("", id);
        return BadRequest(_cargoService.GetNotifications());
    }
}