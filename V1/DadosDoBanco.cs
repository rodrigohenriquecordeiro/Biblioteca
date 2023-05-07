using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    class DadosDoBanco
    {
        public static string StringDeConexao()
        {
            return @"Server = RODRIGO; Database = BIBLIOTECA; Trusted_Connection = True;";
        }
    }
}
