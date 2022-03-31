using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace xadrez
{
    class PartidaXadrez
    {
        public Tabuleiro tabuleiro { get; private set; }
        public int turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool fimDeJogo { get; private set; }
        private HashSet<Peca> pEmJogo;
        private HashSet<Peca> pCapturadas;

        public PartidaXadrez()
        {
            tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            JogadorAtual = Cor.Branca;
            fimDeJogo = false;
            pEmJogo = new HashSet<Peca>();
            pCapturadas = new HashSet<Peca>();
            colocarPecas();
           
        }

        public void executarMovimento(Posicao origem, Posicao destino)
        {           
            Peca aux = tabuleiro.removerPeca(origem);
            aux.incrementarQuantidadeMovimento();
            Peca pecaCapturada = tabuleiro.removerPeca(destino);
            tabuleiro.colocarPeca(aux, destino);
            if(pecaCapturada != null)
            {
                pCapturadas.Add(pecaCapturada);
            }
        }

        public void validarPosicaoOrigem(Posicao origem)
        {
            tabuleiro.validarPosicao(origem);

            if (tabuleiro.peca(origem) == null)
            {
                throw new TabuleiroException("Não há peça nessa posição!");
            }
            if(tabuleiro.peca(origem).cor != JogadorAtual)
            {
                throw new TabuleiroException("Peça inválida!");
            }
            if (!tabuleiro.peca(origem).exiteMovimentoPossivel())
            {
                throw new TabuleiroException("Não há um movimento possível para essa peça!");
            }
        }
        
        public void validarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!tabuleiro.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida");
            }
        }

        public void realizarJogada(Posicao origem, Posicao destino)
        {
            executarMovimento(origem, destino);
            turno++;
            mudarJogador();
        }

        private void mudarJogador()
        {
            if(JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca p in pCapturadas)
            {
                if (p.cor == cor)
                {
                    aux.Add(p);
                }
            }
            return aux;
        }
        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca p in pEmJogo)
            {
                if(p.cor == cor)
                {
                    aux.Add(p);
                }
            }            
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        public PosicaoXadrez lerPosicaoXadrez()
        {
            string posicao = Console.ReadLine();
            char coluna = posicao[0];
            int linha = int.Parse(posicao[1]+"");
            return new PosicaoXadrez(linha, coluna);
        }

        void colocarNovaPeca(Peca peca, char coluna, int linha)
        {
            tabuleiro.colocarPeca(peca, new PosicaoXadrez(linha, coluna).toPosicao());
            pEmJogo.Add(peca);
        }

        void colocarPecas()
        {
            colocarNovaPeca(new Rei(tabuleiro, Cor.Branca),'d',1);
            colocarNovaPeca(new Torre(tabuleiro, Cor.Branca), 'c', 1);
            colocarNovaPeca(new Torre(tabuleiro, Cor.Branca), 'e', 1);
            colocarNovaPeca(new Torre(tabuleiro, Cor.Branca), 'd', 2);
            colocarNovaPeca(new Torre(tabuleiro, Cor.Branca), 'e', 2);
            colocarNovaPeca(new Torre(tabuleiro, Cor.Branca), 'c', 2);


            colocarNovaPeca(new Rei(tabuleiro, Cor.Preta), 'd', 8);
            colocarNovaPeca(new Torre(tabuleiro, Cor.Preta), 'c', 8);
            colocarNovaPeca(new Torre(tabuleiro, Cor.Preta), 'e', 8);
            colocarNovaPeca(new Torre(tabuleiro, Cor.Preta), 'd', 7);
            colocarNovaPeca(new Torre(tabuleiro, Cor.Preta), 'e', 7);
            colocarNovaPeca(new Torre(tabuleiro, Cor.Preta), 'c', 7);
        }

    }
}
