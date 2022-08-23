using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.AutoMapper;
using OnboardingSIGDB1.Domain.Dto.Empresas;
using OnboardingSIGDB1.Domain.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace OnboardingSIGDB1.API.Controllers;

[Route("api/empresa")]
[ApiController]
public class EmpresaController : ControllerBase
{
    private readonly IEmpresaService _empresaService;

    public EmpresaController(IEmpresaService empresaService)
    {
        _empresaService = empresaService;
    }

    /// <summary>
    /// Este serviço permite visualizar todas as empresas.
    /// </summary>
    /// <returns>Retorna status 200 e todas as empresas </returns>
    [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter todas as empresas")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var retorno = _empresaService.GetAll(new FiltroEmpresaDto());
        return Ok(retorno);
    }

    /// <summary>
    /// Este serviço permite visualizar as informações de uma determinada empresa.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna status 200 e dados da empresa escolhida</returns>
    [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter os dados da empresa")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var retorno = _empresaService.Get(id);
        if(retorno != null)
            return Ok(retorno);
        return BadRequest(_empresaService.GetNotifications());
    }

    /// <summary>
    /// Este serviço permite visualizar todas as empresas de acordo com o filtro.
    /// </summary>
    /// <param name="filtro"> filtro da requisição</param>
    /// <returns>Retorna status 200 e todas as empresas de acordo com o filtro</returns>
    [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter todas as empresas filtradas")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpGet("pesquisar")]
    public async Task<IActionResult> GetAll([FromQuery] FiltroEmpresaDto filtro)
    {
        var retorno = _empresaService.GetAll(filtro);
        return Ok(retorno);
    }

    /// <summary>
    /// Este serviço permite cadastrar uma nova empresa.
    /// </summary>
    /// <param name="empresaInput"> informações da requisição</param>
    /// <returns>Retorna status 201 e dados da empresa cadastrada</returns>
    [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar uma empresa")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] EmpresaInputDto empresaInput)
    {
        var empresaDto = BaseMapper.Mapper.Map<EmpresaDto>(empresaInput);
        var id = _empresaService.Add(empresaDto);
        empresaDto.Id = id;
        if (id > 0)
            return Created("", id);
        return BadRequest(_empresaService.GetNotifications());
    }

    /// <summary>
    /// Este serviço permite alterar uma empresa.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="empresaInput"></param>
    /// <returns>Retorna status 202 e dados da empresa alterada</returns>
    [SwaggerResponse(statusCode: 202, description: "Sucesso ao alterar uma empresa")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] EmpresaInputDto empresaInput)
    {
        var empresaDto = BaseMapper.Mapper.Map<EmpresaDto>(empresaInput);
        empresaDto.Id = id;
        id = _empresaService.Update(empresaDto);
        if(id > 0)
            return Accepted("", id);
        return BadRequest(_empresaService.GetNotifications());
    }

    /// <summary>
    /// Este serviço permite excluir uma empresa.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna status 202 e id da empresa excluída</returns>
    [SwaggerResponse(statusCode: 202, description: "Sucesso ao excluir uma empresa")]
    [SwaggerResponse(statusCode: 400, description: "Requisição inválida")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        id = _empresaService.Delete(id);
        if(id > 0)
            return Accepted("", id);
        return BadRequest(_empresaService.GetNotifications());
    }
}