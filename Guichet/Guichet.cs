using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Guichet
{
    public class Guichet
    {
        private double soldeguichet;
        private bool panne;
        private ArrayList listCompte;
        //Administrateur admin = new Administrateur();
        CompteCheque ch1 = new CompteCheque("ch0001");
        CompteCheque ch2 = new CompteCheque("ch0002");
        CompteCheque ch3= new CompteCheque("ch0003");
        CompteCheque ch4= new CompteCheque("ch0004");
        CompteCheque ch5= new CompteCheque("ch0005");
        CompteEpargne ep1 = new CompteEpargne("ep0001");
        CompteEpargne ep2 = new CompteEpargne("ep0002");
        CompteEpargne ep3 = new CompteEpargne("ep0003");
        CompteEpargne ep4= new CompteEpargne("ep0004");
        CompteEpargne ep5 = new CompteEpargne("ep0005");
        public Guichet()
        {
            this.soldeguichet = 10000d;
            //this.listCompte = ListCompte;
           listCompte = new ArrayList();
        }

        public double Soldeguichet { get => soldeguichet; set => soldeguichet = value; }
        public bool Panne { get => panne; set => panne = value; }
        //public ArrayList ListCompte { get => listCompte; set => listCompte = value; }

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
                menuprincipale();
            }
        }
        public void modepanne(bool Panne)
        {
            if (Panne == true)
            {
                Console.WriteLine("Le système ne peut pas se connecter à votre compte. Veuillez demander un administrateur.");
            }
        }
        public void menuprincipale()
        {
            Console.WriteLine();
            Console.WriteLine("Veuillez choisir l'une des actions suivantes:");
            Console.WriteLine("1- Se connecter à vore compte");
            Console.WriteLine("2- Se connecter comme adimnistrateur");
            Console.WriteLine("3- Quitter");
            choisirMenuppl();
        }
        public void choisirMenuppl()
        {
            string menuchoice="";
            switch (menuchoice)
            {
                case "1":
                    //accesComptClient();
                    break;

                case "2":
                    //admin.accesComptAdmin();
                    break;

                case "3":
                    System.Environment.Exit(0);
                    break;

                default:
                    menuprincipale();
                    break;
            }
        }
        
        public virtual void verificationAcces()
        {
            
        }

        public virtual void remplirlistCompte()
        {
            
            ch1.Nom = ep1.Nom = "Fatemeh1";
            ch2.Nom = ep2.Nom ="Xin_WAng";
            ch3.Nom = ep3.Nom = "Marcelle";
            ch4.Nom = ep4.Nom = "PierreLi";
            ch5.Nom = ep5.Nom = "PatrickR";
            ch1.Nip = ep1.Nip = "1234";
            ch2.Nip = ep2.Nip = "1998";
            ch3.Nip = ep3.Nip = "9874";
            ch4.Nip = ep4.Nip = "6541";
            ch5.Nip = ep5.Nip = "9856";
            listCompte.Add(ch1);
            listCompte.Add(ch2);
            listCompte.Add(ch3);
            listCompte.Add(ch4);
            listCompte.Add(ch5);
            listCompte.Add(ep1);
            listCompte.Add(ep2);
            listCompte.Add(ep3);
            listCompte.Add(ep4);
            listCompte.Add(ep5);

            Console.WriteLine("Utilisateur"+"\t"+"NIP"+"\t"+"Chèque"+"\t"+"Solde"+"\t"+"Epargne"+"\t"+"Solde"+"\t"+"Etat du compte");
            foreach (CompteClient item in listCompte)
            {
                if (item is CompteCheque)
                {
                    Console.Write(item.Nom + "\t" + item.Nip + "\t" + item.Numerocompte);
                }
                if (item is CompteEpargne)
                {
                    Console.Write(item.Numerocompte);
                }
                Console.WriteLine();
            }
        }
        public string changenip(string nip)
        {
            string newnip = "";

            while (!(newnip.Length == 4))
            {
                Console.WriteLine("Veuillez saisir votre nouveau mot de passe (4 charactor): ");
                newnip = Console.ReadLine();
            }

            while (newnip.Equals(nip))
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
            return newnip;
        }
    }    
}
