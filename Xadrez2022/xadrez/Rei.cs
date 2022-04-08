using System;
using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        private PartidaXadrez partidaXadrez;

        public Rei(Tabuleiro tab, Cor cor, PartidaXadrez partidaXadrez) : base(tab, cor)
        {
            this.partidaXadrez = partidaXadrez;
        }

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
            if (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
            }


            //ne
            aux.definirPosicao(posicao.linha - 1, posicao.coluna + 1);
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
            aux.definirPosicao(posicao.linha + 1, posicao.coluna + 1);
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
            aux.definirPosicao(posicao.linha + 1, posicao.coluna - 1);
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
            aux.definirPosicao(posicao.linha - 1, posicao.coluna - 1);
            if (tab.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
            }

            if(quantidadeMovimento == 0 && !partidaXadrez.xeque)
            {
                Peca t1 = tab.peca(posicao.linha, posicao.coluna + 3);
                Peca t2 = tab.peca(posicao.linha, posicao.coluna -4);

                //#Roque Pequeno
                if (testeT(t1))
                {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);

                    if (tab.peca(p1) == null && tab.peca(p2) == null)
                    {
                        mat[posicao.linha, posicao.coluna + 2] = true;
                    }
                }

                //#Roque Grande
                if (testeT(t2))
                {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);

                    if (tab.peca(p1) == null && tab.peca(p2) == null && tab.peca(p3) == null)
                    {
                        mat[posicao.linha, posicao.coluna - 2] = true;
                    }
                }

            }

            return mat;
        }

        private bool testeT(Peca peca)
        {
            return peca != null && peca.cor == cor && peca is Torre && peca.quantidadeMovimento == 0; 
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
