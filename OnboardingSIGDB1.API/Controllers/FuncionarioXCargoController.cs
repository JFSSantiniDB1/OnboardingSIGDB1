using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.AutoMapper;
using OnboardingSIGDB1.Domain.Dto.Funcionarios;
using OnboardingSIGDB1.Domain.Dto.FuncionarioXCargos;
using OnboardingSIGDB1.Domain.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace OnboardingSIGDB1.API.Controllers;

/// <summary>
/// Controller da vinculação de cargos a um funcionário.
/// </summary>
[Route("api/cargosFuncionario")]
[ApiController]
public class FuncionarioXCargoController : ControllerBase
{
    private readonly IFuncionarioXCargoService _funcionarioXCargoService;
    /// <summary>
    /// Construtor da controller.
    /// </summary>
    /// <param name="funcionarioXCargoService"></param>
    public FuncionarioXCargoController(IFuncionarioXCargoService funcionarioXCargoService)
    {
        _funcionarioXCargoService = funcionarioXCargoService;
    }
    
    /// <summary>
    /// Este serviço permite visualizar todos os cargos de funcionário de acordo com o filtro.
    /// </summary>
    /// <param name="filtro"> filtro da requisição</param>
    /// <returns>Retorna status 200 e todos os cargos de funcionário de acordo com o filtro</returns>
    [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter todos os funcionários filtrados")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpGet("pesquisar")]
    public async Task<IActionResult> GetAll([FromQuery] FiltroFuncionarioXCargoDto filtro)
    {
        var retorno = _funcionarioXCargoService.GetAll(filtro);
        return Ok(retorno);
    }
    
    /// <summary>
    /// Este serviço permite visualizar as informações de um cargo do funcionário.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna status 200 e dados do cargo do funcionário escolhido</returns>
    [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter os dados do cargo do funcionário")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var retorno = _funcionarioXCargoService.Get(id);
        if(retorno != null)
            return Ok(retorno);
        return BadRequest(_funcionarioXCargoService.GetNotifications());
    }
    
    /// <summary>
    /// Este serviço permite vincular um novo cargo ao funcionário.
    /// </summary>
    /// <param name="funcionarioXCargoInput"> informações da requisição</param>
    /// <returns>Retorna status 201 e dados do funcionário cadastrado</returns>
    [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar um funcionário")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] FuncionarioXCargoInputDto funcionarioXCargoInput)
    {
        var funcionarioXCargoDto = BaseMapper.Mapper.Map<FuncionarioXCargoDto>(funcionarioXCargoInput);
        var id = _funcionarioXCargoService.Add(funcionarioXCargoDto);
        if (id > 0)
            return Created("", id);
        return BadRequest(_funcionarioXCargoService.GetNotifications());
        
    }
    
    /// <summary>
    /// Este serviço permite desvincular um cargo de um funcionário.
    /// </summary>
    /// <param name="deleteDto"></param>
    /// <returns>Retorna status 202 e id do vinculo removido</returns>
    [SwaggerResponse(statusCode: 202, description: "Sucesso ao excluir um funcionário")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        id =_funcionarioXCargoService.Delete(id);
        if (id > 0)
            return Accepted("", id);
        return BadRequest(_funcionarioXCargoService.GetNotifications());
    }
}