using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guichet
{

    public class Guichet
    {
        private double soldeguichet=10000d;
        private bool panne;
        private List<CompteClient> listcompte;
        private CompteClient clientActuel;
        private CompteAdmin compteAdmin;
        public Guichet()
        {
            this.Soldeguichet = Soldeguichet;
        }

        internal Guichet(List<CompteClient> listcompte, CompteAdmin compteAdmin)
        {
            this.Listcompte = listcompte;
            this.CompteAdmin = compteAdmin;
        }

        public double Soldeguichet { get => soldeguichet; set => soldeguichet = value; }
        public bool Panne { get => panne; set => panne = value; }
        internal List<CompteClient> Listcompte { get => listcompte; set => listcompte = value; }
        internal CompteClient ClientActuel { get => clientActuel; set => clientActuel = value; }
        internal CompteAdmin CompteAdmin { get => compteAdmin; set => compteAdmin = value; }

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
                    seconnecterclient();
                    break;

                case"2":
                    //seconnecteradmin()
                    break;

                case"3":
                    System.Environment.Exit(0);
                    break;
                
                default:
                    menuprincipal();
                    break;
            }
        }

        public void seconnecterclient()
        {
            bool T1 = validerNomNip();
            
            if (T1 == false)
            {
                affichererreur();
            }
            else
            {
                menuUsager();
                string input=Console.ReadLine();
                faireChoix(input);
            }
        }
        
        public bool validerNomNip()
        {
            bool rightNomNip = false;
            int j = 0;

            while(rightNomNip == false && j < 3)
            {
                Console.WriteLine("Veuillez saisir votre nom en 8 caractères:");
                string nomclient = Console.ReadLine();
                Console.WriteLine("Veuillez saisir votre mon de passe en 4 caractère");
                string nipclient = Console.ReadLine();

                for (int i = 0; i < Listcompte.Count; i++)
                {
                    if (nomclient.Equals(Listcompte[i].Nom) && nipclient.Equals(Listcompte[i].Nip))
                    {
                        rightNomNip = true;
                        ClientActuel = listcompte[i];  //Pour utiliser le nom et nip du client actuel 
                        break;
                    }
                }
                j++;
            }

            return rightNomNip;
        }

        public bool validerNip()
        {
            bool rightNip = false;
            int j = 0;

            while (rightNip == false && j < 3)
            {
                Console.WriteLine("Veuillez saisir votre mot de passe en 4 caractères");
                string nipclient = Console.ReadLine();

                for (int i = 0; i < Listcompte.Count; i++)
                {
                    if (nipclient.Equals(Listcompte[i].Nip))
                    {
                        rightNip = true;
                        break;
                    }
                }
                j++;
            }

            return rightNip;
        }

        public void affichererreur()
        {
            Console.WriteLine("Une des valeurs n'est pas valide");
        }
        
        public void menuUsager()
        {
            Console.WriteLine();
            Console.WriteLine("1- Changer le mot de passe");
            Console.WriteLine("2- Déposer un montant dans un compte");
            Console.WriteLine("3- Retirer un montant d'un compte");
            Console.WriteLine("4- Afficher le solde du compte chèque ou épargne");
            Console.WriteLine("5- Effecter un virement entre les comptes");
            Console.WriteLine("6- Payer une facture");
            Console.WriteLine("7- Fermer session");
        }

        public void faireChoix(string input)
        {
            switch (input)
            {
                case "1":
                    changenip();
                    break;

                case "2":
                    break;

                case "3":
                    break;

                case "4":
                    break;

                case "5":
                    virement();
                    break;

                case "6":
                    break;

                case "7":
                    fermersession();
                    break;

                default:
                    break;
            }
        }

        public void changenip()
        {
            Console.WriteLine("Enterez votre mot de passe actuel:");
            string nipclient = Console.ReadLine();

            string newnip = "";

            while (!(newnip.Length == 4))
            {
                Console.WriteLine("Veuillez saisir votre nouveau mot de passe (4 charactor): ");
                newnip = Console.ReadLine();
            }

            while (newnip.Equals(nipclient))
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

            for (int i = 0; i < Listcompte.Count; i++)
            {
                if (nipclient.Equals(Listcompte[i].Nip) && ClientActuel.Nom.Equals(Listcompte[i].Nom))
                {
                    Listcompte[i].Nip = newnip;

                    //Console.WriteLine($"Nouveau mot de passe: {Listcompte[i].Nip}");
                    //Console.WriteLine($"Nom du client: {Listcompte[i].Nom}");
                    //Console.WriteLine($"Nouveau mot de passe: {Listcompte[i - 1].Nip}");
                    //Console.WriteLine($"Nom du client: {Listcompte[i - 1].Nom}");
                }
            }

        }

        public void virement()
        {
            
            //Console.WriteLine("");
        }
        
        public void fermersession()
        {
            ClientActuel = null;
            menuprincipal();
        }

        public void seconnecteradmin()
        {

        }
        public void validerAdmin()
        {
            string nom = "";
            string nip = "";


            while (!(nom.Equals(CompteAdmin.Nom)) && nip.Equals(CompteAdmin
                
                
                .Nip))
            {
                Console.WriteLine("Veuillez saisir votre nom:");
                nom = Console.ReadLine();
                Console.WriteLine("Veuillez saisir le mot de passe:");
                nip = Console.ReadLine();
            }
        }
    }   
    
}
