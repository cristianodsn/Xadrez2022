using System;
using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tab, Cor cor) : base (tab, cor) { }

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
            aux.definirPosicao(posicao.linha -1, posicao.coluna);
            if(tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
            }


            //ne
            aux.definirPosicao(posicao.linha - 1, posicao.coluna +1);
            if (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
            }


            //direita
            aux.definirPosicao(posicao.linha, posicao.coluna + 1);
            if (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
            }

            //se
            aux.definirPosicao(posicao.linha +1 , posicao.coluna + 1);
            if (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
            }

            //abaixo
            aux.definirPosicao(posicao.linha + 1, posicao.coluna);
            if (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
            }

            //so
            aux.definirPosicao(posicao.linha + 1, posicao.coluna -1);
            if (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
            }

            //esquerda
            aux.definirPosicao(posicao.linha, posicao.coluna - 1);
            if (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
            }

            //no
            aux.definirPosicao(posicao.linha -1, posicao.coluna - 1);
            if (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
            }

            return mat;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
