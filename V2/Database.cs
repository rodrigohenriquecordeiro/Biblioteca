using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Biblioteca.DTO;

namespace Biblioteca
{
    internal class Database
    {
        private const string _strCon = @"Data Source=RODRIGO;Initial Catalog=BIBLIOTECA;Integrated Security=True";
        private string vsql = string.Empty;
        SqlConnection objCon = null;
        DTOEstante estante = new DTOEstante();

        private bool Conectar()
        {
            objCon = new SqlConnection(_strCon);
            try
            {
                objCon.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool Desconectar()
        {
            if (objCon.State != ConnectionState.Closed)
            {
                objCon.Close();
                objCon.Dispose();
                return true;
            }
            else
            {
                objCon.Dispose();
                return false;
            }
        }

        public bool Insert(ArrayList arrayList)
        {
            vsql = @"INSERT INTO ESTANTE (LIVRO, AUTOR, EDITORA, ANODEPUBLICACAO, NUMERODEPAGINAS, CLASSIFICACAO,
                        DATADEAQUISICAO, OBSERVACAO) 
                     VALUES (@LIVRO, @AUTOR, @EDITORA, @ANODEPUBLICACAO, @NUMERODEPAGINAS, @CLASSIFICACAO,
                        @DATADEAQUISICAO, @OBSERVACAO)";

            SqlCommand cmd = null;

            if (Conectar())
            {
                try
                {
                    cmd = new SqlCommand(vsql, objCon);
                    cmd.Parameters.Add(new SqlParameter("@LIVRO", arrayList[0]));
                    cmd.Parameters.Add(new SqlParameter("@AUTOR", arrayList[1]));
                    cmd.Parameters.Add(new SqlParameter("@EDITORA", arrayList[2]));
                    cmd.Parameters.Add(new SqlParameter("@ANODEPUBLICACAO", arrayList[3]));
                    cmd.Parameters.Add(new SqlParameter("@NUMERODEPAGINAS", arrayList[4]));
                    cmd.Parameters.Add(new SqlParameter("@CLASSIFICACAO", arrayList[5]));
                    cmd.Parameters.Add(new SqlParameter("@DATADEAQUISICAO", arrayList[6]));
                    cmd.Parameters.Add(new SqlParameter("@OBSERVACAO", arrayList[7]));
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException sqlerr)
                {
                    throw sqlerr;
                }
                finally { Desconectar(); }
            }
            else
            {
                return false;
            }
        }

        public bool Update(ArrayList arrayList)
        {
            vsql = @"UPDATE ESTANTE SET LIVRO = @LIVRO, AUTOR = @AUTOR, EDITORA = @EDITORA, ANODEPUBLICACAO = @ANODEPUBLICACAO, 
                                        NUMERODEPAGINAS = @NUMERODEPAGINAS, CLASSIFICACAO = CLASSIFICACAO, DATADEAQUISICAO = @DATADEAQUISICAO, 
                                        OBSERVACAO = @OBSERVACAO
                     WHERE CODLIVRO = @CODLIVRO";

            SqlCommand cmd = null;

            if (Conectar())
            {
                try
                {
                    cmd = new SqlCommand(vsql, objCon);
                    cmd.Parameters.Add(new SqlParameter("@CODLIVRO", arrayList[0]));
                    cmd.Parameters.Add(new SqlParameter("@LIVRO", arrayList[1]));
                    cmd.Parameters.Add(new SqlParameter("@AUTOR", arrayList[2]));
                    cmd.Parameters.Add(new SqlParameter("@EDITORA", arrayList[3]));
                    cmd.Parameters.Add(new SqlParameter("@ANODEPUBLICACAO", arrayList[4]));
                    cmd.Parameters.Add(new SqlParameter("@NUMERODEPAGINAS", arrayList[5]));
                    cmd.Parameters.Add(new SqlParameter("@CLASSIFICACAO", arrayList[6]));
                    cmd.Parameters.Add(new SqlParameter("@DATADEAQUISICAO", arrayList[7]));
                    cmd.Parameters.Add(new SqlParameter("@OBSERVACAO", arrayList[8]));
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException sqlerr)
                {
                    throw sqlerr;
                }
                finally { Desconectar(); }
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int codLivro)
        {
            vsql = @"DELETE FROM ESTANTE WHERE CODLIVRO = @CODLIVRO";

            SqlCommand cmd = null;

            if (Conectar())
            {
                try
                {
                    cmd = new SqlCommand(vsql, objCon);
                    cmd.Parameters.AddWithValue("@CODLIVRO", codLivro);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException sqlerr)
                {
                    throw sqlerr;
                }
                finally { Desconectar(); }
            }
            else
            {
                return false;
            }
        }

        public DataTable ListaGrid()
        {
            vsql = @"SELECT CODLIVRO as [Código do Livro], LIVRO as Livro, AUTOR as Autor, EDITORA as Editora, ANODEPUBLICACAO as [Ano de Publicação] 
                        FROM ESTANTE ";

            SqlCommand cmd = null;

            if (Conectar())
            {
                try
                {
                    cmd = new SqlCommand(vsql, objCon);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    return dt;
                }
                catch (SqlException sqlerr)
                {
                    throw sqlerr;
                }
                finally { Desconectar(); }
            }
            else
            {
                return null;
            }
        }

        public DataTable Pesquisar(string sql, string param)
        {
            vsql = sql;

            SqlCommand cmd = null;

            if (Conectar())
            {
                try
                {
                    cmd = new SqlCommand(vsql, objCon);
                    cmd.Parameters.Add(new SqlParameter("@VALOR", param));
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    return dt;
                }
                catch (SqlException sqlerr)
                {
                    throw sqlerr;
                }
                finally { Desconectar(); }
            }
            else
            {
                return null;
            }
        }
    }
}
