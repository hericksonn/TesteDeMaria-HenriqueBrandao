using DeMariaTeste.Domain.Exceptions;

namespace DeMariaTeste.Domain.Entities
{
    public class ItemOrdemServico
    {
        public int Id { get; set; }
        public int OrdemServicoId { get; set; }
        public int ServicoId { get; set; }

        // ValorUnitario e PercentualImpostoAplicado sao gravados no momento
        // da inclusao do item; alterar o servico depois nao altera OS antigas.
        public string ServicoNome { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal PercentualImpostoAplicado { get; set; }
        public decimal ValorTotalItem { get; set; }

        public void RecalcularTotal()
        {
            decimal subtotal = Quantidade * ValorUnitario;
            decimal imposto = subtotal * (PercentualImpostoAplicado / 100m);
            ValorTotalItem = subtotal + imposto;
        }

        public void Validar()
        {
            if (Quantidade <= 0)
                throw new DominioException("A quantidade do item deve ser maior que zero.");

            if (ValorUnitario <= 0)
                throw new DominioException("O valor unitario deve ser maior que zero.");

            if (PercentualImpostoAplicado < 0 || PercentualImpostoAplicado > 100)
                throw new DominioException("Percentual de imposto fora do intervalo permitido.");
        }
    }
}
