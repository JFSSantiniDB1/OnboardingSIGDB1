using Bogus;
using OnboardingSIGDB1.Domain.Dto.Funcionarios;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Test.Builder;

public class FuncionarioBuilder
{
    private string _nome;
    private string _cpf;
    private DateTime? _dataContratacao;
    private int? _idEmpresa = null;

    public FuncionarioBuilder()
    {
        var faker = new Faker();
        _nome = faker.Random.String2(1, 150);
        _cpf = GerarCpf();
        _dataContratacao = faker.Date.Past(30);
    }

    public static FuncionarioBuilder Novo()
    {
        return new FuncionarioBuilder();
    }

    public FuncionarioBuilder ComNome(string nome)
    {
        _nome = nome;
        return this;
    }
    
    public FuncionarioBuilder ComCpf(string cpf)
    {
        _cpf = cpf;
        return this;
    }    
    
    public FuncionarioBuilder ComDataContratacao(DateTime? dataContratacao)
    {
        _dataContratacao = dataContratacao;
        return this;
    }    
    
    public FuncionarioBuilder ComIdEmpresa(int idEmpresa)
    {
        _idEmpresa = idEmpresa;
        return this;
    }

    public Funcionario Build()
    {
        var funcionario = new Funcionario();
        funcionario.SetNome(_nome);
        funcionario.SetCpf(_cpf);
        funcionario.SetDataContratacao(_dataContratacao);
        funcionario.IdEmpresa = _idEmpresa;
        return funcionario;
    }
    
    public FuncionarioDto BuildDto()
    {
        var funcionario = new FuncionarioDto();
        funcionario.Nome = _nome;
        funcionario.Cpf = _cpf;
        funcionario.DataContratacao = _dataContratacao?.ToString("dd/MM/yyyy");
        funcionario.IdEmpresa = _idEmpresa;
        return funcionario;
    }
    
    public static string GerarCpf()
    {
        int soma = 0, resto = 0;
        var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        var rnd = new Random();
        var semente = rnd.Next(100000000, 999999999).ToString();

        for (var i = 0; i < 9; i++)
            soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        semente += resto;
        soma = 0;

        for (var i = 0; i < 10; i++)
            soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

        resto = soma % 11;

        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        semente += resto;
        return semente;
    }
}