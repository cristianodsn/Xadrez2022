﻿using System;
using tabuleiro;
using xadrez;
namespace Xadrez2022
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaXadrez partida = new PartidaXadrez();
                while (!partida.fimDeJogo)
                {
                    try
                    {
                        Console.Clear();
                        Tela.imprimirTabuleito(partida.tabuleiro);
                        Console.WriteLine();
                        Console.WriteLine("Turno atual: " + partida.turno);
                        Console.WriteLine("Aguardando Jogada: " + partida.JogadorAtual);
                        Console.WriteLine();

                        Console.Write("Origem: ");
                        Posicao origem = partida.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoOrigem(origem);
                        bool[,] aux = partida.tabuleiro.peca(origem).movimentosPossiveis();
                        Console.Clear();
                        Tela.imprimirTabuleito(partida.tabuleiro, aux);

                        Console.Write("Destino: ");
                        Posicao destino = partida.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDestino(origem, destino);
                        partida.realizarJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
            }

            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}
