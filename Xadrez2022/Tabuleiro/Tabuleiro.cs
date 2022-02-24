using System;
using tabuleiro;

namespace tabuleiro
{
    class Tabuleiro
    {
        public int Linhas { get; protected set; }
        public int Colunas { get; protected set; }
        private Peca[,] pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            this.pecas = new Peca[linhas, colunas];
        }

        public Peca peca(int linha, int coluna)
        {
            return pecas[linha, coluna];
        }
    }
}
