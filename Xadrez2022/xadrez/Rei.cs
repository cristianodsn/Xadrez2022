using System;
using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        public Rei(Cor cor) : base (cor) { }

        public override string ToString()
        {
            return "R";
        }
    }
}
