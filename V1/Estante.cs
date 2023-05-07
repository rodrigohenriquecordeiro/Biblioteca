using System;
using System.Data.SqlClient;

namespace Biblioteca
{
    class Estante : DadosDoBanco
    {
        public int CodLivro { get; set; }
        public string Livro { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public string AnoDePublicacao { get; set; }
        public int NumeroDePagina { get; set; }
        public string Classificacao { get; set; }
        public DateTime DataDeAquisicao { get; set; }
        public string Observacao { get; set; }
        
        public void ExecutaQueryInsercao()
        {
            string queryInsert = $@"INSERT INTO ESTANTE
                                    (LIVRO, AUTOR, EDITORA, ANODEPUBLICACAO, NUMERODEPAGINA, CLASSIFICACAO, DATADEAQUISICAO, OBSERVACAO)
                                    VALUES('{Livro}', '{Autor}', '{Editora}', {AnoDePublicacao}, {NumeroDePagina}, '{Classificacao}', 
                                    '{DataDeAquisicao}', '{Observacao}')";

            using (SqlConnection conn = new SqlConnection(StringDeConexao()))
            {
                SqlCommand cmd = new SqlCommand(queryInsert, conn);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Close();
            }
        }

        public void ExecutaQuerySelect()
        {
            string querySelect = $@"SELECT CODLIVRO, LIVRO, AUTOR FROM ESTANTE";

            using (SqlConnection connection = new SqlConnection(StringDeConexao()))
            {
                SqlCommand command = new SqlCommand(querySelect, connection);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                    {
                        Console.WriteLine($"  {reader[0].ToString().PadLeft(5, '0'), 6} | {reader[1], -50} | {reader[2]}");
                    }
            }
        }

        public void SelecionaTodasColunasDaEstante()
        {
            string query = $@"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Estante'";
            int num = 0;

            using (SqlConnection connection = new SqlConnection(StringDeConexao()))
            {
                SqlCommand command = new SqlCommand(query, connection);
                
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                    {
                        num++;
                        Console.WriteLine($"[{num.ToString().PadLeft(2, '0')}] | {reader[0]}");
                    }
            }
        }

        public void ExecutaQueryUpdate(string colunaASerEditada, string codLivro, string novoConteudo)
        {
            string queryUpdate = $@"UPDATE ESTANTE SET {colunaASerEditada} = '{novoConteudo}' WHERE CODLIVRO = {codLivro}";

            using (SqlConnection connection = new SqlConnection(StringDeConexao()))
            {
                SqlCommand command = new SqlCommand(queryUpdate, connection);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read()) Console.WriteLine(reader[0]);
            }
        }

        public void ExecutarQueryDelete(int codLivro)
        {
            string queryDelete =  $@"DELETE ESTANTE WHERE CODLIVRO = {codLivro}
                                     DBCC CHECKIDENT ('Estante', RESEED, {codLivro - 1});";

            using (SqlConnection connection = new SqlConnection(StringDeConexao()))
            {
                SqlCommand command = new SqlCommand(queryDelete, connection);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read()) Console.WriteLine(reader[0]);
            }
        }
    }
}
