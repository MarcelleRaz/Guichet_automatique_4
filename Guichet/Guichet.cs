using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guichet
{
    public class Guichet
    {
        private double soldeguichet=10000;
        private bool panne;

        public Guichet()
        {
            this.Soldeguichet = Soldeguichet;
        }

        public double Soldeguichet { get => soldeguichet; set => soldeguichet = value; }
        public bool Panne { get => panne; set => panne = value; }

        public void ouvrirguichet(double Soldeguichet)
        {
            bool Panne;
            if (Soldeguichet <= 0)
            {
                Panne = true;
                modepanne(Panne);
            }
            else
            {
                Panne = false;
                menuprincipal();
            }
        }

        public void modepanne(bool Panne)
        {
            if (Panne == true)
            {
                Console.WriteLine("Le système ne peut pas se connecter à votre compte. Veuillez demander le administrateur.");
            }
        }
     

        public void menuprincipal()
        {
            Console.WriteLine();
            Console.WriteLine("Veuillez choisir l'une des actions suivantes:");
            Console.WriteLine("1- Se connecter à vore compte");
            Console.WriteLine("2- Se connecter comme administrateur");
            Console.WriteLine("3- Quitter");
            string input=Console.ReadLine();
            choisimenuprin(input);
        }
        
        public void choisimenuprin(string menuchoice)
        {
            switch (menuchoice)
            {
                case "1":
                   // CompteCheque client=new CompteCheque;
                    break;

                case"2":
                    //ouvrircompte admi();
                    break;

                case"3":
                    System.Environment.Exit(0);
                    break;
                
                default:
                    menuprincipal();
                    break;
            }
        }

        public string changenip(string nip)
        {
            string newnip = "";

            while (!(newnip.Length == 4))
            {
                Console.WriteLine("Veuillez saisir votre nouveau mot de passe (4 charactor): ");
                newnip = Console.ReadLine();
            }

            while (newnip.Equals(nip))
            {
                Console.WriteLine("Votre mot de passe doit être différent du mont de passe actuel");
                newnip = Console.ReadLine();
            }

            string newnip2 = "";

            while(newnip != newnip2)
            {
                Console.WriteLine("Veuillez confirmer le nouveau mot de passe:");
                newnip2 = Console.ReadLine();
            }
            return newnip;
        }

    }   
    
}
