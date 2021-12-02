using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;


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
                Console.WriteLine("Le système ne peut pas se connecter à votre compte. Veuillez demander le administrateur.\n");
                
            }
        }

        public void menuPrincipal()
        {
           

            Console.WriteLine("\nVeuillez choisir l'une des actions suivantes:");
            Console.WriteLine("1- Se connecter à vore compte");
            Console.WriteLine("2- Se connecter comme administrateur");
            Console.WriteLine("3- Quitter\n");

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
                afficherErreur();
                fermerSession();
                menuPrincipal();
            }
            else
            {
                menuUsager();
            }
        }

        public bool validerNomNip()
        {
            bool rightNomNip = false;
            Console.WriteLine("Veuillez saisir votre nom en 8 caractères:");
            string nomclient = Console.ReadLine();
            Console.WriteLine("Veuillez saisir votre mon de passe en 4 caractères:");
            string nipclient = Console.ReadLine();
            int j = 0;

            while (j < 2)
            {
                foreach (CompteClient compteClient in Listcompte)
                {
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
                    }
                }

                if (ChequeActuel != null && EpargneActuel != null)
                {
                    rightNomNip = true;
                    break;
                }

                j++;
                Console.WriteLine("\nUne des valeurs n'est pas valide.");
                Console.WriteLine("\nVeuillez saisir votre nom en 8 caractères:");
                nomclient = Console.ReadLine();
                Console.WriteLine("Veuillez saisir votre mon de passe en 4 caractère:");
                nipclient = Console.ReadLine();
            }
            return rightNomNip;
        }

        public bool validerNip()
        {
            bool rightNip = false;
            int j = 0;
            while (j < 3)
            {
                Console.WriteLine("\nVeuillez saisir votre mot de passe en 4 caractères:\n");
                string nipclient = Console.ReadLine();

                foreach (CompteClient compteClient in Listcompte)
                {
                    if (nipclient.Equals(compteClient.Nip))
                    {
                        rightNip = true;
                    }
                }

                if (rightNip == true)
                {
                    break;
                }

                j++;
            }
            return rightNip;
        }

        public void menuUsager()
        {
            Console.WriteLine("\n1- Changer le mot de passe");
            Console.WriteLine("2- Déposer un montant dans un compte");
            Console.WriteLine("3- Retirer un montant d'un compte");
            Console.WriteLine("4- Afficher le solde du compte chèque ou épargne");
            Console.WriteLine("5- Effecter un virement entre les comptes");
            Console.WriteLine("6- Payer une facture");
            Console.WriteLine("7- Fermer session\n");

            string input = Console.ReadLine();
            faireChoix(input);
        }

        public void faireChoix(string input)
        {
            switch (input)
            {
                case "1":
                    changeNip();
                    menuUsager();
                    break;

                case "2":
                    break;

                case "3":
                    break;

                case "4":
                    soldeCompte();
                    menuUsager();
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
                    menuUsager();
                    break;
            }
        }


        //No. 1 du meun usager: Changer le mot de passe
        public void changeNip()
        {
            string nipclient = "";
            while (ChequeActuel.Nip != nipclient && EpargneActuel.Nip != nipclient)
            {
                Console.WriteLine("\nVeuillez sairsir votre mot de passe actuel:");
                nipclient = Console.ReadLine();
            }

            string newnip = "";
            while (newnip.Length != 4)
            {
                Console.WriteLine("\nVeuillez saisir votre nouveau mot de passe (4 charactor): ");
                newnip = Console.ReadLine();
            }

            while (newnip.Equals(nipclient))
            {
                Console.WriteLine("\nVotre mot de passe doit être différent du mont de passe actuel.");
                newnip = Console.ReadLine();
            }

            string newnip2 = "";
            while (newnip != newnip2)
            {
                Console.WriteLine("\nVeuillez confirmer le nouveau mot de passe:");
                newnip2 = Console.ReadLine();
            }

            ChequeActuel.Nip = newnip2;
            EpargneActuel.Nip = newnip2;

        }


        // No.4 du menu usage: Afficher le solde du compte cheque ou du compte epargne
        public void soldeCompte()
        {
            string choix = "";
            while(!(choix=="1" || choix== "2"))
            {
                afficherMenuSolde();
                choix= Console.ReadLine();

                if (choix== "1")
                {
                    string t1 = ChequeActuel.Soldecompte.ToString("C", CultureInfo.CurrentCulture);
                    Console.WriteLine($"Le montant du compte cheque est: {t1}\n");
                }
                else if(choix== "2")
                {
                    string t2 = EpargneActuel.Soldecompte.ToString("C", CultureInfo.CurrentCulture);
                    Console.WriteLine($"Le montant du compte épargne est: {t2}\n");
                }
                
                break;
            }

        }
        public void afficherMenuSolde()
        {
            Console.WriteLine("\n1- Solde du compte chèque");
            Console.WriteLine("2- Solde du compte épargne\n");
        }

        // No.5 du menu usage: Effectuer un virement entre les comptes
        public void virement()
        {
            string c = "";
            while (!(c == "1" || c == "2"))
            {
                Console.WriteLine("\nVeuillez choisir: ");
                Console.WriteLine("1. Du compte cheque à compte épagne");
                Console.WriteLine("2. Du compte épagne à compte cheque\n");
                c = Console.ReadLine();
            }

            switch (c)
            {
                case "1":
                    bool AtoB1 = true;
                    virementAtoB(AtoB1);
                    menuUsager();
                    break;

                case "2":
                    bool AtoB2 = false;
                    virementAtoB(AtoB2);
                    menuUsager();
                    break;

                default:
                    menuUsager();
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
                Console.WriteLine("\nVeuillez saisir le montant:");
                montant = Console.ReadLine();
                bool truenumber = Double.TryParse(montant, out corretmontant);
                corretmontant = Math.Round(corretmontant, 2);

                if (truenumber == false)
                {
                    continue;
                }

                else if (corretmontant < 0)
                {
                    Console.WriteLine("\nVeuillez entre le montant valide.");
                }

                else if(corretmontant>=0 && corretmontant <= 1000)
                {
                   t = true;
                }

                else if(corretmontant > 1000)
                {
                    if (validerNip() == false)
                    {
                        afficherErreur();
                        fermerSession();
                        break;
                    }
                }

            }
            return corretmontant;
        }

        public bool montantEtSoldeGuichet(double montant)
        {
            bool T = true;
            if (montant > Soldeguichet)
            {
                T = false;
                modepanne(T);
            }
            return T;
        }

        public bool validerSoldeCompteCheque(double montant)
        {
            bool T = true;
            while(montant > ChequeActuel.Soldecompte)
            {
                Console.WriteLine("Le solde compte n'est pas sufffisant.");
                montant=validationMontant();
                T = false;
            }
            return T;
        }

        public bool validerSoldeCompteEpargne(double montant)
        {
            bool T = true;
            while (montant > EpargneActuel.Soldecompte)
            {
                Console.WriteLine("Le solde compte n'est pas sufffisant.");
                montant = validationMontant();
                T = false;
            }
            return T;
        }

        public void virementAtoB(bool AtoB)
        {
            double montant = validationMontant();

            if (AtoB == true)
            {
                if (validerSoldeCompteCheque(montant) == true)
                {
                    string t = montant.ToString("C", CultureInfo.CurrentCulture);
                    Console.WriteLine($"Le montant du compte chèque à compte épargne est: {t}\n");
                    ChequeActuel.Soldecompte = ChequeActuel.Soldecompte - montant;
                    EpargneActuel.Soldecompte = EpargneActuel.Soldecompte + montant;

                }
                
            }
            else if (validerSoldeCompteEpargne(montant)==true)
            {
                string t = montant.ToString("C", CultureInfo.CurrentCulture);
                Console.WriteLine($"Le montant du compte Éaprgne à compte chèque est: {t}\n");
                EpargneActuel.Soldecompte = EpargneActuel.Soldecompte - montant;
                ChequeActuel.Soldecompte = ChequeActuel.Soldecompte + montant;
            }

            string t1= ChequeActuel.Soldecompte.ToString("C", CultureInfo.CurrentCulture);
            string t2 = EpargneActuel.Soldecompte.ToString("C", CultureInfo.CurrentCulture);
            Console.WriteLine($"Le solde de compte cheque: {t1}\nLe solde de compteÉaprgne: {t2} ");
        }

        // No.7 du menu usage: Fermer session
        public void fermerSession()
        {
            ChequeActuel = null;
            EpargneActuel = null;
            menuPrincipal();
        }

        public void afficherErreur()
        {
            Console.WriteLine("\nVotre compte est vérouillé\n");
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
                Console.WriteLine("\nVeuillez saisir votre nom:");
                nom = Console.ReadLine();
                Console.WriteLine("Veuillez saisir le mot de passe:");
                nip = Console.ReadLine();
            }
        }
    }  
    
}
