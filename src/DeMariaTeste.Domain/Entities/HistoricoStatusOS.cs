using System;
using DeMariaTeste.Domain.Enums;

namespace DeMariaTeste.Domain.Entities
{
    public class HistoricoStatusOS
    {
        public int Id { get; set; }
        public int OrdemServicoId { get; set; }
        public StatusOrdemServico? StatusAnterior { get; set; }
        public StatusOrdemServico StatusNovo { get; set; }
        public DateTime DataHora { get; set; }
        public string Usuario { get; set; }

        public HistoricoStatusOS()
        {
            DataHora = DateTime.Now;
        }
    }
}
