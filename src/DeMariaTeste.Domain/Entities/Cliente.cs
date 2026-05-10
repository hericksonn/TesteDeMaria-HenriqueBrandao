using System;
using DeMariaTeste.Domain.Enums;
using DeMariaTeste.Domain.Exceptions;

namespace DeMariaTeste.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public TipoCliente Tipo { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }

        public Cliente()
        {
            Ativo = true;
            DataCadastro = DateTime.Now;
        }

        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                throw new DominioException("O nome do cliente e obrigatorio.");

            if (string.IsNullOrWhiteSpace(Documento))
                throw new DominioException("O documento do cliente e obrigatorio.");

            string doc = Documento.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "");

            if (Tipo == TipoCliente.Fisica && doc.Length != 11)
                throw new DominioException("CPF deve possuir 11 digitos.");

            if (Tipo == TipoCliente.Juridica && doc.Length != 14)
                throw new DominioException("CNPJ deve possuir 14 digitos.");

            Documento = doc;
        }
    }
}
