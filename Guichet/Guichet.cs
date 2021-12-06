﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Guichet
{
    public class Guichet
    {
        private double soldeguichet;
        private bool panne;
        private static ArrayList listCompte;
        bool useradmin;
        Administrateur admin = new Administrateur();
        Utilisateur user1 = new Utilisateur("Xin_Wang", "1234", new CompteCheque("ch0001", 20000.00), new CompteEpargne("ep0001",2500.36),true);
        Utilisateur user2 = new Utilisateur("Fatemeh1", "1998", new CompteCheque("ch0002",589.12), new CompteEpargne("ep0002",68452.23),true);
        Utilisateur user3 = new Utilisateur("Marcelle", "9874", new CompteCheque("ch0003",7896.10), new CompteEpargne("ep0003",745.23),true);
        Utilisateur user4 = new Utilisateur("PierreLi", "6541", new CompteCheque("ch0004",1400.25), new CompteEpargne("ep0004",10000.20),true);
        Utilisateur user5 = new Utilisateur("PatrickR", "9856", new CompteCheque("ch0005",7500.54), new CompteEpargne("ep0005",20000.65),true);
        public Guichet()
        {
            this.soldeguichet = 10000d;
            listCompte = new ArrayList();
            listCompte.Add(user1);
            listCompte.Add(user2);
            listCompte.Add(user3);
            listCompte.Add(user4);
            listCompte.Add(user5);
            admin.NomAdmin = "admin";
            admin.NipAdmin = "123456";
            this.panne = false;
            useradmin = false;

        }
        public double Soldeguichet { get => soldeguichet; set => soldeguichet = value; }
        public bool Panne { get => panne; set => panne = value; }
        public static ArrayList ListCompte { get => listCompte; set => listCompte = value; }
        public bool Useradmin { get => useradmin; set => useradmin = value; }
        public void startMachine()
        {
            bool start = true;
            while(start == true)
            {
                menuprincipale();
            }
        }    
        //SECTION GUICHET
        public void ouvrirguichet()
        {
            bool Panne=false;
            if (soldeguichet <= 0)
            {
                Panne = true;
                modepanne(Panne);
            }
        }
        public static void modepanne(bool Panne)
        {
            if (Panne == true)
            {
                Console.WriteLine("Le système est en panne. Veuillez demander un administrateur.");
            }
        }
        public void menuprincipale()
        {
            ouvrirguichet();
            Console.WriteLine();
            Console.WriteLine("Veuillez choisir l'une des actions suivantes:");
            Console.WriteLine("1- Se connecter à vore compte");
            Console.WriteLine("2- Se connecter comme adimnistrateur");
            Console.WriteLine("3- Quitter");
            choisirMenuppl();
        }
        public string choisirMenuppl()
        {
            string menuchoice =Console.ReadLine();
            switch (menuchoice)
            {
                case "1":
                    accesComptClient();
                    break;

                case "2":
                    useradmin = true;
                    accesComptAdmin();
                    break;

                case "3":
                    System.Environment.Exit(0);
                    break;

                default:
                    menuprincipale();
                    break;
            }
            return menuchoice;
        }
        //SECTION UTILISATEUR
        public void accesComptClient()
        {
            Console.WriteLine("******************************************************************************************************");
            Console.WriteLine("Bienvenue sur notre Guichet Automatique");
            Console.WriteLine("Veuillez saisir vos informations:");
            Console.WriteLine("Nom d'utilisateur:\n");
            string name = Console.ReadLine();
            Console.WriteLine("Mot de passe:\n");
            string mdp = Console.ReadLine();
            Utilisateur temporaire=verifAccesClient( name, mdp);
        }
        public Utilisateur verifAccesClient( string name, string mdp)
        {
            Utilisateur tempo = null;
            int x = 1;
            bool verification = true;
            while (tempo == null)
            {
                foreach (Utilisateur user in listCompte)
                {
                    if (tempo == null && user.Nom.Equals(name) && user.Nip.Equals(mdp))
                    {
                        tempo = user;
                    }
                    if ((tempo == null) && (user.Nom.Equals(name) && !user.Nip.Equals(mdp)))
                    {
                        tempo = user;
                        verification = false;
                        x++;
                    }
                    
                }
                while (verification == false && x <= 3)
                {
                    verification = true;
                    tempo = null;
                    Console.WriteLine("La combinaison de nom d'utilisateur et de nip ne correspond pas à un compte. Veuillez ressaisir vos informations.");
                    Console.WriteLine("Nom d'utilisateur:\n");
                    name = Console.ReadLine();
                    Console.WriteLine("Mot de passe:\n");
                    mdp = Console.ReadLine();
                }

                if (tempo != null && verification == false && x>3)
                {
                    tempo.Activation = false;
                    tempo.modeverrouillage(tempo.Activation);
                }
                if (tempo != null && verification == true)
                {
                    MenuUsager(tempo);
                }
            }
            return tempo;
        }
        public void gestionverrouillage(Utilisateur user)
        {
            while (user != null && user.Activation == false)
            {
                Console.WriteLine("Veuillez contacter un admnistrateur pour déverrouiller votre compte.");
                break;
            }
        }
        public void MenuUsager(Utilisateur user)
        {
            Console.WriteLine("******************************************************************************************************");
            Console.WriteLine("1-Changer le mot de passe");
            Console.WriteLine("2-Déposer un montant dans un compte");
            Console.WriteLine("3-Retirer un montant du compte");
            Console.WriteLine("4-Afficher le solde du compte chèque ou épargne");
            Console.WriteLine("5-Effectuer un virement entre les comptes");
            Console.WriteLine("6-Payer une facture");
            Console.WriteLine("7-Fermer la session");
            Console.WriteLine("******************************************************************************************************");
            choiceUsager(user);
        }
        public void choiceUsager(Utilisateur user)
        {
            string choice = Console.ReadLine();
            bool erreur = false;
            if (user != null && user.Activation == false)
            {
                gestionverrouillage(user);
            }
            else if (user != null && user.Activation == true)
            {
                switch (choice)
                {
                    case "1":
                        changeNip(user);
                        break;
                    case "2":
                        menudepot(user);
                        break;
                    case "3":
                        menuretrait(user);
                        break;
                    case "4":
                        menusolde(user);
                        break;
                    case "5":
                        menuvirement(user);
                        break;
                    case "6":
                        menupaiement(user);
                        break;
                    case "7":
                        fermerSession();
                        break;
                    default:
                        erreur = true;
                        erreurChoixuser(choice, user, erreur);
                        break;
                }
            }
        }
        public void erreurChoixuser(string choice, Utilisateur user, bool erreur)
        {
            while (erreur == true)
            {
                erreur = false;
                Console.WriteLine("Veuillez choisir parmi les numéros ci-dessus.");
            }
            choiceUsager(user);
        }
        public void changeNip(Utilisateur user)
        {
            string nipclient = "";
            while (user.Nip != nipclient)
            {
                Console.WriteLine("\nVeuillez saisir votre mot de passe actuel:");
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

            user.Nip = newnip2;
        }
        public void menudepot(Utilisateur user)
        {
            Console.WriteLine("Veuillez choisir dans quel compte vous voulez faire le dépôt:");
            Console.WriteLine("1- Compte chèque");
            Console.WriteLine("2- Compte épargne");
            Console.WriteLine("Saisir 1 pour le compte chèque et 2 pour le compte épargne.");
            choixdepot(user);
        }
        public void choixdepot(Utilisateur user)
        {
            string choice = Console.ReadLine();
            bool erreur = false;
            CompteClient compte = null;
            switch (choice)
            {
                case "1":
                    depotcheque(user);
                    compte = user.Chequeactuel;
                    break;
                case "2":
                    depotepargne(user);
                    compte = user.Epargneactuel;
                    break;
                default:
                    erreur = true;
                    erreurChoixdepot(choice,user, erreur);
                    break;
            }
        }
        public void erreurChoixdepot(string choice, Utilisateur user, bool erreur)
        {
            while (erreur==true)
            {
                erreur = false;
                Console.WriteLine("Veuillez choisir parmi les numéros ci-dessus.");
            }
            choixdepot(user);
        }
        public double depotcheque(Utilisateur user) 
        {
            Console.WriteLine("Veuillez saisir le montant à déposer: \n");
            string montant = Console.ReadLine();
            while (!montant.All(char.IsDigit)||Convert.ToDouble(montant)<0)
            {
                Console.WriteLine("Il y a erreur dans le montant entré. Veuillez saisir votre montant.");
                montant = Console.ReadLine();
            }
            double depot = Convert.ToDouble(montant);
            user.Chequeactuel.Solde += depot;
            double solde = user.Chequeactuel.Solde;
            string soldeStr = solde.ToString("C", CultureInfo.CurrentCulture);
            string depotStr = depot.ToString("C", CultureInfo.CurrentCulture);
            Console.WriteLine("Vous avez déposé {0} dans votre compte épargne", depotStr, "$ .");
            Console.WriteLine("Votre solde du compte épargne est de {0}", soldeStr, "$ .");
            Console.WriteLine("******************************************************************************************************");
            MenuUsager(user);
            return solde;
        }
        public double depotepargne(Utilisateur user)
        {
            Console.WriteLine("Veuillez saisir le montant à déposer: \n");
            string montant = Console.ReadLine();
            while (!montant.All(char.IsDigit) || Convert.ToDouble(montant) < 0)
            {
                Console.WriteLine("Il y a erreur dans le montant entré. Veuillez saisir votre montant.");
                montant = Console.ReadLine();
            }
            double depot = Convert.ToDouble(montant);
            user.Epargneactuel.Solde += depot;
            double solde = user.Epargneactuel.Solde;
            string soldeStr = solde.ToString("C", CultureInfo.CurrentCulture);
            string depotStr= depot.ToString("C", CultureInfo.CurrentCulture);
            Console.WriteLine("Vous avez déposé {0} dans votre compte épargne", depotStr,"$ .");
            Console.WriteLine("Votre solde du compte épargne est de {0}", soldeStr, "$ .");
            Console.WriteLine("******************************************************************************************************");
            MenuUsager(user);
            return solde;
        }
        public void menuretrait(Utilisateur user)
        {
            Console.WriteLine("Veuillez choisir dans quel compte vous voulez faire le retrait:");
            Console.WriteLine("1- Compte chèque");
            Console.WriteLine("2- Compte épargne");
            Console.WriteLine("Saisir 1 pour le compte chèque et 2 pour le compte épargne.");
            choixretrait(user);
        }
        public void choixretrait(Utilisateur user)
        {
            string choice = Console.ReadLine();
            bool erreur = false;
            switch (choice)
            {
                case "1":
                    retraitcheque(user);
                    break;
                case "2":
                    retraitepargne(user);
                    break;
                default:
                    erreur = true;
                    erreurChoixretrait(choice, user, erreur);
                    break;
            }
        }
        public void erreurChoixretrait(string choice, Utilisateur user, bool erreur)
        {
            while (erreur == true)
            {
                erreur = false;
                Console.WriteLine("Veuillez choisir parmi les numéros ci-dessus.");
            }
            choixretrait(user);
        }
       
        public double montantretrait(Utilisateur user)
        {
            Console.WriteLine("Veuillez saisir le montant à retirer: \n");
            string montant = Console.ReadLine();
            while (!montant.All(char.IsDigit) || Convert.ToDouble(montant) < 0)
            {
                Console.WriteLine("Il y a erreur dans le montant entré. Veuillez saisir votre montant.");
                montant = Console.ReadLine();
            }
            double retrait = Convert.ToDouble(montant);
            return retrait;
        }
        public double fondinsuffisantretrait(Utilisateur user)
        {
            Console.WriteLine("Vos fonds sont insuffisants. Voulez-vous retourner au menu ou changer le montant de la transaction?");
            Console.WriteLine("1- Retourner au menu utilisateur");
            Console.WriteLine("2- Changer le montant à transférer");
            string choice = Console.ReadLine();
            bool erreur = false;
            double retrait = 0d;
            switch (choice)
            {
                case "1":
                    MenuUsager(user);
                    break;
                case "2":
                    retrait=montantretrait(user);
                    break;
                default:
                    erreur = true;
                    erreurfondretrait(choice, user, erreur);
                    break;
            }
            return retrait;
        }
        public double fondinsuffisantGuichet(Utilisateur user)
        {
            Console.WriteLine("Le fond dans le guichet est insuffisant. Voulez-vous retourner au menu ou changer le montant de la transaction?");
            Console.WriteLine("1- Retourner au menu utilisateur");
            Console.WriteLine("2- Changer le montant à transférer");
            string choice = Console.ReadLine();
            bool erreur = false;
            double retrait = 0d;
            switch (choice)
            {
                case "1":
                    MenuUsager(user);
                    break;
                case "2":
                    retrait = montantretrait(user);
                    break;
                default:
                    erreur = true;
                    erreurfondretrait(choice, user, erreur);
                    break;
            }
            return retrait;
        }
        public void erreurfondguichet(string choice, Utilisateur user, bool erreur)
        {
            while (erreur == true)
            {
                erreur = false;
                Console.WriteLine("Veuillez choisir parmi les numéros ci-dessus.");
            }
            fondinsuffisantGuichet(user);
        }
        public void erreurfondretrait(string choice, Utilisateur user, bool erreur)
        {
            while(erreur == true)
            {
                erreur = false;
                Console.WriteLine("Veuillez choisir parmi les numéros ci-dessus.");
            }
            fondinsuffisantretrait(user);
        }
        public double retraitcheque(Utilisateur user)
        {
            double retrait = montantretrait(user);
            while (user.Chequeactuel.Solde < retrait)
            {
                retrait = fondinsuffisantretrait(user);
            }
            while (retrait > soldeguichet)
            {
                retrait = fondinsuffisantGuichet(user);
            }
            user.Chequeactuel.Solde -= retrait;
            soldeguichet -= retrait;
            double solde = user.Chequeactuel.Solde;
            string soldeStr = solde.ToString("C", CultureInfo.CurrentCulture);
            string retraitStr = retrait.ToString("C", CultureInfo.CurrentCulture);
            Console.WriteLine("Vous avez retiré {0} dans votre compte épargne", retraitStr, "$ .");
            Console.WriteLine("Votre solde du compte épargne est de {0}", soldeStr, "$ .");
            Console.WriteLine("******************************************************************************************************");
            MenuUsager(user);
            return retrait;
        }
        public double retraitepargne(Utilisateur user)
        {
            double retrait = montantretrait(user);
            while (user.Epargneactuel.Solde < retrait)
            {
                retrait=fondinsuffisantretrait(user);
            }
            user.Epargneactuel.Solde -= retrait;
            soldeguichet -= retrait;
            double solde = user.Epargneactuel.Solde;
            string soldeStr = solde.ToString("C", CultureInfo.CurrentCulture);
            string retraitStr = retrait.ToString("C", CultureInfo.CurrentCulture);
            Console.WriteLine("Vous avez retiré {0} dans votre compte épargne", retraitStr, "$ .");
            Console.WriteLine("Votre solde du compte épargne est de {0}", soldeStr, "$ .");
            Console.WriteLine("******************************************************************************************************");
            MenuUsager(user);
            return retrait;
        }
        public void menusolde(Utilisateur user)
        {
            Console.WriteLine("Veuillez choisir dans quel compte vous voulez afficher le solde:");
            Console.WriteLine("1- Compte chèque");
            Console.WriteLine("2- Compte épargne");
            Console.WriteLine("Saisir 1 pour le compte chèque et 2 pour le compte épargne.");
            choixsolde(user);
        }
        public void choixsolde(Utilisateur user)
        {
            string choice = Console.ReadLine();
            bool erreur = false;
            CompteClient compte = null;
            switch (choice)
            {
                case "1":
                    soldeCheque(user);
                    compte = user.Chequeactuel;
                    break;
                case "2":
                    soldeEpargne(user);
                    compte = user.Epargneactuel;
                    break;
                default:
                    erreur = true;
                    erreurChoixsolde(choice, user, erreur);
                    break;
            }
        }
        public void erreurChoixsolde(string choice, Utilisateur user, bool erreur)
        {
            while (erreur == true)
            {
                erreur = false;
                Console.WriteLine("Veuillez choisir parmi les numéros ci-dessus.");
            }
            choixsolde(user);
        }
        public void soldeCheque(Utilisateur user)
        {
            string t1 = user.Chequeactuel.Solde.ToString("C", CultureInfo.CurrentCulture);
            Console.WriteLine($"Le montant du compte chèque est: {t1}");
            Console.ReadKey();
            MenuUsager(user);
        }
        public void soldeEpargne(Utilisateur user)
        {
            string t2 = user.Epargneactuel.Solde.ToString("C", CultureInfo.CurrentCulture);
            Console.WriteLine($"Le montant de votre compte épargne est de: {t2}");
            Console.ReadKey();
            MenuUsager(user);
        }
        public void menuvirement(Utilisateur user)
        {
            Console.WriteLine("Veuillez choisir votre transfert:");
            Console.WriteLine("1- Du compte chèque à compte épargne");
            Console.WriteLine("2- Du compte épargne à compte chèque");
            Console.WriteLine("Saisir 1 pour le compte chèque et 2 pour le compte épargne.");
            choixvirement(user);
        }
        public void choixvirement(Utilisateur user)
        {
            string choice = Console.ReadLine();
            bool erreur = false;
            switch (choice)
            {
                case "1":
                    chequeversEpargne(user);
                    break;
                case "2":
                    epargneversCheque(user);
                    break;
                default:
                    erreur = true;
                    erreurChoixvirement(choice, user, erreur);
                    break;
            }
        }
        public void erreurChoixvirement(string choice, Utilisateur user, bool erreur)
        {
            while (erreur == true)
            {
                erreur = false;
                Console.WriteLine("Veuillez choisir parmi les numéros ci-dessus.");
            }
            choixvirement(user);
        }
        public string fondinsuffisantvirement(Utilisateur user)
        {
            Console.WriteLine("Vos fonds sont insuffisants. Voulez-vous retourner au menu ou changer le montant de la transaction?");
            Console.WriteLine("1- Retourner au menu utilisateur");
            Console.WriteLine("2- Changer le montant à transférer");
            string choice = Console.ReadLine();
            bool erreur = false;
            string montant = "";
            switch (choice)
            {
                case "1":
                    MenuUsager(user);
                    break;
                case "2":
                    montant=montantvirement(user);
                    break;
                default:
                    erreur = true;
                    erreurfondvirement(choice, user, erreur);
                    break;
            }
           return montant;
        }
        public void erreurfondvirement(string choice, Utilisateur user, bool erreur)
        {
            while (erreur == true)
            {
                erreur = false;
                Console.WriteLine("Veuillez choisir parmi les numéros ci-dessus.");
            }
            fondinsuffisantvirement(user);
        }
        public string montantvirement(Utilisateur user)
        {
            Console.WriteLine("Veuillez saisir le montant à transférer vers le compte épargne: \n");
            string depot = Console.ReadLine();
            return depot;
        }
        public void chequeversEpargne(Utilisateur user)
        {
            double depot = validationMontant(user);
           while (user.Chequeactuel.Solde < depot)
            {
                fondinsuffisantvirement(user);
            }
            double cheque=user.Chequeactuel.Solde - depot;
            string chequeString=cheque.ToString("C", CultureInfo.CurrentCulture);
            double epargne=user.Epargneactuel.Solde + depot;
            user.Chequeactuel.Solde = cheque;
            user.Epargneactuel.Solde = epargne;
            string epargneString=epargne.ToString("C", CultureInfo.CurrentCulture);
            Console.WriteLine("Le solde de votre compte chèque est de: {0}",chequeString,"$ ." );
            Console.WriteLine("Le solde de votre compte épargne est de: {0}", epargneString, "$ .");
            Console.ReadKey();
            MenuUsager(user);

        }
        public void epargneversCheque(Utilisateur user)
        {
            double depot = validationMontant(user);
            while (user.Epargneactuel.Solde < depot)
            {
                fondinsuffisantvirement(user);
                depot = Convert.ToDouble(validationMontant(user));
            }
            double cheque = user.Chequeactuel.Solde + depot;
            string chequeString = cheque.ToString("C", CultureInfo.CurrentCulture);
            double epargne = user.Epargneactuel.Solde - depot;
            user.Chequeactuel.Solde = cheque;
            user.Epargneactuel.Solde = epargne;
            string epargneString = epargne.ToString("C", CultureInfo.CurrentCulture);
            Console.WriteLine("Le solde de votre compte chèque est de: {0}", chequeString, "$ .");
            Console.WriteLine("Le solde de votre compte épargne est de: {0}", epargneString, "$ .");
            Console.ReadKey();
            MenuUsager(user);
        }
        public double validationMontant(Utilisateur user)
        {
            string montant = montantvirement(user);
            double corretmontant = 0;
            bool t = false;

            while (t == false)
            {
                bool truenumber = Double.TryParse(montant, out corretmontant);
                corretmontant = Math.Round(corretmontant, 2);

                if (truenumber == false)
                {
                    continue;
                }

                else if (corretmontant < 0)
                {
                    Console.WriteLine("\nVeuillez entrer un montant valide.");
                }

                else if (corretmontant >= 0 && corretmontant <= 1000)
                {
                    t = true;
                }

                else if (corretmontant > 1000)
                {
                    if (validerNip(user) == false)
                    {
                        user.Activation = false;
                        user.modeverrouillage(user.Activation);
                        fermerSession();
                    }
                    else
                    {
                      t = true;
                    }
                }

            }
            return corretmontant;
        }
        public bool validerNip(Utilisateur user)
        {
            bool rightNip = false;
            int j = 1;
            Console.WriteLine("\nVeuillez saisir votre mot de passe en 4 caractères:\n");
            string nipclient = Console.ReadLine();
            while (rightNip == false && j < 3) {
                foreach (Utilisateur tempo in listCompte)
                {
                    if (user == tempo && tempo.Nip.Equals(nipclient))
                    {
                        rightNip = true;
                        user = tempo;
                        break;
                    }
                    if (user != tempo && !tempo.Nip.Equals(nipclient))
                    {
                        rightNip = false;
                    }
                }
                if (rightNip == false)
                {
                    Console.WriteLine("Votre mot de passe est erroné. Veuillez ressaisir votre NIP.");
                    nipclient = Console.ReadLine();
                    j++;
                }
             }
            return rightNip;
        }
        public string choixFournisseur(Utilisateur user)
        {
            string choix= "";
            double ch = user.Chequeactuel.Solde;
            double ep = user.Epargneactuel.Solde;

            while (!(choix == "1" || choix == "2" || choix == "3"))
            {
                Console.WriteLine("Veuillez choisir parmis ces fournisseurs:");
                Console.WriteLine("1. Amazon ");
                Console.WriteLine("2. Bell");
                Console.WriteLine("3. Vidéotron");
                choix = Console.ReadLine();
            }
            return choix;
        }
        
        public void menupaiement(Utilisateur user)
        {
            Console.WriteLine("Veuillez choisir le compte pour le paiement:");
            Console.WriteLine("1- Compte épargne");
            Console.WriteLine("2- Compte chèque");
            Console.WriteLine("Saisir 1 pour le compte chèque et 2 pour le compte épargne.");
            choixpaiement(user);
        }
        
        public CompteClient choixpaiement(Utilisateur user)
        {
            string choice = Console.ReadLine();
            bool erreur = false;
            CompteClient compte= null;
            switch (choice)
            {
                case "1":
                    chequeFacture(user);
                    compte = user.Chequeactuel;
                    break;
                case "2":
                    epargneFacture(user);
                    compte = user.Epargneactuel;
                    break;
                default:
                    erreur = true;
                    erreurChoixpaiement(choice, user, erreur);
                    break;
            }
            return compte;
        }
        public void erreurChoixpaiement(string choice, Utilisateur user, bool erreur)
        {
            while (erreur == true)
            {
                erreur = false;
                Console.WriteLine("Veuillez choisir parmi les numéros ci-dessus.");
            }
            choixpaiement(user);
        }
        public double choixmontantFacture(Utilisateur user)
        {
            Console.WriteLine("Veuillez saisir le montant à payer pour la facture: \n");
            string montant = Console.ReadLine();
            while (!montant.All(char.IsDigit) || Convert.ToDouble(montant) < 0)
            {
                Console.WriteLine("Il y a erreur dans le montant entré. Veuillez saisir votre montant.");
                montant = Console.ReadLine();
            }
            double paiement = Convert.ToDouble(montant);
            double frais = 2d;
            double debit = paiement + frais;
            return debit;
        }
        public double fondinsuffisantfact(Utilisateur user)
        {
            Console.WriteLine("Vos fonds sont insuffisants. Voulez-vous retourner au menu ou changer le montant de la transaction?");
            Console.WriteLine("1- Retourner au menu utilisateur");
            Console.WriteLine("2- Changer le montant de la facture");
            string choice = Console.ReadLine();
            bool erreur = false;
            double montant = 0d ;
            switch (choice)
            {
                case "1":
                    MenuUsager(user);
                    break;
                case "2":
                    montant=choixmontantFacture(user);
                    break;
                default:
                    erreur = true;
                    erreurfondinsuffisant(choice, user, erreur);
                    break;
            }
            return montant;
        }
        public void erreurfondinsuffisant(string choice, Utilisateur user, bool erreur)
        {
            while (erreur == true)
            {
                erreur = false;
                Console.WriteLine("Veuillez choisir parmi les numéros ci-dessus.");
            }
            fondinsuffisantfact(user);
        }
        public void chequeFacture(Utilisateur user)
        {
            string fournisseur = choixFournisseur(user);
            double debit = choixmontantFacture(user);
            while (user.Chequeactuel.Solde < debit)
            {
               debit= fondinsuffisantfact(user);
            }
            user.Chequeactuel.Solde=user.Chequeactuel.Solde-debit;
            double cheque =user.Chequeactuel.Solde;
            string chequeString = cheque.ToString("C", CultureInfo.CurrentCulture);
            string debitStr= debit.ToString("C", CultureInfo.CurrentCulture);
            Console.WriteLine("Votre compte chèque a été débité de: {0} $ pour le paiement de"+ fournisseur +".", debit);
            Console.WriteLine("Le solde de votre compte chèque est de: {0}", chequeString, "$.");
            Console.ReadKey();
            MenuUsager(user);
        }
        public void epargneFacture(Utilisateur user)
        {
            string fournisseur = choixFournisseur(user);
            double debit = choixmontantFacture(user);
            while (user.Chequeactuel.Solde < debit)
            {
                debit=fondinsuffisantfact(user);
            }
            user.Epargneactuel.Solde -= debit;
            double epargne = user.Epargneactuel.Solde;
            string epargneString = epargne.ToString("C", CultureInfo.CurrentCulture);
            string debitStr = debit.ToString("C", CultureInfo.CurrentCulture);
            Console.WriteLine("Votre compte chèque a été débité de: {0} $ pour le paiement de { 1}.", debitStr, fournisseur);
            Console.WriteLine("Le solde de votre compte épargne est de: {0}", epargneString, "$.");
            Console.ReadKey();
            MenuUsager(user);
        }
        public void fermerSession()
        {
            menuprincipale();
        }

        //SECTION ADMINISTRATEUR

        public void menuAdmin()
        {
            Console.WriteLine("******************************************************************************************************");
            Console.WriteLine("1- Remettre le guichet en fonction");
            Console.WriteLine("2- Déposer de l'argent dans le guichet");
            Console.WriteLine("3- Voir le solde du guichet");
            Console.WriteLine("4- Voir la liste des comptes");
            Console.WriteLine("5- Retourner au menu principal");
            Console.WriteLine("******************************************************************************************************");
            Console.WriteLine("Veuillez choisir votre action parmi les numéros ci-dessus:\n");
            choiceAdmin();
        }
        public void retourmenuadmin()
        {
            while (useradmin == true)
            {
                menuAdmin();
            }
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
                        break;
                    case "3":
                        soldeGuichet();
                        break;
                    case "4":
                        voirlistCompte();
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
        public double depotGuichet()
        {
            double depot;
            Console.WriteLine("Veuillez saisir le montant à déposer: \n");
            depot = Convert.ToDouble(Console.ReadLine());
            if (depot >= 10000d)
            {
                Console.WriteLine("Le montant maximum de dépot est de 10 000,00$.\nVeuillez saisir un nouveau montant à déposer.\n");
                depot = Convert.ToDouble(Console.ReadLine());
            }
            soldeguichet += depot;
            soldeGuichet();
            return (depot);
        }
        public void soldeGuichet()
        {
            string soldeG = soldeguichet.ToString("C", CultureInfo.CurrentCulture);
            Console.WriteLine("Le solde du guichet est de: {0}", soldeG);
        }

        public void remiseFonction()
        {
            Console.WriteLine("Désirez-vous remettre le système en fonction? Saisir O pour 'oui' et N pour 'Non'\n");
            string response = Console.ReadLine();

            if (response.Equals("O"))
            {
                retourMenuppl();
            }
            if (response.Equals("N"))
            {
                modepanne(panne);
            }
            while (!response.Equals("O") && !response.Equals("N"))
            {
                Console.WriteLine("Réponse érronée. Veuillez saisir O pour 'oui' et N pour 'Non'\n");
                response = Console.ReadLine();
            }
        }
        public void accesComptAdmin()
        {
            Console.WriteLine("******************************************************************************************************");
            Console.WriteLine("Bienvenue sur le compte Administrateur");
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
                    modepanne(panne);
                    break;
                }
                if (namead.Equals("admin") && nipad.Equals("123456"))
                {
                    menuAdmin();
                }

                if (!namead.Equals("admin") || !nipad.Equals("123456"))
                {
                    Console.WriteLine("La combinaison 'Nom d'utilisateur et NIP' n'est pas reconnue.\nVeuillez saisir votre nom d'utilisateur et nip.");
                    Console.WriteLine("Nom d'utilisateur:\n");
                    namead = Console.ReadLine();
                    Console.WriteLine("Mot de passe:\n");
                    nipad = Console.ReadLine();
                }
                i = i + 1;
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
                        modepanne(panne);
                        Console.WriteLine("Veuillez contacter un admnistrateur pour remettre en fonction le guichet.");
                        menuAdmin();
                        break;
                }
            }
        }
        public void voirlistCompte()
        {
            Console.WriteLine("Utilisateur" + "\t" + "NIP" + "\t" + "Chèque" + "\t" + "Solde" + "\t" + "\t" + "Epargne" + "\t" + "Solde" + "\t" + "\t" +  "Activation");

            foreach (Utilisateur user in listCompte)
            {
                Console.WriteLine(user.Nom + "\t" + user.Nip + "\t" + user.Chequeactuel.Numerocompte + "\t" + user.Chequeactuel.Solde + "\t" + "\t" + user.Epargneactuel.Numerocompte + "\t" + user.Epargneactuel.Solde + "\t" + "\t" + user.Activation);
            }
        }
        public void retourMenuppl()
        {
            useradmin = false;
            menuprincipale();
        }


    }
}
