using System;
using System.Collections.Generic;
using System.Text;

namespace Guichet
{
    public class CompteEpargne : CompteClient
    {
        private double solde;
        public CompteEpargne(string numero, double solde)
        {
            this.numerocompte = numero;
            this.solde = solde;
        }

        public double Solde { get => solde; set => solde = value; }
    }
}
