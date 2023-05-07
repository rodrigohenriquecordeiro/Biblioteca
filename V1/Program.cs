using System;
using System.Threading;

namespace Biblioteca
{
    class Program
    {
        static void Main(string[] args)
        {
            bool primeiraEscolha = true;
            while (primeiraEscolha)
            {
                CabecalhoBiblioteca();
                Console.WriteLine("[1] Livros na Estante \n[2] Cadastrar \n[3] Editar \n[4] Apagar \n[5] Sair");
                RodapeBiblioteca();

                Console.Write("\nSua escolha: "); int escolha = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (escolha)
                {
                    case 1: 
                        SelecionarLivrosCadastrados();
                        break;
                    case 2: 
                        CadastrarLivro();
                        break;
                    case 3: 
                        AlterarDadosDaEstante();
                        break;
                    case 4: 
                        ApagaRegistro();
                        break;
                    case 5: 
                        primeiraEscolha = false;
                        Console.WriteLine("Saindo...");
                        Thread.Sleep(500);
                        break;
                }
            }
        }

        public static void CabecalhoBiblioteca()
        {
            Console.WriteLine("*********************************\n");
            string nomeDaBiblioteca = ">=====> Biblioteca de RHC <=====<";
            Console.WriteLine($"{nomeDaBiblioteca,25}\n");
        }

        public static void RodapeBiblioteca()
        {
            Console.WriteLine("\n*********************************");
        }

        public static void CadastrarLivro()
        {
            while (true)
            {
                CabecalhoBiblioteca();
                Console.WriteLine("Cadastrar livro\n");

                Console.Write("Livro: "); string livro = Console.ReadLine().Trim().ToUpper();
                Console.Write("Autor: "); string autor = Console.ReadLine().Trim().ToUpper();
                Console.Write("Editora: "); string editora = Console.ReadLine().Trim().ToUpper();
                Console.Write("Ano de Publicação: "); string anoDePublicacao = Console.ReadLine().Trim().ToUpper();
                Console.Write("Número de Páginas: "); int numeroDePagaina = int.Parse(Console.ReadLine().Trim());
                Console.Write("Classificação Catalográfica: "); string classificacao = Console.ReadLine().Trim().ToUpper();
                Console.Write("Data de Aquisição: "); DateTime dataDeAquisicao = Convert.ToDateTime(Console.ReadLine().Trim().ToUpper());
                Console.Write("Observação: "); string observacao = Console.ReadLine().Trim().ToUpper();

                Estante estante = new Estante()
                {
                    Livro = livro,
                    Autor = autor,
                    Editora = editora,
                    AnoDePublicacao = anoDePublicacao,
                    NumeroDePagina = numeroDePagaina,
                    Classificacao = classificacao,
                    DataDeAquisicao = dataDeAquisicao,
                    Observacao = observacao
                };

                estante.ExecutaQueryInsercao();
                Console.Clear(); 
                Console.WriteLine($"\nLivro {livro} cadastrado com sucesso!");

                RodapeBiblioteca();
                Console.WriteLine("\n[1] Voltar \n[2] Executar outro");

                Console.Write("\nSua escolha: "); int escolhaDePagina = int.Parse(Console.ReadLine());
                Console.Clear();
                if (escolhaDePagina == 1) break;
                if (escolhaDePagina == 2) Thread.Sleep(500); Console.Clear(); continue;
            }
        }

        public static void SelecionarLivrosCadastrados()
        {
            while (true)
            {
                CabecalhoBiblioteca();
                Console.WriteLine("RELATÓRIO SIMPLES");
                Console.WriteLine($"\nCodLivro   {"Livro",-50}  Autor");

                Estante estante = new Estante();
                estante.ExecutaQuerySelect();

                Console.WriteLine();
                RodapeBiblioteca();
                Console.WriteLine("\n[1] Voltar \n[2] Relatório Simples \n[3] Relatório Completo");

                Console.Write("\nSua escolha: "); int escolhaDePagina = int.Parse(Console.ReadLine());
                Console.Clear();
                if (escolhaDePagina == 1) break;
                if (escolhaDePagina == 2) Thread.Sleep(500); Console.Clear(); continue;
            }
        }

