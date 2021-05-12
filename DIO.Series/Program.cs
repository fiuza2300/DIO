using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            try
            {
                string opcaoUsuario = ObterOpcaoUsuario();
                while (!opcaoUsuario.ToUpper().Equals("S"))
                {
                    switch (opcaoUsuario)
                    {
                        case "1":
                            ListarSerie();
                            break;
                        case "2":
                            CadastrarSerie();
                            break;
                        case "3":
                            AtualizaSerie();
                            break;
                        case "4":
                            ExcluirSerie();
                            break;
                        case "5":
                            VisualizarSerie();
                            break;
                        case "L":
                            Console.Clear();
                            break;
                    }
                    Console.WriteLine();
                    opcaoUsuario = ObterOpcaoUsuario();
                }
                MensagemFinal();

            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"Ocorreu o seguinte erro: {ex.Message} Código: {ex.HResult} caminho: {ex.Source} rastreamento: {ex.StackTrace}");
            }

        }
        #region Métodos Series
        private static void AtualizaSerie()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("*********************************");
                Console.WriteLine("** Atualizar Série             **");
                Console.WriteLine("*********************************\n");

                if (!ValidaSeriesCadastradas()) return;

                Console.Write("Digite o id da série: ");
                int entradaId = int.Parse(Console.ReadLine());

                var serie = CriaSerie(entradaId);
                repositorio.Atualiza(entradaId, serie);

                MensagemSucesso();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void ExcluirSerie()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("*********************************");
                Console.WriteLine("** Excluir Serie               **");
                Console.WriteLine("*********************************\n");

                if (!ValidaSeriesCadastradas()) return;

                Console.Write("Digite o id da série: ");
                int entradaId = int.Parse(Console.ReadLine());

                repositorio.Exclui(entradaId);

                MensagemSucesso();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void VisualizarSerie()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("*********************************");
                Console.WriteLine("** Visualizar Serie            **");
                Console.WriteLine("*********************************\n");

                if (!ValidaSeriesCadastradas()) return;
                Console.Write("Digite o id da série: ");
                int entradaId = int.Parse(Console.ReadLine());

                var serie = repositorio.RetornoPorId(entradaId);
                Console.WriteLine(serie.ToString());

                MensagemSucesso();

            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void ListarSerie()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("*********************************");
                Console.WriteLine("** Listar Series               **");
                Console.WriteLine("*********************************\n");

                if (!ValidaSeriesCadastradas()) return;

                foreach (var serie in repositorio.Lista())
                {
                    var excluido = serie.RetornaExcluido() ? "*Excluído" : "";
                    Console.WriteLine($"#{serie.RetornaId()} - {serie.RetornaTitulo()} - {excluido}");
                }
                MensagemSucesso();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private static void CadastrarSerie()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("*********************************");
                Console.WriteLine("** Cadastrar Serie             **");
                Console.WriteLine("*********************************\n");

                var novaSerie = CriaSerie(repositorio.ProximoId());

                repositorio.Insere(novaSerie);
                MensagemSucesso();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static Serie CriaSerie(int id)
        {
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine($"{i} - {Enum.GetName(typeof(Genero), i)}");
            }

            Console.Write("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Write("Digite o título da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o ano da série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a descrição da série: ");
            string entradaDescricao = Console.ReadLine();

            return new Serie(
                    id: id,
                    genero: (Genero)entradaGenero,
                    titulo: entradaTitulo,
                    ano: entradaAno,
                    descricao: entradaDescricao);

        }

        #endregion

        #region Métodos Controeles Visuais
        private static void MensagemFinal()
        {
            Console.Clear();
            Console.WriteLine("*******************************************");
            Console.WriteLine("** Obrigado por utilizar nossos serviços **");
            Console.WriteLine("** Pressione qualquer tecla para voltar. **");
            Console.WriteLine("*******************************************");
            Console.ReadLine();
        }
        private static string ObterOpcaoUsuario()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("*********************************");
                Console.WriteLine("** Seja Bem Vindo ao DIO.Serie **");
                Console.WriteLine("*********************************");
                Console.WriteLine("** Informe a opção desejada:   **");
                Console.WriteLine("**                             **");
                Console.WriteLine("** 1 - Listar Séries           **");
                Console.WriteLine("** 2 - Inserir Nova Série      **");
                Console.WriteLine("** 3 - Atualizar Série         **");
                Console.WriteLine("** 4 - Excluir Série           **");
                Console.WriteLine("** 5 - Visualizar Série        **");
                Console.WriteLine("** L - Limpar Tela             **");
                Console.WriteLine("** S - Sair                    **");
                Console.WriteLine("*********************************\n");

                return Console.ReadLine().ToUpper();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private static void MensagemSucesso()
        {
            Console.WriteLine("\n*******************************************");
            Console.WriteLine("** Operação Concluída com Sucesso.       **");
            Console.WriteLine("** Pressione qualquer tecla para voltar. **");
            Console.WriteLine("*******************************************");
            Console.ReadLine();
        }
        private static bool ValidaSeriesCadastradas()
        {
            if (repositorio.Lista().Count == 0)
            {
                Console.WriteLine("\nNão existe series cadastradas.\nPressione qualquer tecla para continuar");
                Console.ReadLine();
                return false;
            }
            return true;
        }
        #endregion
    }
}
