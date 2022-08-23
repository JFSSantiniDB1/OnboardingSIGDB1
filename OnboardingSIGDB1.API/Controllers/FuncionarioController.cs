using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.AutoMapper;
using OnboardingSIGDB1.Domain.Dto.Funcionarios;
using OnboardingSIGDB1.Domain.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace OnboardingSIGDB1.API.Controllers;

/// <summary>
/// Controller de Funcionário
/// </summary>
[Route("api/funcionario")]
[ApiController]
public class FuncionarioController : ControllerBase
{
    private readonly IFuncionarioService _funcionarioService;
    /// <summary>
    /// Construtor de funcionário
    /// </summary>
    /// <param name="funcionarioService"></param>
    public FuncionarioController(IFuncionarioService funcionarioService)
    {
        _funcionarioService = funcionarioService;
    }
    
    /// <summary>
    /// Este serviço permite visualizar todos os funcionários.
    /// </summary>
    /// <returns>Retorna status 200 e todos os funcionários </returns>
    [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter todos os funcionários")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var retorno = _funcionarioService.GetAll(new FiltroFuncionarioDto());
        return Ok(retorno);
    }
    
    /// <summary>
    /// Este serviço permite visualizar as informações de um determinado funcionário.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna status 200 e dados do funcionário escolhido</returns>
    [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter os dados do funcionário")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var retorno = _funcionarioService.Get(id);
        if(retorno != null)
            return Ok(retorno);
        return BadRequest(_funcionarioService.GetNotifications());
    }
    
    /// <summary>
    /// Este serviço permite visualizar todos os funcionários de acordo com o filtro.
    /// </summary>
    /// <param name="filtro"> filtro da requisição</param>
    /// <returns>Retorna status 200 e todos os funcionários de acordo com o filtro</returns>
    [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter todos os funcionários filtrados")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpGet("pesquisar")]
    public async Task<IActionResult> GetAll([FromQuery] FiltroFuncionarioDto filtro)
    {
        var retorno = _funcionarioService.GetAll(filtro);
        return Ok(retorno);
    }
    
    /// <summary>
    /// Este serviço permite cadastrar um novo funcionário.
    /// </summary>
    /// <param name="funcionarioInput"> informações da requisição</param>
    /// <returns>Retorna status 201 e dados do funcionário cadastrado</returns>
    [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar um funcionário")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] FuncionarioInputDto funcionarioInput)
    {
        var funcionarioDto = BaseMapper.Mapper.Map<FuncionarioDto>(funcionarioInput);
        var id = _funcionarioService.Add(funcionarioDto);
        if (id > 0)
            return Created("", id);
        return BadRequest(_funcionarioService.GetNotifications());
    }

    /// <summary>
    /// Este serviço permite alterar um funcionário.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="funcionarioInput"></param>
    /// <returns>Retorna status 202 e dados do funcionário alterado</returns>
    [SwaggerResponse(statusCode: 202, description: "Sucesso ao alterar um funcionário")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] FuncionarioInputDto funcionarioInput)
    {
        var funcionarioDto = BaseMapper.Mapper.Map<FuncionarioDto>(funcionarioInput);
        funcionarioDto.Id = id;
        id = _funcionarioService.Update(funcionarioDto);
        if (id > 0)
            return Accepted("", id);
        return BadRequest(_funcionarioService.GetNotifications());
    }
    
    /// <summary>
    /// Este serviço permite excluir um funcionário.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna status 202 e id do funcionário excluído</returns>
    [SwaggerResponse(statusCode: 202, description: "Sucesso ao excluir um funcionário")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        id = _funcionarioService.Delete(id);
        if (id > 0)
            return Accepted("", id);
        return BadRequest(_funcionarioService.GetNotifications());
    }
}