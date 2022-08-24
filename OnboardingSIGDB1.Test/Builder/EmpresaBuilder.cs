using Bogus;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Test.Builder;

public class EmpresaBuilder
{
    private string _nome;
    private string _cnpj;
    private DateTime? _dataFundacao;

    public EmpresaBuilder()
    {
        var faker = new Faker();
        _nome = faker.Random.String2(1, 150);
        _cnpj = GerarCnpj();
        _dataFundacao = faker.Date.Past(30);
        
    }

    public static EmpresaBuilder Novo()
    {
        return new EmpresaBuilder();
    }

    public EmpresaBuilder ComNome(string nome)
    {
        _nome = nome;
        return this;
    }
    
    public EmpresaBuilder ComCnpj(string cnpj)
    {
        _cnpj = cnpj;
        return this;
    }    
    
    public EmpresaBuilder ComDataFundacao(DateTime? dataFundacao)
    {
        _dataFundacao = dataFundacao;
        return this;
    }

    public Empresa Build()
    {
        var empresa = new Empresa();
        empresa.SetNome(_nome);
        empresa.SetCnPj(_cnpj);
        empresa.SetDataFundacao(_dataFundacao);
        return empresa;
    }
    
    public static String GerarCnpj()
    {
        int soma = 0, resto = 0;
        int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        Random rnd = new Random();
        string semente = rnd.Next(10000000, 99999999).ToString() + "0001";

        for (int i = 0; i < 12; i++)
            soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        semente = semente + resto;
        soma = 0;

        for (int i = 0; i < 13; i++)
            soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

        resto = soma % 11;

        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        semente = semente + resto;
        return semente;
    }
}