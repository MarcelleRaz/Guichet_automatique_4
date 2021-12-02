using System;
using System.Collections.Generic;
using System.Text;

namespace Guichet
{
    public class CompteCheque : CompteClient
    {
        private double solde;
        public CompteCheque(string numero, double solde)
        {
            this.numerocompte = numero;
            this.solde = solde;
        }

        public double Solde { get => solde; set => solde = value; }
    }
}
