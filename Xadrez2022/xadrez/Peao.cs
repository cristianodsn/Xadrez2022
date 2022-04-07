using System;
using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tab, Cor cor) : base (tab, cor) { }

        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor;
        }

        private bool existeInimigo(Posicao posicao)
        {
            Peca p = tab.peca(posicao);

            return p != null && p.cor != cor;

        }

        private bool livre(Posicao posicao)
        {
            return tab.peca(posicao) == null;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.Linhas, tab.Colunas];
            Posicao aux = new Posicao(0, 0);

            //Essas duas condicionais poderiam ser convertidas em métodos.
            if (cor == Cor.Branca) 
            {
                aux.definirPosicao(posicao.linha - 1, posicao.coluna);
                if(tab.posicaoValida(aux) && livre(aux))
                {
                    mat[aux.linha, aux.coluna] = true;
                }

                aux.definirPosicao(posicao.linha - 2, posicao.coluna);
                if (tab.posicaoValida(aux) && livre(aux) && quantidadeMovimento == 0)
                {
                    mat[aux.linha, aux.coluna] = true;
                }

                aux.definirPosicao(posicao.linha - 1, posicao.coluna - 1);
                if(tab.posicaoValida(aux) && existeInimigo(aux))
                {
                    mat[aux.linha, aux.coluna] = true;
                }
                aux.definirPosicao(posicao.linha - 1, posicao.coluna + 1);
                if (tab.posicaoValida(aux) && existeInimigo(aux))
                {
                    mat[aux.linha, aux.coluna] = true;
                }
            }

            if (cor == Cor.Preta)
            {
                aux.definirPosicao(posicao.linha + 1, posicao.coluna);
                if (tab.posicaoValida(aux) && livre(aux))
                {
                    mat[aux.linha, aux.coluna] = true;
                }

                aux.definirPosicao(posicao.linha + 2, posicao.coluna);
                if (tab.posicaoValida(aux) && livre(aux) && quantidadeMovimento == 0)
                {
                    mat[aux.linha, aux.coluna] = true;
                }

                aux.definirPosicao(posicao.linha + 1, posicao.coluna - 1);
                if (tab.posicaoValida(aux) && existeInimigo(aux))
                {
                    mat[aux.linha, aux.coluna] = true;
                }
                aux.definirPosicao(posicao.linha + 1, posicao.coluna + 1);
                if (tab.posicaoValida(aux) && existeInimigo(aux))
                {
                    mat[aux.linha, aux.coluna] = true;
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
