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
        public void decrementarQuantidadeMovimento()
        {
            quantidadeMovimento--;
        }

        public abstract bool[,] movimentosPossiveis();

        public bool exiteMovimentoPossivel()
        {
            bool[,] mat = movimentosPossiveis();
            
            for(int i = 0; i<mat.GetLength(0); i++)
            {
                for(int j =0; j<mat.GetLength(1); j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public bool podeMoverPara(Posicao destino)
        {
            bool[,] mat = movimentosPossiveis();
            return mat[destino.linha, destino.coluna];
        }
    }
}
