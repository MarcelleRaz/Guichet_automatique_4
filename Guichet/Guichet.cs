using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Guichet
{
    public class Guichet
    {
        private double soldeguichet;
        private bool panne;
        private List<string [,,,]>listCompte;
        //Administrateur admin = new Administrateur();
        public Guichet()
        {
            this.Soldeguichet = Soldeguichet;
            Soldeguichet = 10000d;
            this.listCompte = ListCompte;
        }

        public double Soldeguichet { get => soldeguichet; set => soldeguichet = value; }
        public bool Panne { get => panne; set => panne = value; }
        public List<string[,,,]> ListCompte { get => listCompte; set => listCompte = value; }

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
                menuprincipale();
            }
        }
        public void modepanne(bool Panne)
        {
            if (Panne == true)
            {
                Console.WriteLine("Le système ne peut pas se connecter à votre compte. Veuillez demander le administrateur.");
            }
        }
        public void menuprincipale()
        {
            Console.WriteLine();
            Console.WriteLine("Veuillez choisir l'une des actions suivantes:");
            Console.WriteLine("1- Se connecter à vore compte");
            Console.WriteLine("2- Se connecter comme adimnistrateur");
            Console.WriteLine("3- Quitter");
            choisirMenuppl();
        }
        public void choisirMenuppl()
        {
            string menuchoice="";
            switch (menuchoice)
            {
                case "1":
                    accesComptClient();
                    break;

                case "2":
                    //admin.accesComptAdmin();
                    break;

                case "3":
                    System.Environment.Exit(0);
                    break;

                default:
                    menuprincipale();
                    break;
            }
        }
        public void accesComptClient()
        {
            Console.WriteLine("******************************************************************************************************");
            Console.WriteLine("Bienvenue sur notre Guichet Automatique");
            Console.WriteLine("Veuillez saisir vos informations:");
            Console.WriteLine("Nom d'utilisateur:\n");
            Console.ReadLine();
            Console.WriteLine("Mot de passe:\n");
            Console.ReadLine();
        }  
        public virtual void verificationAcces()
        {
            
        }
    }    
}
