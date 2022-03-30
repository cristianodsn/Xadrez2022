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
                    Console.Clear();
                    Tela.imprimirTabuleito(partida.tabuleiro);
                    Console.Write("Origem: ");

                    Posicao origem = partida.lerPosicaoXadrez().toPosicao();
                    bool[,] aux = partida.tabuleiro.peca(origem).movimentosPossiveis();
                    Console.Clear();
                    Tela.imprimirTabuleito(partida.tabuleiro, aux);

                    Console.Write("Destino: ");
                    Posicao destino = partida.lerPosicaoXadrez().toPosicao();
                    partida.executarMovimento(origem, destino);
                }
            }

            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
