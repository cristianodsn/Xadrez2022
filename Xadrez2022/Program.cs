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
                    Console.WriteLine("Origem: ");
                    Posicao origem = partida.lerPosicaoXadrez().toPosicao();
                    Console.WriteLine(origem);
                    Console.WriteLine("Destino: ");
                    Posicao destino = partida.lerPosicaoXadrez().toPosicao();
                    Console.WriteLine(destino);
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
