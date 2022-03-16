using System;
using tabuleiro;

namespace xadrez
{
    class PosicaoXadrez
    {
        public int Linha { get; set; }
        public char Coluna { get; set; }
        public PosicaoXadrez(int linha, char coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public Posicao toPosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }

        public override string ToString()
        {
            return "" + Coluna + Linha;
        }

    }
}
