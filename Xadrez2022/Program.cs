using System;
using tabuleiro;
namespace Xadrez2022
{
    class Program
    {
        static void Main(string[] args)
        {

            Tabuleiro tab = new Tabuleiro(8,8);
            Tela.imprimirTabuleito(tab);
        }
    }
}
