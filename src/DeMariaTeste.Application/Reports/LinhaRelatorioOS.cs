using System;

namespace DeMariaTeste.Application.Reports
{
    public class LinhaRelatorioOS
    {
        public int OrdemServicoId { get; set; }
        public string Cliente { get; set; }
        public string Documento { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataConclusao { get; set; }
        public string Status { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorImposto { get; set; }
    }
}
