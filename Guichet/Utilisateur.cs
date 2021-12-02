using System;
using System.Collections.Generic;
using System.Text;

namespace Guichet
{
    public class Utilisateur
    {
        protected string nom;
        protected string nip;
        protected bool activation;
        protected CompteCheque chequeactuel;
        protected CompteEpargne epargneactuel;

        public Utilisateur(string nom, string nip, CompteCheque cheque,CompteEpargne epargne, bool activate )
        {
            this.nom = nom;
            this.nip = nip;
            this.chequeactuel = cheque;
            this.epargneactuel = epargne;
            this.activation = activate;
            activation = true;
        }
        public string Nom { get => nom; set => nom = value; }
        public string Nip { get => nip; set => nip = value; }
        public bool Activation { get => activation; set => activation = value; }
        public CompteCheque Chequeactuel { get => chequeactuel; set => chequeactuel = value; }
        public CompteEpargne Epargneactuel { get => epargneactuel; set => epargneactuel = value; }
        
        public void changermdp()
        {
            Console.WriteLine("******************************************************************************************************");
            Console.WriteLine("Changement de mot de passe");
            Console.WriteLine("Veuillez saisir votre nouveau mot de passe:\nIl doit contenir 4 caractères de votre choix.");
            string newnip = Console.ReadLine();
            if (newnip.Equals(nip))
            {
                Console.WriteLine("Il faut que le nouveau mot de passe soit différent du précédent.\nVeuillez fournir un nouveau mot de passe:");
                newnip = Console.ReadLine();
            }
            if (!(newnip.Length == 4))
            {
                Console.WriteLine("Le mot de passe doit contenir 4 caratères. Veuillez saisir à nouveau un mot de passe:");
                newnip = Console.ReadLine();
            }
            Console.WriteLine("Veuillez confirmer votre nouveau mot de passe:");
            string confirmnip = Console.ReadLine();
            if (!(newnip == confirmnip))
            {
                Console.WriteLine("Les deux saisies doivent être identiques.\n Veuillez reconfirmer à nouveau votre nouveau mot de passe:");
                confirmnip = Console.ReadLine();
            }
            else
            {
                nip = confirmnip;
                Console.WriteLine("Votre nouveau mot de passe est actif.");
            }
        }
        public void menudepot()
        {
            Console.WriteLine("******************************************************************************************************");
            Console.WriteLine("Dans quel compte voulez-vous déposer votre argent?");
            Console.WriteLine("1- Compte Chèque");
            Console.WriteLine("2- Compte Epargne");
            Console.WriteLine("Saisir 1 pour le compte Chèque et 2 pour le compte Epargne:");
            choicedepot();
        }
        public void choicedepot()
        {
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    //CompteClient.depot();
                    break;
                case "2":
                    //CompteEpargne.depot();
                    break;
                default:
                    Console.WriteLine("Veuillez refaire un choix valide svp.");
                    choice = Console.ReadLine();
                    break;
            }
        }
        public void modeverrouillage(bool verrouillage)
        {
            verrouillage = activation;
            if (verrouillage == false)
            {
                Console.WriteLine("Vous avez effectué 3 tentatives. Votre compte est verrouillé.\n Veuillez contacter un administrateur pour le réactiver.");
            }
            
        }
    }
}
