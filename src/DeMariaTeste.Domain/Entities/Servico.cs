using DeMariaTeste.Domain.Exceptions;

namespace DeMariaTeste.Domain.Entities
{
    public class Servico
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal ValorBase { get; set; }
        public decimal PercentualImposto { get; set; }
        public bool Ativo { get; set; }

        public Servico()
        {
            Ativo = true;
        }

        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                throw new DominioException("O nome do servico e obrigatorio.");

            if (ValorBase <= 0)
                throw new DominioException("O valor base deve ser maior que zero.");

            if (PercentualImposto < 0 || PercentualImposto > 100)
                throw new DominioException("O percentual de imposto precisa estar entre 0 e 100.");
        }
    }
}