        public static void AlterarDadosDaEstante()
        {
            while (true)
            {
                CabecalhoBiblioteca();
                Console.WriteLine("EDITAR CADASTRO DE LIVROS");

                Console.WriteLine("\nTodas as colunas da tabela Estante\n");
                Estante estante = new Estante();
                Console.WriteLine($"Num    {"Coluna",-20}");
                estante.SelecionaTodasColunasDaEstante();

                Console.Write("\nQual o Número da coluna que deseja alterar: ");
                string escolhaCodigoColuna = "";

                switch (Console.ReadLine().Trim().PadLeft(2, '0'))
                {
                    case "01":
                        escolhaCodigoColuna = "CODLIVRO";
                        break;
                    case "02":
                        escolhaCodigoColuna = "LIVRO";
                        break;
                    case "03":
                        escolhaCodigoColuna = "AUTOR";
                        break;
                    case "04":
                        escolhaCodigoColuna = "EDITORA";
                        break;
                    case "05":
                        escolhaCodigoColuna = "ANODEPUBLICACAO";
                        break;
                    case "06":
                        escolhaCodigoColuna = "NUMERODEPAGINA";
                        break;
                    case "07":
                        escolhaCodigoColuna = "CLASSIFICACAO";
                        break;
                    case "08":
                        escolhaCodigoColuna = "DATADEAQUISICAO";
                        break;
                    case "09":
                        escolhaCodigoColuna = "OBSERVACAO";
                        break;
                }
                
                Console.Write("Qual o CodLivro deseja alterar? "); 
                string escolhaDoCodLivro = Console.ReadLine().Trim();

                Console.Write($"Qual a nova informação a ser salva? "); 
                string novaInformacao = Console.ReadLine().Trim();

                if (escolhaCodigoColuna == "CODLIVRO")
                {
                    Console.WriteLine("\nNão é possível alterar CodLivro!");
                    Thread.Sleep(2000); Console.Clear();
                    break;
                }
                else if (escolhaCodigoColuna == "NUMERODEPAGINA") int.Parse(novaInformacao);
                else if (escolhaCodigoColuna == "DATADEAQUISICAO") Convert.ToDateTime(novaInformacao);
                else novaInformacao = novaInformacao.ToUpper();

                estante.ExecutaQueryUpdate(escolhaCodigoColuna, escolhaDoCodLivro, novaInformacao);
                Console.WriteLine("\nAlterado com sucesso!");

                Console.WriteLine();
                RodapeBiblioteca();
                Console.WriteLine("\n[1] Voltar \n[2] Outra alteração");

                Console.Write("\nSua escolha: "); int escolhaDePagina = int.Parse(Console.ReadLine());
                Console.Clear();
                if (escolhaDePagina == 1) break;
                if (escolhaDePagina == 2) Thread.Sleep(500); Console.Clear(); continue;
            }
        }

        public static void ApagaRegistro()
        {
            while (true)
            {
                CabecalhoBiblioteca();
                Console.WriteLine("APAGAR REGISTRO\n");

                Console.Write($"Digite o CodLivro do registro que deseja apagar da Estante: "); 
                string escolha = Console.ReadLine().Trim();

                Estante estante = new Estante();
                estante.ExecutarQueryDelete(int.Parse(escolha));
                Console.WriteLine("\nRegistro apagado com sucesso!");

                Console.WriteLine();
                RodapeBiblioteca();
                Console.WriteLine("\n[1] Voltar \n[2] Apagar outro registro");

                Console.Write("\nSua escolha: "); int escolhaDePagina = int.Parse(Console.ReadLine());
                Console.Clear();
                if (escolhaDePagina == 1) break;
                if (escolhaDePagina == 2) Thread.Sleep(500); Console.Clear(); continue;
            }
        }
    }
}
