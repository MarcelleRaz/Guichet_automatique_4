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
        private Utilisateur userActuel;
        private double soldeguichet = 10000d;
        private bool panne;
        //private List<Utilisateur> listUsers;
        private ArrayList listUsers;
        private CompteAdmin compteAdmin = new CompteAdmin("admin", "123456");
        bool useradmin;
        bool userclient;



        public Guichet()
        {
            Utilisateur user1 = new Utilisateur("Xin_Wang", "1234", new CompteCheque("ch0001", 10000.00), new CompteEpargne("ep0001", 2500.36), true);
            Utilisateur user2 = new Utilisateur("Fatemeh1", "1998", new CompteCheque("ch0002", 589.12), new CompteEpargne("ep0002", 68452.23), true);
            Utilisateur user3 = new Utilisateur("Marcelle", "9874", new CompteCheque("ch0003", 7896.10), new CompteEpargne("ep0003", 745.23), true);
            Utilisateur user4 = new Utilisateur("PierreLi", "6541", new CompteCheque("ch0004", 1400.25), new CompteEpargne("ep0004", 10000.20), true);
            Utilisateur user5 = new Utilisateur("PatrickR", "9856", new CompteCheque("ch0005", 7500.54), new CompteEpargne("ep0005", 20000.65), true);

            // ListUsers1 = new List<Utilisateur>();
            ListUsers = new ArrayList();
            ListUsers.Add(user1);
            ListUsers.Add(user2);
            ListUsers.Add(user3);
            ListUsers.Add(user4);
            ListUsers.Add(user5);

            this.panne = false;
            useradmin = false;
            userclient = false;

            this.Soldeguichet = Soldeguichet;
        }

        public double Soldeguichet { get => soldeguichet; set => soldeguichet = value; }
        public bool Panne { get => panne; set => panne = value; }
        //internal List<Utilisateur> ListUsers { get => ListUsers1; set => ListUsers1 = value; }

        internal CompteAdmin CompteAdmin { get => compteAdmin; set => compteAdmin = value; }

        public Utilisateur UserActuel { get => userActuel; set => userActuel = value; }
        public bool Useradmin { get => useradmin; set => useradmin = value; }
        public bool Userclient { get => userclient; set => userclient = value; }
        public ArrayList ListUsers { get => listUsers; set => listUsers = value; }

        public void ouvrirguichet(double Soldeguichet)
        {
            if (Soldeguichet <= 0)
            {
                Panne = true;
                modepanne();
                menuPrincipal();
            }
            else
            {
                Panne = false;
                menuPrincipal();
            }
        }

        public void modepanne()
        {
            string seulchoix;

            do
            {
                Console.WriteLine("Le système ne peut pas se connecter à votre compte. Veuillez demander le administrateur.\n");
                menuPrincipal();
                seulchoix = Console.ReadLine();
            } while (seulchoix == "2");


        }

        public void menuPrincipal()
        {
            Console.WriteLine("\nVeuillez choisir l'une des actions suivantes:");
            Console.WriteLine("1- Se connecter à votre compte");
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

        //No. 1 du menu principal:Se connecter à votre compte
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
                if (userActuel.Activation == false)
                {
                    menuPrincipal();
                }
                menuUsager();
            }
        }

        public bool validerNomNip()
        {

            bool rightNomNip = false;
            int j = 0;

            do
            {
                Console.WriteLine("\nVeuillez saisir votre nom en 8 caractères:");
                string nomclient = Console.ReadLine();
                Console.WriteLine("Veuillez saisir votre mon de passe en 4 caractère:");
                string nipclient = Console.ReadLine();

                foreach (Utilisateur utilisateur in ListUsers)
                {
                    if (nomclient.Equals(utilisateur.Nom) && nipclient.Equals(utilisateur.Nip))
                    {
                        userActuel = utilisateur;
                        rightNomNip = true;
                        break;
                    }
                }

                if (rightNomNip == false)
                {
                    j++;
                    Console.WriteLine("\nUne des valeurs n'est pas valide.");
                }
            } while (j < 3 && rightNomNip == false);

            return rightNomNip;
        }

        public bool validerNip()
        {

            bool rightNip = false;
            int j = 0;
            do
            {
                Console.WriteLine("Veuillez saisir votre mon de passe en 4 caractère:");
                string nipclient = Console.ReadLine();
                foreach (Utilisateur utilisateur in ListUsers)
                {
                    if (nipclient.Equals(utilisateur.Nip))
                    {
                        userActuel = utilisateur;
                        rightNip = true;
                        break;
                    }
                }
                if (rightNip == false)
                {
                    j++;
                    Console.WriteLine("\nUne des valeurs n'est pas valide.");
                }

            } while (j < 3 && rightNip == false);
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
                    deposerArgent();
                    menuUsager();
                    break;

                case "3":
                    retirerArgent();
                    menuUsager();
                    break;

                case "4":
                    soldeCompte();
                    menuUsager();
                    break;

                case "5":
                    virement();
                    menuUsager();
                    break;

                case "6":
                    payerFacture();
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
            while (userActuel.Nip != nipclient)
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

            userActuel.Nip = newnip2;

        }

        //No.2 du menu usager: Déposer un montant dans un compte
        public void deposerArgent()
        {
            string compteOption = "";

            while (!(compteOption == "1" || compteOption == "2"))

            {
                Console.WriteLine("Veuillez choisir un compte à déposer: ");
                Console.WriteLine("1. Compte chèque   2. Compte épargne");
                compteOption = Console.ReadLine();
            }

            Console.WriteLine("\nVeuillez saisir le montant:");
            double montant = validationMontant(Console.ReadLine());

            if (compteOption == "1")
            {
                userActuel.Chequeactuel.Soldecompte = userActuel.Chequeactuel.Soldecompte + montant;
                Console.WriteLine($"Le montant du compte cheque est: {afficherRightType(userActuel.Chequeactuel.Soldecompte)}\n");
            }
            else
            {
                userActuel.Epargneactuel.Soldecompte = userActuel.Epargneactuel.Soldecompte + montant;
                Console.WriteLine($"Le montant du compte épargne est: {afficherRightType(userActuel.Epargneactuel.Soldecompte)}\n");
            }

        }

        //No.3 du menu usager: Retirer un montant d'un compte
        public void retirerArgent()
        {
            string compteOption = "";

            while (!(compteOption == "1" || compteOption == "2"))

            {
                Console.WriteLine("Veuillez choisir un compte à déposer: ");
                Console.WriteLine("1. Compte chèque   2. Compte épargne");
                compteOption = Console.ReadLine();
            }

            Console.WriteLine("\nVeuillez saisir le montant:");
            double montant = validationMontant(Console.ReadLine());

            if (montantEtSoldeGuichet(montant) == true)
            {
                modepanne();
            }

            if (compteOption == "1")
            {
                while(siSoldeCompteSuffisant(montant, userActuel.Chequeactuel)==false)
                {
                    Console.WriteLine("Le solde compte n'est pas sufffisant.");
                    Console.WriteLine("Entrez le nouveau montant:");
                    montant = validationMontant(Console.ReadLine());
                }
                userActuel.Chequeactuel.Soldecompte = userActuel.Chequeactuel.Soldecompte - montant;
                Console.WriteLine($"Le montant du compte cheque est: {afficherRightType(userActuel.Chequeactuel.Soldecompte)}\n");
            }
            else if (compteOption == "2")
            {
                while (siSoldeCompteSuffisant(montant, userActuel.Epargneactuel) == false)
                {
                    Console.WriteLine("Le solde compte n'est pas sufffisant.");
                    Console.WriteLine("Entrez le nouveau montant:");
                    montant = validationMontant(Console.ReadLine());
                }
                userActuel.Epargneactuel.Soldecompte = userActuel.Epargneactuel.Soldecompte - montant;
                Console.WriteLine($"Le montant du compte épargne est: {afficherRightType(userActuel.Epargneactuel.Soldecompte)}\n");
            }

            Soldeguichet = Soldeguichet - montant;
        }

        // No.4 du menu usage: Afficher le solde du compte cheque ou du compte epargne
        public void soldeCompte()
        {
            string choix = "";
            while (!(choix == "1" || choix == "2"))
            {
                afficherMenuSolde();
                choix = Console.ReadLine();

                if (choix == "1")
                {
                    Console.WriteLine($"Le montant du compte cheque est: {afficherRightType(userActuel.Chequeactuel.Soldecompte)}\n");
                }
                else if (choix == "2")
                {
                    Console.WriteLine($"Le montant du compte épargne est: {afficherRightType(userActuel.Epargneactuel.Soldecompte)}\n");
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
            string virementOption = "";
            while (!(virementOption == "1" || virementOption == "2"))
            {
                Console.WriteLine("\nVeuillez choisir: ");
                Console.WriteLine("1. Du compte cheque à compte épagne");
                Console.WriteLine("2. Du compte épagne à compte cheque\n");
                virementOption = Console.ReadLine();
            }

            switch (virementOption)
            {
                case "1":
                    virementAtoB(userActuel.Chequeactuel, UserActuel.Epargneactuel);
                    break;

                case "2":
                    virementAtoB(userActuel.Epargneactuel, UserActuel.Chequeactuel);
                    break;

                default:
                    break;
            }
            menuUsager();
        }

        //pour afficher en type xx xxx.xx$
        public string afficherRightType(double montant)
        {
            return montant.ToString("C", CultureInfo.CurrentCulture);
        }

        public double validationMontant(string montant)
        {
            bool isMontantValid;
            double montantValide;
            do
            {
                isMontantValid = Double.TryParse(montant, out montantValide);

                if (isMontantValid)
                {
                    montantValide = Math.Round(montantValide, 2);
                    if (montantValide < 0)
                    {
                        isMontantValid = false;
                    }
                    else if (montantValide > 1000)
                    {
                        if (validerNip() == false)
                        {
                            afficherErreur();
                            userActuel.Activation = false;
                            fermerSession();
                            break;
                        }
                    }
                }

                if (!isMontantValid)
                {
                    Console.WriteLine("\nVeuillez entre un montant valide.");
                    montant = Console.ReadLine();
                }

            } while (!isMontantValid);

            return montantValide;
        }

        //Valider si montant plus que le solde du guichet
        public bool montantEtSoldeGuichet(double montant)
        {
            bool T = false;
            if (montant > Soldeguichet)
            {
                T = true;
            }
            return T;
        }

        public bool siSoldeCompteSuffisant(double montantDebite, CompteClient compteDebite)
        {
            return (montantDebite <= compteDebite.Soldecompte);
        }

        public void virementAtoB(CompteClient debiteur, CompteClient crediteur)
        {
            Console.WriteLine("Veuillez saisir le montant de virement:");
            double montant = validationMontant(Console.ReadLine());

            while (!siSoldeCompteSuffisant(montant, debiteur))
            {
                Console.WriteLine("Le solde compte n'est pas sufffisant.");
                Console.WriteLine("Entrez le nouveau montant:");
                montant = validationMontant(Console.ReadLine());
            }


            Console.WriteLine($"Le montant du compte {(debiteur is CompteCheque ? "cheque" : "epargne")} à compte " +
                $"{(crediteur is CompteCheque ? "cheque" : "epargne")} est: {afficherRightType(montant)}\n");

            debiteur.Soldecompte = debiteur.Soldecompte - montant;
            crediteur.Soldecompte = crediteur.Soldecompte + montant;

            Console.WriteLine($"Le solde du compte {(debiteur is CompteCheque ? "cheque" : "epargne")}: {afficherRightType(debiteur.Soldecompte)}\n" +
                $"Le solde du compte {(crediteur is CompteCheque ? "cheque" : "epargne")}: {afficherRightType(crediteur.Soldecompte)} ");
        }

        //No.6 du menu usager: Payer une facture
        public void payerFacture()
        {
            string choix = "";
            double ch = userActuel.Chequeactuel.Soldecompte;
            double ep = userActuel.Epargneactuel.Soldecompte;

            while (!(choix == "1" || choix == "2" || choix == "3"))
            {
                Console.WriteLine("Veuillez choisir un fournisseur:");
                Console.WriteLine("1. Amazon ");
                Console.WriteLine("2. Bell");
                Console.WriteLine("3. Vidéotron");
                choix = Console.ReadLine();
            }

            string compteOption = "";
            while (!(compteOption == "1" || compteOption == "2"))
            {
                Console.WriteLine("Veuillez choisir de quel compte:");
                Console.WriteLine("1. Compte chèque");
                Console.WriteLine("2. Compte épargne");
                compteOption = Console.ReadLine();
            }
            

            Console.WriteLine("Entrez le montant de la facture:");
            double montant = validationMontant(Console.ReadLine());

            if (compteOption == "1")
            {
                while (siSoldeCompteSuffisant(montant, userActuel.Chequeactuel) == false)
                {
                    Console.WriteLine("Le solde compte n'est pas sufffisant.");
                    Console.WriteLine("Entrez le nouveau montant:");
                    montant = validationMontant(Console.ReadLine());
                }

                userActuel.Chequeactuel.Soldecompte = userActuel.Chequeactuel.Soldecompte - montant;
                Console.WriteLine($"Le montant du compte cheque est: {afficherRightType(userActuel.Chequeactuel.Soldecompte)}\n");
            }
            else if (compteOption == "2")
            {
                while (siSoldeCompteSuffisant(montant, userActuel.Epargneactuel) == false)
                {
                    Console.WriteLine("Le solde compte n'est pas sufffisant.");
                    Console.WriteLine("Entrez le nouveau montant:");
                    montant = validationMontant(Console.ReadLine());
                }
                userActuel.Epargneactuel.Soldecompte = userActuel.Epargneactuel.Soldecompte - montant;
                Console.WriteLine($"Le montant du compte épargne est: {afficherRightType(userActuel.Epargneactuel.Soldecompte)}\n");
            }
            
        }

        // No.7 du menu usage: Fermer session
        public void fermerSession()
        {
            //ChequeActuel = null;
            //EpargneActuel = null;
            menuPrincipal();
        }

        public void afficherErreur()
        {
            Console.WriteLine("\nVotre compte est vérouillé\n");
        }


        // No.2 du menu principal:Se connecter comme administrateur
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
