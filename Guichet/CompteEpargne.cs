using System;
using System.Collections.Generic;
using System.Text;

namespace Guichet
{    
    class CompteEpargne : CompteClient
    {
        public CompteEpargne(string numero)
        {
            this.numerocompte = numero;
        }
    }
}
