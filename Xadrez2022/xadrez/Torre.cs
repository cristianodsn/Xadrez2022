using System;
using tabuleiro;

namespace xadrez
{
    class Torre : Peca
    {

        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor) { }

        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.Linhas, tab.Colunas];
            Posicao aux = new Posicao(0, 0);

            //acima
            aux.definirPosicao(posicao.linha - 1, posicao.coluna);
            while (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
                if (tab.peca(aux) != null && tab.peca(aux).cor != cor)
                {
                    break;
                }
                aux.linha = aux.linha - 1;
            }

            //direita
            aux.definirPosicao(posicao.linha, posicao.coluna -1);
            while (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
                if (tab.peca(aux) != null && tab.peca(aux).cor != cor)
                {
                    break;
                }
                aux.coluna = aux.coluna - 1;
            }

            //abaixo
            aux.definirPosicao(posicao.linha +1, posicao.coluna);
            while (podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
                if (tab.peca(aux) != null && tab.peca(aux).cor != cor)
                {
                    break;
                }
                aux.linha = aux.linha + 1;
            }

            //esquerda
            aux.definirPosicao(posicao.linha, posicao.coluna +1);
            while (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
                if (tab.peca(aux) != null && tab.peca(aux).cor != cor)
                {
                    break;
                }
                aux.coluna = aux.coluna + 1;
            }

            return mat;
        }

        public override string ToString()
        {
            return "T";
        }

    }
}
