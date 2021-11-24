using System;
using System.Collections.Generic;
using System.Text;

namespace Guichet
{
    class Administrateur:Guichet
    {
        private string nom;
        private string nip;
        public Administrateur()
        {

        }
        public string NomAdmin { get => nom; set => nom = value; }
        public string NipAdmin { get => nip; set => nip = value; }

        public void menuAdmin()
        {
            Console.WriteLine("******************************************************************************************************");
            Console.WriteLine("1- Remettre le guichet en fonction");
            Console.WriteLine("2- Déposer de l'argent dans le guichet");
            Console.WriteLine("3- Voir le solde du guichet");
            Console.WriteLine("4- Retourner au menu principal");
            Console.WriteLine("******************************************************************************************************");
            Console.WriteLine("Veuillez choisir votre action parmi les numéros ci-dessus:\n");
            choiceAdmin();
        }
        public void choiceAdmin()
        {
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    remiseFonction();
                    break;
                case "2":
                    depotGuichet();
                    break;
                case "3":
                    soldeGuichet();
                    break;
                case "4":
                    retourMenuppl();
                    break;
                default:
                    Console.WriteLine("Veuillez choisir un bon numéro parmi les actions ci-dessus.\n");
                    break;
            }
        }
        public void remiseFonction()
        {
            Console.WriteLine("Désirez-vous remettre le système en fonction? Saisir O pour 'oui' et N pour 'Non'\n");
            string response=Console.ReadLine();
            
            if (response.Equals("O"))
            {
                retourMenuppl();
            }
            if (response.Equals("N"))
            {
                modepanne(true);
            }
            while (!response.Equals("O") && !response.Equals("N")) 
            {
             Console.WriteLine("Réponse érronée. Veuillez saisir O pour 'oui' et N pour 'Non'\n");
             response = Console.ReadLine();
            }
        }
        public double depotGuichet()
        {
            double depot;
            Console.WriteLine("Veuillez saisir le montant à déposer: \n");
            depot = Convert.ToDouble(Console.ReadLine());
            if (depot>=10000d)
            {
                Console.WriteLine("Le montant maximum de dépot est de 10 000,00$.\nVeuillez saisir un nouveau montant à déposer.\n");
                depot = Convert.ToDouble(Console.ReadLine());
            }
                return (depot);
        }
        public void soldeGuichet()
        {
            double soldeGuichet = Soldeguichet;
            soldeGuichet += depotGuichet();
            Console.WriteLine("Le solde du guichet est de: {0}",soldeGuichet);
        }
        public void retourMenuppl()
        {
            menuprincipale();
        }
        public void accesComptAdmin()
        {
            Console.WriteLine("******************************************************************************************************");
            Console.WriteLine("Bienvenue sur le compte Administrateur");
            Console.WriteLine("Veuillez saisir vos informations:");
            Console.WriteLine("Nom d'utilisateur:\n");
            NomAdmin=Console.ReadLine();
            Console.WriteLine("Mot de passe:\n");
            NipAdmin=Console.ReadLine();
            verificationAcces();
        }
        public override void verificationAcces()
        {
            int i = 1;
            while(i<3)
            {
                if (NomAdmin.Equals("admin") && NipAdmin.Equals("123456"))
                {
                    menuAdmin();
                }
                
                if(!NomAdmin.Equals("admin") || !NipAdmin.Equals("123456"))
                {
                    Console.WriteLine("La combinaison 'Nom d'utilisateur et NIP' n'est pas reconnue.\nVeuillez saisir votre nom d'utilisateur et nip.");
                    Console.WriteLine("Nom d'utilisateur:\n");
                    NomAdmin = Console.ReadLine();
                    Console.WriteLine("Mot de passe:\n");
                    NipAdmin = Console.ReadLine();
                }
                i = i + 1;
            }
        }
    }
}
