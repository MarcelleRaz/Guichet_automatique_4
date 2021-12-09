using System;
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
        Administrateur admin = new Administrateur();
        private Utilisateur user;
        
        public Guichet()
        {
            Utilisateur user1 = new Utilisateur("Xin_Wang", "1234", new CompteCheque("ch0001", 20000.00), new CompteEpargne("ep0001", 2500.36), true);
            Utilisateur user2 = new Utilisateur("Fatemeh1", "1998", new CompteCheque("ch0002", 589.12), new CompteEpargne("ep0002", 68452.23), true);
            Utilisateur user3 = new Utilisateur("Marcelle", "9874", new CompteCheque("ch0003", 7896.10), new CompteEpargne("ep0003", 745.23), true);
            Utilisateur user4 = new Utilisateur("PierreLi", "6541", new CompteCheque("ch0004", 1400.25), new CompteEpargne("ep0004", 10000.20), true);
            Utilisateur user5 = new Utilisateur("PatrickR", "9856", new CompteCheque("ch0005", 7500.54), new CompteEpargne("ep0005", 20000.65), true);
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
        }
        public double Soldeguichet { get => soldeguichet; set => soldeguichet = value; }
        public bool Panne { get => panne; set => panne = value; }
        public static ArrayList ListCompte { get => listCompte; set => listCompte = value; }
        public Utilisateur User { get => user; set => user = value; }

        public void startMachine()
        {
            bool start = true;
            while(start == true)
            {
                menuprincipale();
            }
        }    
        //SECTION GUICHET
        public bool ouvrirguichet()
        {
            bool Panne=false;
            if (soldeguichet <= 0)
            {
                panne = true;
                modepanne(Panne);
            }
            return panne;
        }
        public void modepanne(bool panneguichet)
        {
            panneguichet = panne;
            if (panne == true)
            {
                Console.WriteLine("Le système est en panne. Veuillez demander un administrateur.");
            }
        }
        public void menuprincipale()
        {
            Console.WriteLine();
            Console.WriteLine("Veuillez choisir l'une des actions suivantes:");
            Console.WriteLine("1- Se connecter à votre compte");
            Console.WriteLine("2- Se connecter comme adimnistrateur");
            Console.WriteLine("3- Quitter");
            choisirMenuppl();
        }
        public string choisirMenuppl()
        {
            string menuchoice =Console.ReadLine();
            panne = ouvrirguichet();
            if (panne == true)
            {
                switch (menuchoice)
                {
                    case "1":
                        modepanne(panne);
                        break;

                    case "2":
                        accesComptAdmin();
                        break;

                    case "3":
                        System.Environment.Exit(0);
                        break;

                    default:
                        menuprincipale();
                        break;
                }
                MenuUsager();
            }
            else
            {

                switch (menuchoice)
                {
                    case "1":
                        accesComptClient();
                        break;

                    case "2":
                        accesComptAdmin();
                        break;

                    case "3":
                        System.Environment.Exit(0);
                        break;

                    default:
                        menuprincipale();
                        break;
                }
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
                foreach (Utilisateur user_ in listCompte)
                {
                    if (tempo == null && user_.Nom.Equals(name) && user_.Nip.Equals(mdp))
                    {
                        tempo = user_;
                        user = tempo;
                    }
                    if ((tempo == null) && (user_.Nom.Equals(name) && !user_.Nip.Equals(mdp)))
                    {
                        tempo = user_;
                        verification = false;
                        x++;
                    }
                }
                while (verification == false && x <= 4)
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
                    user = tempo;
                    tempo.Activation = false;
                    tempo.modeverrouillage(tempo.Activation);
                }
                if (tempo != null && verification == true)
                {
                    user = tempo;
                    MenuUsager();
                }
            }
            return tempo;
        }
        public void gestionverrouillage()
        {
            while (user != null && user.Activation == false)
            {
                Console.WriteLine("Veuillez contacter un admnistrateur pour déverrouiller votre compte.");
                break;
            }
        }
        public void MenuUsager()
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
            choiceUsager();
        }
        public void choiceUsager()
        {
            string choice = Console.ReadLine();
            bool erreur = false;
            if (user != null && user.Activation == false)
            {
                gestionverrouillage();
            }
            else if (user != null && user.Activation == true)
            {
                switch (choice)
                {
                    case "1":
                        changeNip();
                        break;
                    case "2":
                        menudepot();
                        break;
                    case "3":
                        menuretrait();
                        break;
                    case "4":
                        menusolde();
                        break;
                    case "5":
                        menuvirement();
                        break;
                    case "6":
                        menupaiement();
                        break;
                    case "7":
                        fermerSession();
                        break;
                    default:
                        erreur = true;
                        erreurChoixuser(choice, erreur);
                        break;
                }
                MenuUsager();
            }
        }
        public void erreurChoixuser(string choice, bool erreur)
        {
            while (erreur == true)
            {
                erreur = false;
                Console.WriteLine("Veuillez choisir parmi les numéros ci-dessus.");
            }
            choiceUsager();
        }
        public void changeNip()
        {
            Console.WriteLine(user.Nom +" "+ user.Nip);
            Console.WriteLine("\nVeuillez saisir votre mot de passe actuel:");
            string nipclient = Console.ReadLine();
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
        public void menudepot()
        {
            Console.WriteLine("Veuillez choisir dans quel compte vous voulez faire le dépôt:");
            Console.WriteLine("1- Compte chèque");
            Console.WriteLine("2- Compte épargne");
            Console.WriteLine("Saisir 1 pour le compte chèque et 2 pour le compte épargne.");
            choixdepot();
        }
        public void choixdepot()
        {
            string choice = Console.ReadLine();
            bool erreur = false;
            CompteClient compte = null;
            switch (choice)
            {
                case "1":
                    depotcheque();
                    compte = user.Chequeactuel;
                    break;
                case "2":
                    depotepargne();
                    compte = user.Epargneactuel;
                    break;
                default:
                    erreur = true;
                    erreurChoixdepot(choice, erreur);
                    break;
            }
        }
        public void erreurChoixdepot(string choice, bool erreur)
        {
            while (erreur==true)
            {
                erreur = false;
                Console.WriteLine("Veuillez choisir parmi les numéros ci-dessus.");
            }
            choixdepot();
        }
        public double depotcheque() 
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
            Console.WriteLine("Vous avez déposé {0} dans votre compte chèque", depotStr, "$ .");
            Console.WriteLine("Votre solde du compte chèque est de {0}", soldeStr, "$ .");
            Console.WriteLine("******************************************************************************************************");
            return solde;
        }
        public double depotepargne()
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
            return solde;
        }
        public void menuretrait()
        {
            Console.WriteLine("Veuillez choisir dans quel compte vous voulez faire le retrait:");
            Console.WriteLine("1- Compte chèque");
            Console.WriteLine("2- Compte épargne");
            Console.WriteLine("Saisir 1 pour le compte chèque et 2 pour le compte épargne.");
            choixretrait();
        }
        public void choixretrait()
        {
            string choice = Console.ReadLine();
            bool erreur = false;
            switch (choice)
            {
                case "1":
                    retraitcheque();
                    break;
                case "2":
                    retraitepargne();
                    break;
                default:
                    erreur = true;
                    erreurChoixretrait(choice,user, erreur);
                    break;
            }
        }
        public void erreurChoixretrait(string choice, Utilisateur user_, bool erreur)
        {
            user = user_;
            while (erreur == true)
            {
                erreur = false;
                Console.WriteLine("Veuillez choisir parmi les numéros ci-dessus.");
            }
            choixretrait();
        }
       
        public double montantretrait()
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
        public double fondinsuffisantretrait()
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
                    MenuUsager();
                    break;
                case "2":
                    retrait=montantretrait();
                    break;
                default:
                    erreur = true;
                    erreurfondretrait(choice,erreur);
                    break;
            }
            return retrait;
        }
        public double fondinsuffisantGuichet()
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
                    MenuUsager();
                    break;
                case "2":
                    retrait = montantretrait();
                    break;
                default:
                    erreur = true;
                    erreurfondretrait(choice, erreur);
                    break;
            }
            return retrait;
        }
        public void erreurfondguichet(string choice, bool erreur)
        {
            while (erreur == true)
            {
                erreur = false;
                Console.WriteLine("Veuillez choisir parmi les numéros ci-dessus.");
            }
            fondinsuffisantGuichet();
        }
        public void erreurfondretrait(string choice, bool erreur)
        {
            while(erreur == true)
            {
                erreur = false;
                Console.WriteLine("Veuillez choisir parmi les numéros ci-dessus.");
            }
            fondinsuffisantretrait();
        }
        public double retraitcheque()
        {
            double retrait = montantretrait();
            while (user.Chequeactuel.Solde < retrait)
            {
                retrait = fondinsuffisantretrait();
            }
            while (retrait > soldeguichet)
            {
                retrait = fondinsuffisantGuichet();
            }
            user.Chequeactuel.Solde -= retrait;
            soldeguichet -= retrait;
            double solde = user.Chequeactuel.Solde;
            string soldeStr = solde.ToString("C", CultureInfo.CurrentCulture);
            string retraitStr = retrait.ToString("C", CultureInfo.CurrentCulture);
            Console.WriteLine("Vous avez retiré {0} dans votre compte épargne", retraitStr, "$ .");
            Console.WriteLine("Votre solde du compte épargne est de {0}", soldeStr, "$ .");
            Console.WriteLine("******************************************************************************************************");
            return retrait;
        }
        public double retraitepargne()
        {
            double retrait = montantretrait();
            while (user.Epargneactuel.Solde < retrait)
            {
                retrait=fondinsuffisantretrait();
            }
            user.Epargneactuel.Solde -= retrait;
            soldeguichet -= retrait;
            double solde = user.Epargneactuel.Solde;
            string soldeStr = solde.ToString("C", CultureInfo.CurrentCulture);
            string retraitStr = retrait.ToString("C", CultureInfo.CurrentCulture);
            Console.WriteLine("Vous avez retiré {0} dans votre compte épargne", retraitStr, "$ .");
            Console.WriteLine("Votre solde du compte épargne est de {0}", soldeStr, "$ .");
            Console.WriteLine("******************************************************************************************************");
            return retrait;
        }
        public void menusolde()
        {
            Console.WriteLine("Veuillez choisir dans quel compte vous voulez afficher le solde:");
            Console.WriteLine("1- Compte chèque");
            Console.WriteLine("2- Compte épargne");
            Console.WriteLine("Saisir 1 pour le compte chèque et 2 pour le compte épargne.");
            choixsolde();
        }
        public void choixsolde()
        {
            string choice = Console.ReadLine();
            bool erreur = false;
            CompteClient compte = null;
            switch (choice)
            {
                case "1":
                    soldeCheque();
                    compte = user.Chequeactuel;
                    break;
                case "2":
                    soldeEpargne();
                    compte = user.Epargneactuel;
                    break;
                default:
                    erreur = true;
                    erreurChoixsolde(choice, erreur);
                    break;
            }
        }
        public void erreurChoixsolde(string choice, bool erreur)
        {
            while (erreur == true)
            {
                erreur = false;
                Console.WriteLine("Veuillez choisir parmi les numéros ci-dessus.");
            }
            choixsolde();
        }
        public void soldeCheque()
        {
            string t1 = user.Chequeactuel.Solde.ToString("C", CultureInfo.CurrentCulture);
            Console.WriteLine($"Le montant du compte chèque est: {t1}");
            Console.ReadKey();
        }
        public void soldeEpargne()
        {
            string t2 = user.Epargneactuel.Solde.ToString("C", CultureInfo.CurrentCulture);
            Console.WriteLine($"Le montant de votre compte épargne est de: {t2}");
            Console.ReadKey();
        }
        public void menuvirement()
        {
            Console.WriteLine("Veuillez choisir votre transfert:");
            Console.WriteLine("1- Du compte chèque à compte épargne");
            Console.WriteLine("2- Du compte épargne à compte chèque");
            Console.WriteLine("Saisir 1 pour le compte chèque et 2 pour le compte épargne.");
            choixvirement();
        }
        public void choixvirement()
        {
            string choice = Console.ReadLine();
            bool erreur = false;
            switch (choice)
            {
                case "1":
                    chequeversEpargne();
                    break;
                case "2":
                    epargneversCheque();
                    break;
                default:
                    erreur = true;
                    erreurChoixvirement(choice, erreur);
                    break;
            }
        }
        public void erreurChoixvirement(string choice, bool erreur)
        {
            while (erreur == true)
            {
                erreur = false;
                Console.WriteLine("Veuillez choisir parmi les numéros ci-dessus.");
            }
            choixvirement();
        }
        public string fondinsuffisantvirement()
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
                    MenuUsager();
                    break;
                case "2":
                    montant=montantvirement();
                    break;
                default:
                    erreur = true;
                    erreurfondvirement(choice, erreur);
                    break;
            }
           return montant;
        }
        public void erreurfondvirement(string choice, bool erreur)
        {
            while (erreur == true)
            {
                erreur = false;
                Console.WriteLine("Veuillez choisir parmi les numéros ci-dessus.");
            }
            fondinsuffisantvirement();
        }
        public string montantvirement()
        {
            Console.WriteLine("Veuillez saisir le montant à transférer vers le compte épargne: \n");
            string depot = Console.ReadLine();
            return depot;
        }
        public void chequeversEpargne()
        {
            double depot = validationMontant();
           while (user.Chequeactuel.Solde < depot)
            {
                fondinsuffisantvirement();
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
        }
        public void epargneversCheque()
        {
            double depot = validationMontant();
            while (user.Epargneactuel.Solde < depot)
            {
                fondinsuffisantvirement();
                depot = Convert.ToDouble(validationMontant());
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
        }
        public double validationMontant()
        {
            string montant = montantvirement();
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
                    if (validerNip() == false)
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
        public bool validerNip()
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
        public string choixFournisseur()
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
        
        public void menupaiement()
        {
            Console.WriteLine("Veuillez choisir le compte pour le paiement:");
            Console.WriteLine("1- Compte épargne");
            Console.WriteLine("2- Compte chèque");
            Console.WriteLine("Saisir 1 pour le compte chèque et 2 pour le compte épargne.");
            choixpaiement();
        }
        
        public CompteClient choixpaiement()
        {
            string choice = Console.ReadLine();
            bool erreur = false;
            CompteClient compte= null;
            switch (choice)
            {
                case "1":
                    chequeFacture();
                    compte = user.Chequeactuel;
                    break;
                case "2":
                    epargneFacture();
                    compte = user.Epargneactuel;
                    break;
                default:
                    erreur = true;
                    erreurChoixpaiement(choice, erreur);
                    break;
            }
            return compte;
        }
        public void erreurChoixpaiement(string choice, bool erreur)
        {
            while (erreur == true)
            {
                erreur = false;
                Console.WriteLine("Veuillez choisir parmi les numéros ci-dessus.");
            }
            choixpaiement();
        }
        public double choixmontantFacture()
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
        public double fondinsuffisantfact()
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
                    MenuUsager();
                    break;
                case "2":
                    montant=choixmontantFacture();
                    break;
                default:
                    erreur = true;
                    erreurfondinsuffisant(choice, erreur);
                    break;
            }
            return montant;
        }
        public void erreurfondinsuffisant(string choice, bool erreur)
        {
            while (erreur == true)
            {
                erreur = false;
                Console.WriteLine("Veuillez choisir parmi les numéros ci-dessus.");
            }
            fondinsuffisantfact();
        }
        public void chequeFacture()
        {
            string fournisseur = choixFournisseur();
            double debit = choixmontantFacture();
            while (user.Chequeactuel.Solde < debit)
            {
               debit= fondinsuffisantfact();
            }
            user.Chequeactuel.Solde=user.Chequeactuel.Solde-debit;
            double cheque =user.Chequeactuel.Solde;
            string chequeString = cheque.ToString("C", CultureInfo.CurrentCulture);
            string debitStr= debit.ToString("C", CultureInfo.CurrentCulture);
            Console.WriteLine("Votre compte chèque a été débité de: {0} $ pour le paiement de"+ fournisseur +".", debit);
            Console.WriteLine("Le solde de votre compte chèque est de: {0}", chequeString, "$.");
            Console.ReadKey();
        }
        public void epargneFacture()
        {
            string fournisseur = choixFournisseur();
            double debit = choixmontantFacture();
            while (user.Chequeactuel.Solde < debit)
            {
                debit=fondinsuffisantfact();
            }
            user.Epargneactuel.Solde -= debit;
            double epargne = user.Epargneactuel.Solde;
            string epargneString = epargne.ToString("C", CultureInfo.CurrentCulture);
            string debitStr = debit.ToString("C", CultureInfo.CurrentCulture);
            Console.WriteLine("Votre compte chèque a été débité de: {0} $ pour le paiement de { 1}.", debitStr, fournisseur);
            Console.WriteLine("Le solde de votre compte épargne est de: {0}", epargneString, "$.");
            Console.ReadKey();
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
        //public void retourmenuadmin()
        //{
        //    while (useradmin == true)
        //    {
        //        menuAdmin();
        //    }
        //}
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
                menuAdmin();
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
                panne = false;
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
            while (i <= 4)
            {
                if (i == 4)
                {
                    panne = true;
                    modepanne(panne);
                }else if
                 (namead.Equals("admin") && nipad.Equals("123456"))
                {
                    menuAdmin();
                    break;
                }
                if (!namead.Equals("admin") || !nipad.Equals("123456"))
                {
                    Console.WriteLine("La combinaison 'Nom d'utilisateur et NIP' n'est pas reconnue.\nVeuillez saisir votre nom d'utilisateur et nip.");
                    Console.WriteLine("Nom d'utilisateur:\n");
                    namead = Console.ReadLine();
                    Console.WriteLine("Mot de passe:\n");
                    nipad = Console.ReadLine();
                }
                i ++;
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
            menuprincipale();
        }


    }
}
