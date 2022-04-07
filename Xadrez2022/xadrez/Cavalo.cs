using System;
using tabuleiro;

namespace xadrez
{
    class Cavalo : Peca
    {

        public Cavalo(Tabuleiro tab, Cor cor) : base(tab, cor) { }

        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.Linhas, tab.Colunas];
            Posicao aux = new Posicao(0, 0);

            //N
            aux.definirPosicao(posicao.linha - 2, posicao.coluna + 1);
            if(tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
            }
            aux.definirPosicao(posicao.linha - 2, posicao.coluna - 1);
            if (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
            }

            //L
            aux.definirPosicao(posicao.linha - 1, posicao.coluna + 2);
            if (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
            }
            aux.definirPosicao(posicao.linha +1, posicao.coluna +2);
            if (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
            }

            //S
            aux.definirPosicao(posicao.linha +2, posicao.coluna -1);
            if (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
            }
            aux.definirPosicao(posicao.linha + 2, posicao.coluna + 1);
            if (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
            }

            //O
            aux.definirPosicao(posicao.linha -1, posicao.coluna - 2);
            if (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
            }
            aux.definirPosicao(posicao.linha + 1, posicao.coluna - 2);
            if (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
            }

            return mat;
        }

        public override string ToString()
        {
            return "C";
        }
    }
}
