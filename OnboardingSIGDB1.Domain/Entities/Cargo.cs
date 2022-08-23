using OnboardingSIGDB1.Domain.Base;

namespace OnboardingSIGDB1.Domain.Entities
{
    public class Cargo : BaseEntityValidation
    {
        public int Id { get; set; }
        public string Descricao { get; private set; }

        public void SetDescricao(string descricao) => Descricao = descricao;
    }
}