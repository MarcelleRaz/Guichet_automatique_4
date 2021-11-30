using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guichet
{

    public class Guichet
    {
        private double soldeguichet = 10000d;
        private bool panne;
        private List<CompteClient> listcompte;
        private CompteCheque chequeActuel;
        private CompteEpargne epargneActuel;
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

        internal CompteAdmin CompteAdmin { get => compteAdmin; set => compteAdmin = value; }
        internal CompteCheque ChequeActuel { get => chequeActuel; set => chequeActuel = value; }
        internal CompteEpargne EpargneActuel { get => epargneActuel; set => epargneActuel = value; }

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
                menuPrincipal();
            }
        }

        public void modepanne(bool Panne)
        {
            if (Panne == true)
            {
                Console.WriteLine("Le système ne peut pas se connecter à votre compte. Veuillez demander le administrateur.");
            }
        }

        public void menuPrincipal()
        {
            Console.WriteLine();
            Console.WriteLine("Veuillez choisir l'une des actions suivantes:");
            Console.WriteLine("1- Se connecter à vore compte");
            Console.WriteLine("2- Se connecter comme administrateur");
            Console.WriteLine("3- Quitter");

            string input = Console.ReadLine();
            choisiMenuPrin(input);
        }

        public void choisiMenuPrin(string menuchoice)
        {
            switch (menuchoice)
            {
                case "1":
                    seconnecterClient();
                    break;

                case "2":
                    //seconnecterAdmin()
                    break;

                case "3":
                    System.Environment.Exit(0);
                    break;

                default:
                    menuPrincipal();
                    break;
            }
        }

        //No. 1 du menu principal
        public void seconnecterClient()
        {
            if (validerNomNip() == false)
            {
                ChequeActuel.Blocked = true;
                EpargneActuel.Blocked = true;

                validerBlocked();
                
            }
            else
            {
                menuUsager();
                string input = Console.ReadLine();
                faireChoix(input);
            }
        }

        public bool validerNomNip()
        {
            bool rightNomNip = false;
            Console.WriteLine("Veuillez saisir votre nom en 8 caractères:");
            string nomclient = Console.ReadLine();
            Console.WriteLine("Veuillez saisir votre mon de passe en 4 caractère");
            string nipclient = Console.ReadLine();
            int j = 0;

            //while (rightNomNip == true && j < 2)
            //{
            foreach (CompteClient compteClient in Listcompte)
            {
                if (j == 2)
                {
                    rightNomNip = false;
                }
                if (nomclient.Equals(compteClient.Nom) && nipclient.Equals(compteClient.Nip))
                {
                    if (compteClient is CompteCheque)
                    {
                        ChequeActuel = compteClient as CompteCheque;
                    }
                    else if (compteClient is CompteEpargne)
                    {
                        EpargneActuel = compteClient as CompteEpargne;
                    }

                    rightNomNip = true;
                    break;
                }
                //return rightNomNip;
                if (j<2 && (!nomclient.Equals(compteClient.Nom) || !nipclient.Equals(compteClient.Nip)))
                {
                    afficherErreur();
                    Console.WriteLine("Veuillez saisir votre nom en 8 caractères:");
                    nomclient = Console.ReadLine();
                    Console.WriteLine("Veuillez saisir votre mon de passe en 4 caractère");
                    nipclient = Console.ReadLine();
                }
                j++;
            }
            //}
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

                foreach (CompteClient compteClient in Listcompte)
                {
                    if (nipclient.Equals(compteClient.Nip))
                    {
                        rightNip = true;
                        break;
                    }
                }
                j++;
            }

            return rightNip;
        }

        public void afficherErreur()
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
                    changeNip();
                    break;

                case "2":
                    break;

                case "3":
                    break;

                case "4":
                    soldeCompte();
                    break;

                case "5":
                    virement();
                    break;

                case "6":
                    break;

                case "7":
                    fermerSession();
                    break;

                default:
                    break;
            }
        }

        //No. 1 du meun usager: Changer le mot de passe
        public void changeNip()
        {
            Console.WriteLine("Veuillez sairsir votre mot de passe actuel:");
            string nipclient = Console.ReadLine();

            string newnip = "";

            while (newnip.Length != 4)
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

            while (newnip != newnip2)
            {
                Console.WriteLine("Veuillez confirmer le nouveau mot de passe:");
                newnip2 = Console.ReadLine();
            }

            ChequeActuel.Nip = newnip2;
            EpargneActuel.Nip = newnip2;

            Console.WriteLine($"Nouveau mot de passe:{newnip2}");

        }
        // No.4 du menu usage: Afficher le solde du compte cheque ou du compte epargne
        public void soldeCompte()
        {
            string a = "";
            while(!(a =="1" || a == "2"))
            {
                afficherMenuSolde();
                a = Console.ReadLine();

                if (a == "1")
                {
                    string t1 = Convert.ToDouble(ChequeActuel.Soldecompte).ToString("0 000.00");
                    Console.WriteLine($"Le montant du compte cheque est: {t1}");
                }
                else if(a == "2")
                {
                    string t2 = Convert.ToDouble(EpargneActuel.Soldecompte).ToString("0 000.00");
                    Console.WriteLine($"Le montant du compte épargne est: {t2}");
                }
                menuUsager();
            }

        }
        public void afficherMenuSolde()
        {
            Console.WriteLine();
            Console.WriteLine("1- Solde du compte chèque");
            Console.WriteLine("2- Solde du compte épargne");
        }

        // No.5 du menu usage: Effectuer un virement entre les comptes
        public void virement()
        {
            string c = "";

            while (!(c == "1" || c == "2"))
            {
                Console.WriteLine();
                Console.WriteLine("Veuillez choisir: ");
                Console.WriteLine("1. Du compte cheque à compte épagne");
                Console.WriteLine("2. Du compte épagne à compte cheque");
                c = Console.ReadLine();
            }

            switch (c)
            {
                case "1":
                    bool AtoB1 = true;
                    virementAtoB(AtoB1);
                    break;

                case "2":
                    bool AtoB2 = false;
                    virementAtoB(AtoB2);
                    break;

                default:

                    break;

            }

        }

        public double validationMontant()
        {
            string montant = "";
            double corretmontant = 0;
            bool t = false;

            while (t == false)
            {
                Console.WriteLine("Veuillez saisir le montant:");
                montant = Console.ReadLine();
                t = Double.TryParse(montant, out corretmontant);
                corretmontant = Math.Round(corretmontant, 2);

                if (corretmontant < 0)
                {
                    t = false;
                    continue;
                }
                else if (corretmontant > 10000)
                {
                     
                    if (validerNip() == false)
                    {
                        ChequeActuel.Blocked = true;
                        EpargneActuel.Blocked = true;
                    }
                    return corretmontant;
                   
                }

            }

            return corretmontant;
        }


        public void virementAtoB(bool AtoB)
        {
            double montant = validationMontant();
            string t = Convert.ToDouble(montant).ToString(" 000.00");
            Console.WriteLine($"Le montant du compte cheque à compte épagne est: {t}");
            if (AtoB == true)
            {
                ChequeActuel.Soldecompte = ChequeActuel.Soldecompte - montant;
                EpargneActuel.Soldecompte = EpargneActuel.Soldecompte + montant;
                Console.WriteLine("");
            }
            else
            {
                EpargneActuel.Soldecompte = EpargneActuel.Soldecompte - montant;
                ChequeActuel.Soldecompte = ChequeActuel.Soldecompte + montant;
            }

        }

        // No.7 du menu usage: Fermer session
        public void fermerSession()
        {
            ChequeActuel = null;
            EpargneActuel = null;
            menuPrincipal();
        }

        public bool validerBlocked()
        {
            bool t = false;
            if (ChequeActuel.Blocked == true || EpargneActuel.Blocked == true)
            {
                Console.WriteLine("Votre compte est vérouillé");
                t = true;
            }
            return t;
        }

        // No.2 du menu principal
        public void seconnecterAdmin()
        {

        }
        public void validerAdmin()
        {
            string nom = "";
            string nip = "";

            while (!(nom.Equals(CompteAdmin.Nom)) && nip.Equals(CompteAdmin.Nip))
            {
                Console.WriteLine("Veuillez saisir votre nom:");
                nom = Console.ReadLine();
                Console.WriteLine("Veuillez saisir le mot de passe:");
                nip = Console.ReadLine();
            }
        }
    }  
    
}
