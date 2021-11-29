using System;

namespace Guichet
{
    class Controller
    {
        static void Main(string[] args)
        {
            Guichet guichet = new Guichet();
            
            Controller controller = new Controller();
            controller.Menucopmte1();
            controller.changerleMotdePass();
            controller.déposerMontent();

        }
            public void Menucopmte1()
            {

            CompteCheque cheque = new CompteCheque("23334", 55555);
            CompteEpargne epargne = new CompteEpargne("11111", 55555);
            Console.WriteLine("1-changer le mot de pass");
            Console.WriteLine("2-déposer un montent dans un compte");
            Console.WriteLine("3-retirer un montent");
            Console.WriteLine("4-afficehr le solde compte du chéque ou épargen");
            Console.WriteLine("5-effectuer un virement entre le compte");
            Console.WriteLine("6-payer une facture");
            Console.WriteLine("7-payer une facture");
                int choix = Convert.ToInt32(Console.ReadLine());
                switch (choix)
                {

                    case 1:
                        { changerleMotdePass(); }
                        break;

                    case 2:
                        { déposerMontent(); }
                        break;
                    case 3:
                        { retirerMontent(); }
                        break;
                    case 4:
                        {
                            cheque.afficherleSold();
                            epargne.afficherleSold();
                        }
                        break;
                    case 5:
                        {
                        viremententrelecompte();
                        }
                        break;
                    case 6:
                        { payerunafacture(); }
                        break;
                    case 7:
                        { fermersession(); }
                        break;
                    default:
                        { Console.ReadKey(); }
                        break;
                }
            }
            public string changerleMotdePass()
            {
                string motpass = "jjjj";               
                string newmotpass;
                do
                {
                    Console.WriteLine("entrez le mot de pass avec 4 caractére, différent mote du pass actuel");
                    newmotpass = Console.ReadLine();
                } while (newmotpass.Length != 4 || motpass.Equals(newmotpass));

                Console.WriteLine("nouvelle mot de passe est " + newmotpass);
                Menucopmte1();             
                return newmotpass;
            }

            public void déposerMontent()
            {
            CompteCheque cheque = new CompteCheque("23334", 550);
            CompteEpargne cheque1 = new CompteEpargne("11111",88);
                Console.WriteLine("Dans quel compte vous voudrez déposer un montent?");
                string sortcompte = Console.ReadLine();
            if (sortcompte == "comptecheque") 
                        {
                            Console.WriteLine("enrez votre montent pour déposer");
                            cheque.depot();
                        }
                        
            else if (sortcompte == "CompteEpargne")
                        {
                            Console.WriteLine("enrez votre montent pour déposer");
                cheque1.depot1();
                        }    
                    else
                        {
                            Menucopmte1();
                        }
            Console.WriteLine("*******************************************");
            Menucopmte1(); 
            }
            /* retirer un montent dans un compte */
            public void retirerMontent()
            {
                CompteCheque cheque = new CompteCheque("23334", 11111);
                CompteEpargne Epargne = new CompteEpargne("11111", 5555);
                Console.WriteLine("Dans quel compte vous voudrez déposer un montent?");
                string typedecompte = Console.ReadLine();
            if (typedecompte == "comptecheque")
            {
                Console.WriteLine("enrez votre montent pour retirer");
                cheque.retirer();
            }
            else if (typedecompte == "CompteEpargne")
            {
                Console.WriteLine("enrez votre montent pour retirer");
                Epargne.retirer();
            }
            else
            {
                Menucopmte1();
            }
            Console.WriteLine("*******************************************");
            Menucopmte1();
            }
            public void viremententrelecompte()
            {
                double sold = 100.000;
                CompteCheque cheque = new CompteCheque("23334", 11111);
                CompteEpargne epargne = new CompteEpargne("11111", 5555);
                Console.WriteLine("choisissez votre compte pour virer de l'argent");
                string typedecompte = Console.ReadLine();
                     if (typedecompte == "comptecheque") 
                      {
                            Console.WriteLine("entrez votre montent pour virer");
                            double montent = Convert.ToDouble(Console.ReadLine());
                            if (montent > sold)
                            {
                                Console.WriteLine("n'est pas assez montent dans la compte");
                                Menucopmte1();
                            }
                            else if (montent == 1000)
                            {
                                Console.WriteLine(" donnez votre mot de pass");
                                string motdepass = Console.ReadLine();
                                if (motdepass != "1234" || motdepass != "1234" || motdepass != "1998" || motdepass != "9874" || motdepass != "6541" || motdepass != "9856")
                                { 
                                       for (int i = 0; i < 3; i++)
                                  {
                                    Console.WriteLine("entrez votre mot de pass carrect");
                                    motdepass = Console.ReadLine();
                                  }
                                   if (motdepass == "1234" || motdepass== "1234" || motdepass == "1998" || motdepass == "9874" || motdepass == "6541" || motdepass == "9856")
                                   {
                                    cheque.virerdecheque();
                                    epargne.recudecheque();
                                   }
                                 }
                              }
                            cheque.virerdecheque();
                            epargne.recudecheque();
                            }
                       else  if (typedecompte == "comptecheque")
                       {
                            Console.WriteLine("enrez votre montent pour retirer");
                            double montent = Convert.ToDouble(Console.ReadLine());
                            epargne.virerdeepargne();
                            cheque.recudeepargne();
                        }
            Menucopmte1();   
            }

            /* Affiche un solde*/

            /*payer un facture*/
            public void payerunafacture()
            {
                double sold = 10000;
                CompteCheque cheque = new CompteCheque("23334", 111111);
                CompteEpargne Epargne = new CompteEpargne("11111", 555555);
                Console.WriteLine("entrez votre nome ");
                string propriétaire = Console.ReadLine();
                Console.WriteLine("choisir les trois fournisseur SVP");
                Console.WriteLine(" 1-Amazon");
                Console.WriteLine(" 2-Bell");
                Console.WriteLine(" 3-Vidéotron");
                string fournisseur = Console.ReadLine();
                if (fournisseur != "Amazon " || fournisseur != "Bell" || fournisseur != "Vidéotron")
                {
                    Console.WriteLine("choisissez bon fournisseur SVP");
                }

                Console.WriteLine("entrez votre type de copmte ");
                string choisircompte = Console.ReadLine();
                switch (choisircompte)
                {

                    case "comptecheque":
                        {
                            Console.WriteLine("entrez votre montent pour payer");
                            double montent = Convert.ToDouble(Console.ReadLine());
                            cheque.payerfacture();
                            if (montent > sold)
                            {
                                Console.WriteLine("choisissez entre les deux:1-désirez retourner au menu .");
                                Console.WriteLine("2-changez le montant de la transaction");
                                string chois = Console.ReadLine();
                                switch (chois)
                                {
                                    case "1":
                                        {
                                            Menucopmte1();
                                        }
                                        break;
                                    case "2":
                                        {

                                        }
                                        break;
                                    default:
                                        Console.ReadKey();
                                        break;
                                }

                            }
                        }
                        break;

                    case "copmteepargne":
                        {
                            Console.WriteLine("enrez votre montent pour déposer");
                            double montent = Convert.ToDouble(Console.ReadLine());
                            if (montent > sold)
                            {
                                Console.WriteLine("désirez retourner au menu ou changez le montant de la transaction.");
                            }
                            Epargne.payerfacture();
                        }
                        break;
                    default:
                        {
                            Menucopmte1();
                        }
                        break;
                }
            }

            public void fermersession()
            {
                Guichet guichet = new Guichet();
                guichet.menuprincipale();
            }

        }
    }
