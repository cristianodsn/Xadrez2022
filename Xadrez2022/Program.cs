using System;
using tabuleiro;
using xadrez;
namespace Xadrez2022
{
    class Program
    {
        static void Main(string[] args)
        {

            Tabuleiro tab = new Tabuleiro(8,8);
            tab.colocarPeca(new Torre(Cor.Preta), new Posicao(0, 0));
            tab.colocarPeca(new Torre(Cor.Preta), new Posicao(1, 3));
            tab.colocarPeca(new Rei(Cor.Preta), new Posicao(2, 4));

            Tela.imprimirTabuleito(tab);
        }
    }
}
