using System;
using System.Collections.Generic;
using DeMariaTeste.Domain.Enums;
using DeMariaTeste.Domain.Exceptions;

namespace DeMariaTeste.Domain.Entities
{
    public class OrdemServico
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        // ClienteNome e populado em listagens via JOIN, nao persiste em ordens_servico.
        public string ClienteNome { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataConclusao { get; set; }
        public StatusOrdemServico Status { get; set; }
        public string Observacao { get; set; }
        public decimal ValorTotal { get; set; }

        // Coluna usada para concorrencia otimista no UPDATE.
        public int Versao { get; set; }

        public List<ItemOrdemServico> Itens { get; set; }

        public OrdemServico()
        {
            DataAbertura = DateTime.Now;
            Status = StatusOrdemServico.Aberta;
            Versao = 1;
            Itens = new List<ItemOrdemServico>();
        }

        public void Validar()
        {
            if (ClienteId <= 0)
                throw new DominioException("Selecione um cliente para a OS.");

            if (Itens == null || Itens.Count == 0)
                throw new DominioException("A OS precisa de ao menos um item.");

            foreach (var item in Itens)
                item.Validar();
        }

        public void RecalcularValorTotal()
        {
            decimal total = 0m;
            if (Itens != null)
            {
                foreach (var item in Itens)
                {
                    item.RecalcularTotal();
                    total += item.ValorTotalItem;
                }
            }
            ValorTotal = total;
        }

        public bool PodeEditar()
        {
            return Status != StatusOrdemServico.Concluida && Status != StatusOrdemServico.Cancelada;
        }

        public void GarantirEdicao()
        {
            if (!PodeEditar())
                throw new DominioException("Esta OS esta finalizada e nao pode mais ser alterada.");
        }

        public void GarantirConclusaoValida()
        {
            if (ValorTotal <= 0)
                throw new DominioException("Nao e possivel concluir uma OS com valor total zero.");
        }
    }
}
