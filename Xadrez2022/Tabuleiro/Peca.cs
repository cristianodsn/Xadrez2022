using System;
using tabuleiro;

namespace tabuleiro
{
    abstract class Peca
    {
        public Tabuleiro tab { get; set; }
        public Posicao posicao { get; set; }
        public Cor cor { get; set; }
        public int quantidadeMovimento { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            this.tab = tab;
            this.posicao = null;
            this.cor = cor;

            quantidadeMovimento = 0;
        }

        public void incrementarQuantidadeMovimento()
        {
            quantidadeMovimento++;
        }

        public abstract bool[,] movimentosPossiveis();

    }
}
