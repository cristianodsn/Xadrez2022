using System;
using tabuleiro;
using xadrez;
namespace Xadrez2022
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaXadrez partida = new PartidaXadrez();
                while (!partida.fimDeJogo)
                {
                    try
                    {
                        Tela.imprimirPartida(partida);

                        Console.Write("Origem: ");
                        Posicao origem = partida.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoOrigem(origem);
                        bool[,] aux = partida.tabuleiro.peca(origem).movimentosPossiveis();
                        Console.Clear();
                        Tela.imprimirTabuleito(partida.tabuleiro, aux);

                        Console.Write("Destino: ");
                        Posicao destino = partida.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDestino(origem, destino);
                        partida.realizarJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                    catch (SystemException)
                    {
                        Console.WriteLine("Erro ao inserir uma nova posição!");
                        Console.ReadLine();
                    }
                    Tela.imprimirPartida(partida);
                }
            }

            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }           
        }
    }
}
