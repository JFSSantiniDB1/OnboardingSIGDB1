using Bogus;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Test.Builder;

public class CargoBuilder
{
    private string _descricao;

    public CargoBuilder()
    {
        var faker = new Faker();
        _descricao = faker.Random.String2(1, 250);
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

    public Cargo Build()
    {
        var cargo = new Cargo();
        cargo.SetDescricao(_descricao);
        return cargo;
    }
}