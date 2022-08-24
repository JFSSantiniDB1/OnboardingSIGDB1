namespace OnboardingSIGDB1.Domain.Utils
{
    public static class Messages
    {
        public const string DescricaoLimiteMax250Caracteres = "O campo 'Descrição' atingiu o limite máximo de caracteres (250).";
        public const string DescricaoObrigatoria = "O campo 'Descrição' é obrigatório.";
        public const string DescricaoRepetida = "A Descrição digitada já existe na base.";
        
        public const string NomeObrigatorio = "O campo 'Nome' é obrigatório.";
        public const string NomeLimiteMax150Caracteres = "O campo 'Nome' atingiu o limite máximo de caracteres (150).";

        public const string CnpjObrigatorio = "O campo 'CNPJ' é obrigatório.";
        public const string CnpjLimiteMax14Caracteres = "O campo 'CNPJ' atingiu o limite máximo de caracteres (14).";
        public const string CnpjInvalido = "A informação no campo 'CNPJ' é inválida.";
        public const string CnpjRepetido = "O CNPJ digitado já existe na base.";
        
        public const string CpfObrigatorio = "O campo 'CPF' é obrigatório.";
        public const string CpfLimiteMax11Caracteres = "O campo 'CPF' atingiu o limite máximo de caracteres (11).";
        public const string CpfInvalido = "A informação no campo 'CPF' é inválida.";
        public const string CpfRepetido = "O CPF digitado já existe na base.";
        
        public const string DataFundacaoInvalida = "A informação no campo 'Data da Fundação' é inválida.";
        public const string DataContratacaoInvalida = "A informação no campo 'Data de contratação' é inválida.";
        
        public const string EmpresaRepetida = "A empresa selecionada não existe.";
        
        public const string FuncionarioSoPodeTerCargoSeEstarEmEmpresa = "O funcionário só pode possuir um cargo se estiver vinculado a uma empresa.";
        public const string FuncionarioNaoExiste = "O funcionário escolhido não existe.";
        public const string FuncionarioJaPoissuiCargoEscolhido = "O funcionário já possui vínculo com o cargo escolhido.";
        public const string CargoNaoExiste = "O cargo escolhido não existe.";
    }
}