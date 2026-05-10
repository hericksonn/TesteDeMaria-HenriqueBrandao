using System;
using DeMariaTeste.Domain.Enums;

namespace DeMariaTeste.Domain.Entities
{
    public class Auditoria
    {
        public long Id { get; set; }
        public string Entidade { get; set; }
        public string IdRegistro { get; set; }
        public OperacaoAuditoria Operacao { get; set; }
        public DateTime DataHora { get; set; }
        public string Usuario { get; set; }

        // Texto JSON. A coluna no banco e jsonb; o cast e feito pelo Npgsql.
        public string SnapshotAntes { get; set; }
        public string SnapshotDepois { get; set; }

        public Auditoria()
        {
            DataHora = DateTime.Now;
        }
    }
}
