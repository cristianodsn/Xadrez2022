using System;
using System.Collections.Generic;
using tabuleiro;
using xadrez;

namespace tabuleiro
{
    class Tela
    {

        public static void imprimirPartida(PartidaXadrez partida)
        {
            Console.Clear();
            imprimirTabuleito(partida.tabuleiro);
            Console.WriteLine();
            imprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno atual: " + partida.turno);

            if (!partida.fimDeJogo)
            {
                Console.WriteLine("Aguardando Jogada: " + partida.JogadorAtual);
                if (partida.xeque)
                {
                    Console.WriteLine("XEQUE!");
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("XEQUEMATE!!!");
                Console.WriteLine("Vencedor: " + partida.JogadorAtual);
            }
            Console.WriteLine();          
        }

        static void imprimirPecasCapturadas(PartidaXadrez partida)
        {
            Console.WriteLine("Peças capturadas: ");

            Console.Write("Brancas ");
            HashSet<Peca> aux1 = partida.pecasCapturadas(Cor.Branca);                                                     
            imprimirConjunto(aux1);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Pretas  ");
            HashSet<Peca> aux2 = partida.pecasCapturadas(Cor.Preta);
            imprimirConjunto(aux2);
            Console.ResetColor();
            Console.WriteLine();
        }

        static void imprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[ ");
            foreach (Peca p in conjunto)
            {
                Console.Write(p + " ");
            }
            Console.Write("]");
        }

        public static void imprimirTabuleito(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i);
                for (int j = 0; j < tab.Colunas; j++)
                {
                    imprimirPeca(tab.peca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void imprimirTabuleito(Tabuleiro tab, bool[,] mat)
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
