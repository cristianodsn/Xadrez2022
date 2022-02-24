using System;
using tabuleiro;

namespace tabuleiro
{
    class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; set; }
        public int quantidadeMovimento { get; protected set; }

        public Peca(Cor cor)
        {
            this.posicao = null;
            this.cor = cor;
            quantidadeMovimento = 0;
        }
    }
}
