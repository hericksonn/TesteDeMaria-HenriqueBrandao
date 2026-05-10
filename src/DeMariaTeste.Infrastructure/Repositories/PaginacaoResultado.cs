using System.Collections.Generic;

namespace DeMariaTeste.Infrastructure.Repositories
{
    public class PaginacaoResultado<T>
    {
        public IList<T> Itens { get; set; }
        public int TotalRegistros { get; set; }
        public int Pagina { get; set; }
        public int TamanhoPagina { get; set; }

        public PaginacaoResultado()
        {
            Itens = new List<T>();
        }

        public int TotalPaginas
        {
            get
            {
                if (TamanhoPagina <= 0) return 0;
                int p = TotalRegistros / TamanhoPagina;
                if (TotalRegistros % TamanhoPagina != 0) p++;
                return p;
            }
        }
    }
}
