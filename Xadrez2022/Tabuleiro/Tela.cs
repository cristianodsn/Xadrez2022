using System;
using tabuleiro;

namespace tabuleiro
{
    class Tela
    {
        public static void imprimirTabuleito(Tabuleiro tab)
        {
            for(int i = 0; i < tab.Linhas; i++)
            {
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if(tab.peca(i,j) == null)
                    {
                        Console.Write(" _");
                    }
                    else
                    {
                        Console.Write(" " + tab.peca(i,j));
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
