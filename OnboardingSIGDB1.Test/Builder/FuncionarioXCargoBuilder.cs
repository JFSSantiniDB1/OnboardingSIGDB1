using Bogus;
using OnboardingSIGDB1.Domain.Dto.Funcionarios;
using OnboardingSIGDB1.Domain.Dto.FuncionarioXCargos;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Test.Builder;

public class FuncionarioXCargoBuilder
{
    private int _id;
    private int _idFuncionario;
    private int _idCargo;
    private DateTime _dataVinculo;

    public FuncionarioXCargoBuilder()
    {
        var faker = new Faker();
        _idFuncionario = faker.Random.Int();
        _idCargo = faker.Random.Int();
        _dataVinculo = faker.Date.Past(30);
    }

    public static FuncionarioXCargoBuilder Novo()
    {
        return new FuncionarioXCargoBuilder();
    }
    
    public FuncionarioXCargoBuilder ComIdFuncionario(int idFuncionario)
    {
        _idFuncionario = idFuncionario;
        return this;
    }
    
    public FuncionarioXCargoBuilder ComIdCargo(int idCargo)
    {
        _idCargo = idCargo;
        return this;
    }
    
    public FuncionarioXCargoBuilder ComDataVinculo(DateTime dataVinculo)
    {
        _dataVinculo = dataVinculo;
        return this;
    }  
    
    public FuncionarioXCargoBuilder ComId(int id)
    {
        _id = id;
        return this;
    }  
    
    public FuncionarioXCargo Build()
    {
        var entity = new FuncionarioXCargo();
        entity.Id = _id;
        entity.IdCargo = _idCargo;
        entity.IdFuncionario = _idFuncionario;
        entity.DataVinculo = _dataVinculo;
        return entity;
    }
    
    public FuncionarioXCargoDto BuildDto()
    {
        var dto = new FuncionarioXCargoDto();
        dto.Id = _id;
        dto.IdCargo = _idCargo;
        dto.IdFuncionario = _idFuncionario;
        dto.DataVinculo = _dataVinculo.ToString("dd/MM/yyyy");
        return dto;
    }
}