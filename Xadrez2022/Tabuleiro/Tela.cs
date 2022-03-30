using System;
using tabuleiro;
using xadrez;

namespace tabuleiro
{
    class Tela
    {
        public static void imprimirTabuleito(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i);
                for (int j = 0; j < tab.Colunas; j++)
                {
                    imprimirPeca(tab.peca(i,j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void imprimirTabuleito(Tabuleiro tab, bool [,] mat)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i);
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (mat[i, j])
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }
                    imprimirPeca(tab.peca(i, j));
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        static public void imprimirPeca(Peca peca)
        {

            if (peca == null)
            {
                Console.Write(" _");
            }
            else
            {
                if (peca.cor == Cor.Branca)
                {
                    Console.Write(" " + peca);
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
}
