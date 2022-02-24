using System;
using tabuleiro;

namespace tabuleiro
{
    class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; set; }
        public int quantidadeMovimento { get; protected set; }

        public Peca(Posicao posicao, Cor cor)
        {
            this.posicao = posicao;
            this.cor = cor;
            quantidadeMovimento = 0;
        }
    }
}
