using System;
using System.Collections.Generic;
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
        public bool xeque { get; private set; }

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

        public Peca executarMovimento(Posicao origem, Posicao destino)
        {
            Peca aux = tabuleiro.removerPeca(origem);
            aux.incrementarQuantidadeMovimento();
            Peca pecaCapturada = tabuleiro.removerPeca(destino);
            tabuleiro.colocarPeca(aux, destino);
            if (pecaCapturada != null)
            {
                pCapturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }
        public void desfazMovimento(Posicao origem, Posicao destino, Peca capturada)
        {
            Peca aux = tabuleiro.removerPeca(destino);
            aux.decrementarQuantidadeMovimento();
            tabuleiro.colocarPeca(aux, origem);
            if (capturada != null)
            {
                tabuleiro.colocarPeca(capturada, destino);
                pCapturadas.Remove(capturada);
            }
        }

        public void realizarJogada(Posicao origem, Posicao destino)
        {
            Peca capturada = executarMovimento(origem, destino);

            if (estaEmXeque(JogadorAtual))
            {
                desfazMovimento(origem, destino, capturada);
                throw new TabuleiroException("Você não pode se colocar em Xeque!");
            }

            if (estaEmXeque(adversaria(JogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

            if (xequeMate(adversaria(JogadorAtual)))
            {
                fimDeJogo = true;
            }
            else
            {
                turno++;
                mudarJogador();
            }
          
        }


        public void validarPosicaoOrigem(Posicao origem)
        {
            tabuleiro.validarPosicao(origem);

            if (tabuleiro.peca(origem) == null)
            {
                throw new TabuleiroException("Não há peça nessa posição!");
            }
            if (tabuleiro.peca(origem).cor != JogadorAtual)
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
            tabuleiro.validarPosicao(destino);

            if (!tabuleiro.peca(origem).movimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida");
            }
        }

        private void mudarJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor)
        {

            foreach (Peca p in pecasEmJogo(cor))
            {
                if (p is Rei)
                {

                    return p;
                }
            }

            return null;
        }

        private bool estaEmXeque(Cor cor)
        {
            Peca Rei = rei(cor);
            if (Rei == null)
            {
                throw new TabuleiroException("Não há um rei dessa cor na partida!!!");
            }

            foreach (Peca p in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = p.movimentosPossiveis();
                if (mat[Rei.posicao.linha, Rei.posicao.coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public bool xequeMate(Cor cor)
        {
            if (!estaEmXeque(adversaria(JogadorAtual)))
            {
                return false;
            }

            foreach(Peca p in pecasEmJogo(JogadorAtual))
            {
                bool[,] mat = p.movimentosPossiveis();

                for(int i = 0; i < tabuleiro.Linhas; i++)
                {
                    for(int j = 0; j < tabuleiro.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = p.posicao;
                            Posicao destino = new Posicao(i, j);

                            Peca cap = executarMovimento(origem, destino);
                            bool xequeTeste = estaEmXeque(adversaria(JogadorAtual));
                            desfazMovimento(origem, destino, cap);
                            if (xequeTeste)
                            {
                                return false;
                            }
                        }
                    }
                }

            }

            return true;
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca p in pCapturadas)
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
            foreach (Peca p in pEmJogo)
            {
                if (p.cor == cor)
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
            int linha = int.Parse(posicao[1] + "");
            return new PosicaoXadrez(linha, coluna);
        }

        void colocarNovaPeca(Peca peca, char coluna, int linha)
        {
            tabuleiro.colocarPeca(peca, new PosicaoXadrez(linha, coluna).toPosicao());
            pEmJogo.Add(peca);
        }

        void colocarPecas()
        {
            //transformar em método
            colocarNovaPeca(new Rei(tabuleiro, Cor.Branca), 'e', 1);
            colocarNovaPeca(new Torre(tabuleiro, Cor.Branca), 'a', 1);
            colocarNovaPeca(new Torre(tabuleiro, Cor.Branca), 'h', 1);
            colocarNovaPeca(new Cavalo(tabuleiro, Cor.Branca), 'b', 1);
            colocarNovaPeca(new Cavalo(tabuleiro, Cor.Branca), 'g', 1);
            colocarNovaPeca(new Bispo(tabuleiro, Cor.Branca), 'c', 1);
            colocarNovaPeca(new Torre(tabuleiro, Cor.Branca), 'f', 1);
            colocarNovaPeca(new Dama(tabuleiro, Cor.Branca), 'd', 1);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Branca), 'a', 2);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Branca), 'b', 2);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Branca), 'c', 2);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Branca), 'd', 2);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Branca), 'e', 2);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Branca), 'f', 2);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Branca), 'g', 2);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Branca), 'h', 2);

            colocarNovaPeca(new Rei(tabuleiro, Cor.Preta), 'e', 8);
            colocarNovaPeca(new Torre(tabuleiro, Cor.Preta), 'a', 8);
            colocarNovaPeca(new Torre(tabuleiro, Cor.Preta), 'h', 8);
            colocarNovaPeca(new Cavalo(tabuleiro, Cor.Preta), 'b', 8);
            colocarNovaPeca(new Cavalo(tabuleiro, Cor.Preta), 'g', 8);
            colocarNovaPeca(new Bispo(tabuleiro, Cor.Preta), 'c', 8);
            colocarNovaPeca(new Torre(tabuleiro, Cor.Preta), 'f', 8);
            colocarNovaPeca(new Dama(tabuleiro, Cor.Preta), 'd', 8);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Preta), 'a', 7);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Preta), 'b', 7);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Preta), 'c', 7);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Preta), 'd', 7);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Preta), 'e', 7);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Preta), 'f', 7);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Preta), 'g', 7);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Preta), 'h', 7);
        }

    }
}
