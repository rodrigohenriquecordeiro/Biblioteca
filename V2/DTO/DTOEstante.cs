using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.DTO
{
    public class DTOEstante
    {
        public int CodLivro { get; set; }
        public string Livro { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public string AnoDePublicacao { get; set; }
        public int NumeroDePaginas { get; set; }
        public string Classificacao { get; set; }
        public DateTime DataDeAquisicao { get; set; }
        public string Observacao { get; set; }
    }
}
