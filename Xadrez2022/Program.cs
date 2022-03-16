using System;
using tabuleiro;
using xadrez;
namespace Xadrez2022
{
    class Program
    {
        static void Main(string[] args)
        {
            PosicaoXadrez posicaoXadrez = new PosicaoXadrez(1, 'b');
            Console.WriteLine(posicaoXadrez.toPosicao());
            Console.WriteLine(posicaoXadrez);
        }
    }
}
