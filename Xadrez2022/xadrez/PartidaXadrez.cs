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
        public Peca vulneravelEmPassant { get; private set; }

        public PartidaXadrez()
        {
            tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            JogadorAtual = Cor.Branca;
            fimDeJogo = false;
            pEmJogo = new HashSet<Peca>();
            pCapturadas = new HashSet<Peca>();
            colocarPecas();
            vulneravelEmPassant = null;
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

            //# Roque Pequeno
            if (aux is Rei && destino.coluna == origem.coluna + 2)
            {
                Peca torre = tabuleiro.removerPeca(new Posicao(destino.linha, destino.coluna + 1));
                torre.incrementarQuantidadeMovimento();
                tabuleiro.colocarPeca(torre, new Posicao(origem.linha, origem.coluna + 1));
            }
            //# Roque Grande
            if (aux is Rei && destino.coluna == origem.coluna - 2)
            {
                Peca torre = tabuleiro.removerPeca(new Posicao(origem.linha, origem.coluna - 4));
                torre.incrementarQuantidadeMovimento();
                tabuleiro.colocarPeca(torre, new Posicao(origem.linha, origem.coluna - 1));
            }

            //# Executar En Passant
            if (tabuleiro.peca(destino) is Peao && origem.coluna != destino.coluna && pecaCapturada == null)
            {
                //Peca capEmPassant;
                if (JogadorAtual == Cor.Branca)
                {
                    Posicao pos = new Posicao(destino.linha + 1, destino.coluna);
                    pecaCapturada = tabuleiro.removerPeca(pos);
                    pCapturadas.Add(pecaCapturada);
                }
                else
                {
                    Posicao pos = new Posicao(destino.linha - 1, destino.coluna);
                    pecaCapturada = tabuleiro.removerPeca(pos);
                    pCapturadas.Add(pecaCapturada);
                }
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
                if(aux is Peao && capturada != vulneravelEmPassant)
                {
                    tabuleiro.colocarPeca(capturada, destino);
                    pCapturadas.Remove(capturada);
                }
                else
                {
                    if(JogadorAtual == Cor.Branca)
                    {
                        tabuleiro.colocarPeca(capturada, new Posicao(destino.linha + 1, destino.coluna));
                        pCapturadas.Remove(capturada);
                    }
                    else
                    {
                        tabuleiro.colocarPeca(capturada, new Posicao(destino.linha - 1, destino.coluna));
                        pCapturadas.Remove(capturada);
                    }
                }
            }

            //# Roque Pequeno
            if (aux is Rei && destino.coluna == origem.coluna + 2)
            {
                Peca torre = tabuleiro.removerPeca(new Posicao(origem.linha, origem.coluna + 1));
                torre.decrementarQuantidadeMovimento();
                tabuleiro.colocarPeca(torre, new Posicao(aux.posicao.linha, aux.posicao.coluna + 3));
            }

            //# Roque Grande
            if (aux is Rei && destino.coluna == origem.coluna - 2)
            {
                Peca torre = tabuleiro.removerPeca(new Posicao(origem.linha, origem.coluna - 1));
                torre.decrementarQuantidadeMovimento();
                tabuleiro.colocarPeca(torre, new Posicao(aux.posicao.linha, aux.posicao.coluna - 4));
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

            //#En Passant
            Peca p = tabuleiro.peca(destino);
            if (p is Peao && p.posicao.linha == origem.linha + 2 || p.posicao.linha == origem.linha - 2)
            {
                vulneravelEmPassant = p;
            }
            else
            {
                vulneravelEmPassant = null;
            }

            if(p.posicao.linha == 0 || p.posicao.linha == 7)
            {
                promocao(p);
            }
        }

        public void promocao(Peca peca)
        {
            Console.WriteLine();
            Console.WriteLine("Peça promovida!!!");
            Console.Write("Escolha a promoção: T C B D ");
            char promo = char.Parse(Console.ReadLine().ToUpper());
            Peca upada;

            if (promo == 'T')
            {
                upada = new Torre(tabuleiro, peca.cor);
            }
            else if (promo == 'C')
            {
                upada = new Cavalo(tabuleiro, peca.cor);
            }
            else if (promo == 'B')
            {
                upada = new Bispo(tabuleiro, peca.cor);
            }
            else
            {
                upada = new Dama(tabuleiro, peca.cor);
            }

            upada.posicao = new Posicao(peca.posicao.linha, peca.posicao.coluna);
            Peca aux = tabuleiro.removerPeca(peca.posicao);
            pEmJogo.Remove(aux);
            pEmJogo.Add(upada);
            tabuleiro.colocarPeca(upada, upada.posicao);

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

        public bool estaEmXeque(Cor cor)
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

            foreach (Peca p in pecasEmJogo(JogadorAtual))
            {
                bool[,] mat = p.movimentosPossiveis();

                for (int i = 0; i < tabuleiro.Linhas; i++)
                {
                    for (int j = 0; j < tabuleiro.Colunas; j++)
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
            colocarNovaPeca(new Rei(tabuleiro, Cor.Branca, this), 'e', 1);
            colocarNovaPeca(new Torre(tabuleiro, Cor.Branca), 'a', 1);
            colocarNovaPeca(new Torre(tabuleiro, Cor.Branca), 'h', 1);
            colocarNovaPeca(new Cavalo(tabuleiro, Cor.Branca), 'b', 1);
            colocarNovaPeca(new Cavalo(tabuleiro, Cor.Branca), 'g', 1);
            colocarNovaPeca(new Bispo(tabuleiro, Cor.Branca), 'c', 1);
            colocarNovaPeca(new Bispo(tabuleiro, Cor.Branca), 'f', 1);
            colocarNovaPeca(new Dama(tabuleiro, Cor.Branca), 'd', 1);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Branca, this), 'a', 2);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Branca, this), 'b', 2);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Branca, this), 'c', 2);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Branca, this), 'd', 2);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Branca, this), 'e', 2);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Branca, this), 'f', 2);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Branca, this), 'g', 2);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Branca, this), 'h', 2);

            colocarNovaPeca(new Rei(tabuleiro, Cor.Preta, this), 'e', 8);
            colocarNovaPeca(new Torre(tabuleiro, Cor.Preta), 'a', 8);
            colocarNovaPeca(new Torre(tabuleiro, Cor.Preta), 'h', 8);
            colocarNovaPeca(new Cavalo(tabuleiro, Cor.Preta), 'b', 8);
            colocarNovaPeca(new Cavalo(tabuleiro, Cor.Preta), 'g', 8);
            colocarNovaPeca(new Bispo(tabuleiro, Cor.Preta), 'c', 8);
            colocarNovaPeca(new Bispo(tabuleiro, Cor.Preta), 'f', 8);
            colocarNovaPeca(new Dama(tabuleiro, Cor.Preta), 'd', 8);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Preta, this), 'a', 7);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Preta, this), 'b', 7);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Preta, this), 'c', 7);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Preta, this), 'd', 7);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Preta, this), 'e', 7);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Preta, this), 'f', 7);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Preta, this), 'g', 7);
            colocarNovaPeca(new Peao(tabuleiro, Cor.Preta, this), 'h', 7);
        }

    }
}
