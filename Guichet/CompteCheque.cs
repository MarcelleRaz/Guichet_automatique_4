using System;
using System.Collections.Generic;
using System.Text;

namespace Guichet
{    
    class CompteCheque : CompteClient
    {
        public CompteCheque(string numero)
        {
            this.numerocompte = numero;
        }
    }
}
