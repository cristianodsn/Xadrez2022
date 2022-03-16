﻿using System;
using tabuleiro;

namespace tabuleiro
{
    class Tabuleiro
    {
        public int Linhas { get; protected set; }
        public int Colunas { get; protected set; }
        private Peca[,] pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            this.pecas = new Peca[linhas, colunas];
        }

        public Peca peca(Posicao posicao)
        {
            return pecas[posicao.linha, posicao.coluna];
        }
        public Peca peca(int linha, int coluna)
        {
            return pecas[linha, coluna];
        }

        public void colocarPeca(Peca peca, Posicao posicao)
        {
            pecas[posicao.linha, posicao.coluna] = peca;
        }

        public bool posicaoValida(Posicao posicao)
        {
            if (posicao.linha < 0 || posicao.linha >= this.Linhas || posicao.coluna < 0 
                || posicao.coluna >= this.Colunas)
            {
                return false;
            }
            return true;
        }

        public void validarPosicao(Posicao posicao)
        {
            if(!posicaoValida(posicao))
            {
                throw new TabuleiroException("Posição inválida.");
            }
        }

        public bool existePeca(Posicao posicao)
        {
            validarPosicao(posicao);
            return peca(posicao) != null; //Retornar nulo se não hover peça no campo.
        }
    }
}
