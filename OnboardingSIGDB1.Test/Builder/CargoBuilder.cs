using Bogus;
using OnboardingSIGDB1.Domain.Dto.Cargos;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Test.Builder;

public class CargoBuilder
{
    private string _descricao;
    private int _id;

    public CargoBuilder()
    {
        var faker = new Faker();
        _descricao = faker.Random.String2(1, 250);
        _id = 0;
    }

    public static CargoBuilder Novo()
    {
        return new CargoBuilder();
    }

    public CargoBuilder ComDescricao(string descricao)
    {
        _descricao = descricao;
        return this;
    }    
    
    public CargoBuilder ComId(int id)
    {
        _id = id;
        return this;
    }

    public Cargo Build()
    {
        var cargo = new Cargo();
        cargo.Id = _id;
        cargo.SetDescricao(_descricao);
        return cargo;
        
    }
    public CargoDto BuildDto()
    {
        var cargoDto = new CargoDto();
        cargoDto.Id = _id;
        cargoDto.Descricao = _descricao;
        return cargoDto;
    }
}