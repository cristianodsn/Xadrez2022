using System;
using tabuleiro;

namespace xadrez
{
    class Torre : Peca
    {

        public Torre(Cor cor) : base(cor) { }

        public override string ToString()
        {
            return "T";
        }

    }
}
