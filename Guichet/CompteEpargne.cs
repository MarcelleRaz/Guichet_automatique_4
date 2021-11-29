using System;
using System.Collections.Generic;
using System.Text;

namespace Guichet
{    
    class CompteEpargne : CompteClient
    {
        public double montent;
        public CompteEpargne(string numero, double sold)
        {
            this.numerocompte = numero;
            this.sold = sold;
        }
        public  double depot1()
        {
            montent = Convert.ToDouble(Console.ReadLine());
            sold += montent;
            Console.WriteLine(propriétair + "a déposé " + montent);
            return sold;
        }

        public double retirer()
        {
            montent = Convert.ToDouble(Console.ReadLine());
            sold -= montent;
            Console.WriteLine(propriétair + "a retiré " + montent);
            return sold;
        }
        public double virerdeepargne()
        { 
            sold -= montent;
            Console.WriteLine(propriétair + "de compte cheque a envoyé Comptecheque");
            return sold;
        }
        public double recudecheque()
        {
            sold += montent;
            return sold;
        }
        public double payerfacture()
        {
            double d = montent / 10;
            int n = (int)d;
            sold = sold - montent - n;
            return sold;
        }

        public override void afficherleSold()
        {
            Console.WriteLine(propriétair + "le compte epargne a " + sold);
        }
    }
}
