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
                Console.Write(8 - i);
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if(tab.peca(i,j) == null)
                    {
                        Console.Write(" _");
                    }
                    else
                    {
                        imprimirPeca(tab.peca(i, j));
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        static public void imprimirPeca(Peca peca)
        {
            if(peca.cor == Cor.Branca)
            {
                Console.Write(peca);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" " + peca);
                Console.ResetColor();
            }
        }
    }
}
