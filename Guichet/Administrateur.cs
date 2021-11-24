using System;
using System.Collections.Generic;
using System.Text;

namespace Guichet
{
    class Administrateur
    {
        private string nom;
        private string nip;
        public Administrateur()
        {

        }
        protected string NomAdmin { get => nom; set => nom = value; }
        protected string NipAdmin { get => nip; set => nip = value; }

        public void menuAdmin()
        {
            Console.WriteLine("******************************************************************************************************");
            Console.WriteLine("1- Remettre le guichet en fonction");
            Console.WriteLine("2- Déposer de l'argent dans le guichet");
            Console.WriteLine("3- Voir le solde du guichet");
            Console.WriteLine("4- Retourner au menu principal");
            Console.WriteLine("******************************************************************************************************");
            Console.WriteLine("Veuillez choisir votre action parmi les numéros ci-dessus.\n");
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
                //appel fonction panne du guichet
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
            double soldeGuichet;
            soldeGuichet = 10000d;
            //soldeGuichet=soldeGuichet+depotGuichet()
        }
        public void retourMenuppl()
        {
            //appel menu ppl
        }
    }
}
