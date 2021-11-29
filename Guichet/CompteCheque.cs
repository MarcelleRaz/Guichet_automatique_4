using System;
using System.Collections.Generic;
using System.Text;

namespace Guichet
{    
    class CompteCheque : CompteClient
    {
        public double montent;
       
        public CompteCheque(string numero,double sold)
        {
            this.numerocompte = numero;
            this.sold= sold;
        }
        public  double depot()
        {
         montent = Convert.ToDouble(Console.ReadLine());
            sold += montent;
            Console.WriteLine(propriétair  + "a déposé " + montent);
            return sold;
        }
       
        public  double retirer()
        {
            montent = Convert.ToDouble(Console.ReadLine());
            sold -= montent;
            Console.WriteLine(propriétair+ "a retiré "+ montent);
            return sold;
        }
        public  double virerdecheque()
        {
            sold -= montent;
            Console.WriteLine(propriétair + "de compte cheque a envoyé CompteEpargne"+ sold);
            return sold;
        }
        public double recudeepargne()
        {
            sold += montent;
            return sold;
        }
       
         public double payerfacture()
        {
            double d= montent / 10;
            int n = (int)d;
            sold = sold - montent - n;
            return sold;
        }

        public override void afficherleSold()
        {
            Console.WriteLine(propriétair + "le compte chéque a " + sold);
        }
    }
}
