using System;
using tabuleiro;
namespace xadrez
{
    class Dama : Peca 
    {
        public Dama (Tabuleiro tab, Cor cor) : base (tab, cor) { }

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
            aux.definirPosicao(posicao.linha - 1, posicao.coluna);
            while (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
                if(tab.peca(aux) != null && tab.peca(aux).cor != cor)
                {
                    break;
                }
                aux.definirPosicao(aux.linha - 1, aux.coluna);
            }

            //Ne
            aux.definirPosicao(posicao.linha - 1, posicao.coluna + 1);
            while(tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
                if (tab.peca(aux) != null && tab.peca(aux).cor != cor)
                {
                    break;
                }
                aux.definirPosicao(aux.linha -1, aux.coluna +1);
            }

            //L
            aux.definirPosicao(posicao.linha, posicao.coluna + 1);
            while (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
                if (tab.peca(aux) != null && tab.peca(aux).cor != cor)
                {
                    break;
                }
                aux.definirPosicao(aux.linha, aux.coluna + 1);
            }

            //Se
            aux.definirPosicao(posicao.linha +1, posicao.coluna + 1);
            while (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
                if (tab.peca(aux) != null && tab.peca(aux).cor != cor)
                {
                    break;
                }
                aux.definirPosicao(aux.linha +1, aux.coluna + 1);
            }

            //S
            aux.definirPosicao(posicao.linha + 1, posicao.coluna);
            while (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
                if (tab.peca(aux) != null && tab.peca(aux).cor != cor)
                {
                    break;
                }
                aux.definirPosicao(aux.linha + 1, aux.coluna);
            }

            //So
            aux.definirPosicao(posicao.linha + 1, posicao.coluna -1);
            while (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
                if (tab.peca(aux) != null && tab.peca(aux).cor != cor)
                {
                    break;
                }
                aux.definirPosicao(aux.linha + 1, aux.coluna -1);
            }

            //O
            aux.definirPosicao(posicao.linha, posicao.coluna - 1);
            while (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
                if (tab.peca(aux) != null && tab.peca(aux).cor != cor)
                {
                    break;
                }
                aux.definirPosicao(aux.linha, aux.coluna - 1);
            }

            //no
            aux.definirPosicao(posicao.linha - 1, posicao.coluna - 1);
            while (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
                if (tab.peca(aux) != null && tab.peca(aux).cor != cor)
                {
                    break;
                }
                aux.definirPosicao(aux.linha - 1, aux.coluna - 1);
            }

            return mat;
        }

        public override string ToString()
        {
            return "D";
        }
    }
}
