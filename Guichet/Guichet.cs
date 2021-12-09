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
            }
            else
            {
                Panne = false;
                menuPrincipal();
            }
        }

        public void modepanne()
        {
            string seulchoix = "";

            while (seulchoix != "2")
            {
                Console.WriteLine("Le système ne peut pas se connecter à votre compte. Veuillez demander le administrateur.\n");
                menuPrincipal();
                seulchoix = Console.ReadLine();
            }

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
                    accesComptAdmin();
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
            if (panne == true)
            {
                modepanne();
            }
            else if (validerNomNip() == false)
            {
                afficherErreur();
                fermerSession();
                menuPrincipal();
            }
            else if (userActuel.Activation == false)
            {
                menuPrincipal();
            }
            menuUsager();
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
                Console.WriteLine("Veuillez saisir votre mot de passe en 4 caractère:");
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
                    menuUsager();
                    break;

                case "7":
                    fermerSession();
                    menuPrincipal();
                    break;

                default:
                    menuUsager();
                    break;
            }

        }


        //No. 1 du meun usager: Changer le mot de passe
        public void changeNip()
        {
            if (userActuel.Activation == false || panne == true)
            {
                menuPrincipal();
            }
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
            if (userActuel.Activation == false || panne == true)
            {
                menuPrincipal();
            }
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
            if (userActuel.Activation == false || panne == true)
            {
                menuPrincipal();
            }
            string compteOption = "";

            while (!(compteOption == "1" || compteOption == "2"))

            {
                Console.WriteLine("Veuillez choisir un compte à retirer: ");
                Console.WriteLine("1. Compte chèque   2. Compte épargne");
                compteOption = Console.ReadLine();
            }

            Console.WriteLine("\nVeuillez saisir le montant:");
            double montant = validationMontant(Console.ReadLine());

            if (montantEtSoldeGuichet(montant) == true)
            {
                modepanne();
            }
            else if (compteOption == "1")
            {
                while (siSoldeCompteSuffisant(montant, userActuel.Chequeactuel) == false)
                {
                    Console.WriteLine("Le solde compte n'est pas sufffisant.");
                    Console.WriteLine("Entrez le nouveau montant:");
                    montant = validationMontant(Console.ReadLine());
                }
                userActuel.Chequeactuel.Soldecompte = userActuel.Chequeactuel.Soldecompte - montant;
                Console.WriteLine($"Le montant du compte cheque est: {afficherRightType(userActuel.Chequeactuel.Soldecompte)}\n");
                Soldeguichet = Soldeguichet - montant;
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
                Soldeguichet = Soldeguichet - montant;
            }

        }

        // No.4 du menu usage: Afficher le solde du compte cheque ou du compte epargne
        public void soldeCompte()
        {
            if (userActuel.Activation == false || panne == true)
            {
                menuPrincipal();
            }
            string choix = "";
            while (!(choix == "1" || choix == "2"))
            {
                Console.WriteLine("\n1- Solde du compte chèque");
                Console.WriteLine("2- Solde du compte épargne\n");
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

        // No.5 du menu usage: Effectuer un virement entre les comptes
        public void virement()
        {
            if (userActuel.Activation == false || panne == true)
            {
                menuPrincipal();
            }
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
                    else if (montantValide >= 1000 && montantValide <= 10000)
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
            if (montant > Soldeguichet)
            {
                panne = true;
            }
            return panne;
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
            if (userActuel.Activation == false || panne == true)
            {
                menuPrincipal();
            }
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
            userActuel = null;
        }

        public void afficherErreur()
        {
            Console.WriteLine("\nVotre compte est vérouillé\n");
        }

        public void accesComptAdmin()
        {
            Console.WriteLine("\nBienvenue sur le compte Administrateur");
            Console.WriteLine("Veuillez saisir vos informations:");
            Console.WriteLine("Nom d'utilisateur:\n");
            string nameadmin = Console.ReadLine();
            Console.WriteLine("Mot de passe:\n");
            string nipadmin = Console.ReadLine();
            verificationAccesadm(nameadmin, nipadmin);
        }
        public void verificationAccesadm(string namead, string nipad)
        {
            int i = 1;
            while (i <= 3)
            {
                if (i == 3)
                {
                    panne = true;
                    modepanne();
                    break;
                }
                if (namead.Equals("admin") && nipad.Equals("123456"))
                {
                    menuAdmin();
                    break;
                }
                else
                {
                    Console.WriteLine("La combinaison 'Nom d'utilisateur et NIP' n'est pas reconnue.\nVeuillez saisir votre nom d'utilisateur et nip.");
                    Console.WriteLine("Nom d'utilisateur:\n");
                    namead = Console.ReadLine();
                    Console.WriteLine("Mot de passe:\n");
                    nipad = Console.ReadLine();
                }
                i++;
            }
        }
        public void menuAdmin()
        {
            Console.WriteLine("\n1- Remettre le guichet en fonction");
            Console.WriteLine("2- Déposer de l'argent dans le guichet");
            Console.WriteLine("3- Voir le solde du guichet");
            Console.WriteLine("4- Voir la liste des comptes");
            Console.WriteLine("5- Retourner au menu principal\n");
            Console.WriteLine("Veuillez choisir votre action parmi les numéros ci-dessus:\n");
            choiceAdmin();
        }
        public void choiceAdmin()
        {
            string choice = Console.ReadLine();
            if (panne == true)
            {
                gestionPanne();
            }
            else
            {
                switch (choice)
                {
                    case "1":
                        remiseFonction();
                        break;
                    case "2":
                        depotGuichet();
                        menuAdmin();
                        break;
                    case "3":
                        soldeGuichet();
                        menuAdmin();
                        break;
                    case "4":
                        voirlistCompte();
                        menuAdmin();
                        break;
                    case "5":
                        retourMenuppl();
                        break;
                    default:
                        Console.WriteLine("Veuillez choisir un bon numéro parmi les actions ci-dessus.\n");
                        break;
                }
            }
        }

        public void gestionPanne()
        {
            string choice = Console.ReadLine();
            while (panne == true)
            {
                switch (choice)
                {
                    case "1":
                        remiseFonction();
                        break;
                    default:
                        panne = true;
                        modepanne();
                        Console.WriteLine("Veuillez contacter un admnistrateur pour remettre en fonction le guichet.");
                        menuAdmin();
                        break;
                }
            }
        }

        //No. 1 du menu adminisrateur: Remettre le guichet en fonction
        public void remiseFonction()
        {
            Console.WriteLine("Désirez-vous remettre le système en fonction? Saisir O pour 'oui' et N pour 'Non'\n");
            string response = Console.ReadLine();

            while (!response.Equals("O") && !response.Equals("N"))
            {
                Console.WriteLine("Réponse érronée. Veuillez saisir O pour 'oui' et N pour 'Non'\n");
                response = Console.ReadLine();
            }
            if (response.Equals("O"))
            {
                Panne = false;
                menuPrincipal();
            }
            if (response.Equals("N"))
            {
                modepanne();
            }

        }

        //No. 2 du menu admin: Déposer de l'argent dans le guichet
        public double depotGuichet()
        {

            Console.WriteLine("Veuillez saisir le montant à déposer: \n");
            double depot = validationMontantAdmin(Console.ReadLine());
            while (depot >= 10000d)
            {
                Console.WriteLine("Le montant maximum de dépot est de 10 000,00$.\nVeuillez saisir un nouveau montant à déposer.\n");
                depot = Convert.ToDouble(Console.ReadLine());
            }
            soldeguichet += depot;
            soldeGuichet();
            return (depot);
        }
        public double validationMontantAdmin(string montant)
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
                }
                if (!isMontantValid)
                {
                    Console.WriteLine("\nVeuillez entre un montant valide.");
                    montant = Console.ReadLine();
                }

            } while (!isMontantValid);

            return montantValide;
        }

        //No.3 du munu admin: Voir le solde du guichet
        public void soldeGuichet()
        {
            string soldeG = soldeguichet.ToString("C", CultureInfo.CurrentCulture);
            Console.WriteLine("Le solde du guichet est de: {0}", soldeG);
        }

        // No.4 du menu admin: Voir la liste des comptes
        public void voirlistCompte()
        {
            Console.WriteLine("Utilisateur" + "\t" + "NIP" + "\t" + "Chèque" + "\t" + "Solde" + "\t" + "\t" + "Epargne" + "\t" + "Solde" + "\t" + "\t" + "Activation");

            foreach (Utilisateur user in listUsers)
            {
                Console.WriteLine(user.Nom + "\t" + user.Nip + "\t" + user.Chequeactuel.Nom + "\t" + user.Chequeactuel.Soldecompte + "\t" + "\t" + user.Epargneactuel.Nom + "\t" + user.Epargneactuel.Soldecompte + "\t" + "\t" + user.Activation);
            }
        }

        //No. 5 du menu admin: Retourner au menu principal
        public void retourMenuppl()
        {
            useradmin = false;
            menuPrincipal();
        }
    }

}
