using System;
using System.Collections.Generic;

namespace DIO.Bank
{
    class Program
    {
        static List<Conta> listContas = new List<Conta>();
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
                            ListarContas();
                            break;
                        case "2":
                            CadastrarConta();
                            break;
                        case "3":
                            Transferir();
                            break;
                        case "4":
                            Sacar();
                            break;
                        case "5":
                            Depositar();
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

        private static void Depositar()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("*********************************");
                Console.WriteLine("** Depositar                   **");
                Console.WriteLine("*********************************\n");

                if (!ValidaContasCadastradas()) return;

                int idConta = -1;
                bool result = false;
                do
                {
                    Console.Write("Digite o número da conta: ");
                    result = int.TryParse(Console.ReadLine(), out idConta);
                    if (result && idConta > listContas.Count - 1)
                    {
                        Console.WriteLine("conta não localizada.\nPressione qualquer tecla para informar outra conta ou S para sair");
                        if (Console.ReadLine().ToUpper().Equals("S"))
                            return;

                        result = false;
                    }
                } while (!result);

                double valorDeposito = 0;
                do
                {
                    Console.Write("Digite o valor a ser depositado: ");
                    result = double.TryParse(Console.ReadLine(), out valorDeposito);
                } while (!result);

                listContas[idConta].Depositar(valorDeposito);

                MensagemSucesso();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void Sacar()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("*********************************");
                Console.WriteLine("** Sacar                       **");
                Console.WriteLine("*********************************\n");

                if (!ValidaContasCadastradas()) return;

                int idConta = -1;
                bool result = false;
                do
                {
                    Console.Write("Digite o número da conta: ");
                    result = int.TryParse(Console.ReadLine(), out idConta);
                    if (result && idConta > listContas.Count - 1)
                    {
                        Console.WriteLine("conta não localizada.\nPressione qualquer tecla para informar outra conta ou S para sair");
                        if (Console.ReadLine().ToUpper().Equals("S"))
                            return;

                        result = false;
                    }
                } while (!result);

                double valorSaque = 0;
                do
                {
                    Console.Write("Digite o valor a ser sacado: ");
                    result = double.TryParse(Console.ReadLine(), out valorSaque);
                } while (!result);

                listContas[idConta].Sacar(valorSaque);

                MensagemSucesso();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void Transferir()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("*********************************");
                Console.WriteLine("** Transferir                  **");
                Console.WriteLine("*********************************\n");

                if (!ValidaContasCadastradas()) return;
                int idContaOrigem = -1;
                bool result = false;
                do
                {
                    Console.Write("Digite o número da conta origem: ");
                    result = int.TryParse(Console.ReadLine(), out idContaOrigem);
                    if (result && idContaOrigem > listContas.Count - 1)
                    {
                        Console.WriteLine("conta não localizada.\nPressione qualquer tecla para informar outra conta ou S para sair");
                        if (Console.ReadLine().ToUpper().Equals("S"))
                            return;

                        result = false;
                    }
                } while (!result);
                int idContaDestino = -1;
                do
                {
                    Console.Write("Digite o número da conta destino: ");
                    result = int.TryParse(Console.ReadLine(), out idContaDestino);
                    if (result && idContaDestino > listContas.Count - 1)
                    {
                        Console.WriteLine("conta não localizada.\nPressione qualquer tecla para informar outra conta ou S para sair");
                        if (Console.ReadLine().ToUpper().Equals("S"))
                            return;

                        result = false;
                    }
                } while (!result);

                double valorTransferencia = 0;
                do
                {
                    Console.Write("Digite o valor a ser transferido: ");
                    result = double.TryParse(Console.ReadLine(), out valorTransferencia);
                } while (!result);

                listContas[idContaOrigem].Transferir(valorTransferencia, listContas[idContaDestino]);

                MensagemSucesso();

            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void ListarContas()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("*********************************");
                Console.WriteLine("** Listar Contas               **");
                Console.WriteLine("*********************************\n");

                if (!ValidaContasCadastradas()) return;

                for (int i = 0; i < listContas.Count; i++)
                {
                    Conta conta = listContas[i];
                    Console.WriteLine($"#{i} - {conta.ToString()}");
                }

                MensagemSucesso();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private static void CadastrarConta()
        {
            try
            {
                bool result = false;
                Console.Clear();
                Console.WriteLine("*********************************");
                Console.WriteLine("** Cadastrar Conta             **");
                Console.WriteLine("*********************************\n");

                int entradaTipoConta = 0;
                do
                {
                    Console.Write("Digite 1 para conta Fisica ou 2 para conta Jurídica: ");
                    result = int.TryParse(Console.ReadLine(), out entradaTipoConta);

                } while (!result || !(entradaTipoConta == 1 || entradaTipoConta == 2));


                Console.Write("Digite o nome do Cliente: ");
                string entradaNome = Console.ReadLine();

                double entradaCredito = 0;
                do
                {
                    Console.Write("Digite o saldo inicial: ");
                    result = double.TryParse(Console.ReadLine(), out entradaCredito);
                } while (!result);

                double entradaSaldo = 0;
                do
                {
                    Console.Write("Digite o crédito: ");
                    result = double.TryParse(Console.ReadLine(), out entradaSaldo);
                } while (!result);


                Conta novaConta = new Conta(tipoConta: (TipoConta)entradaTipoConta,
                saldo: entradaSaldo, credito: entradaCredito, nome: entradaNome);

                listContas.Add(novaConta);
                MensagemSucesso();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string ObterOpcaoUsuario()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("*********************************");
                Console.WriteLine("** Seja Bem Vindo ao DIO.Bank  **");
                Console.WriteLine("*********************************");
                Console.WriteLine("** Informe a opção desejada:   **");
                Console.WriteLine("**                             **");
                Console.WriteLine("** 1 - Listar Contas           **");
                Console.WriteLine("** 2 - Inserir Nova Conta      **");
                Console.WriteLine("** 3 - Transferir              **");
                Console.WriteLine("** 4 - Sacar                   **");
                Console.WriteLine("** 5 - Depositar               **");
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
        private static void MensagemFinal()
        {
            Console.Clear();
            Console.WriteLine("*******************************************");
            Console.WriteLine("** Obrigado por utilizar nossos serviços **");
            Console.WriteLine("** Pressione qualquer tecla para voltar. **");
            Console.WriteLine("*******************************************");
            Console.ReadLine();
        }

        private static void MensagemSucesso()
        {
            Console.WriteLine("\n*******************************************");
            Console.WriteLine("** Operação Concluída com Sucesso.       **");
            Console.WriteLine("** Pressione qualquer tecla para voltar. **");
            Console.WriteLine("*******************************************");
            Console.ReadLine();
        }

        private static bool ValidaContasCadastradas()
        {
            if (listContas.Count == 0)
            {
                Console.WriteLine("\nNão existe contas cadastradas.\nPressione qualquer tecla para continuar");
                Console.ReadLine();
                return false;
            }
            return true;
        }
    }
}
