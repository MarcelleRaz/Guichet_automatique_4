using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Guichet
{
    class Controller
    {
        static void Main(string[] args)
        {
            CompteCheque ch1 = new CompteCheque("ch0001");
            CompteCheque ch2 = new CompteCheque("ch0002");
            CompteCheque ch3 = new CompteCheque("ch0003");
            CompteCheque ch4 = new CompteCheque("ch0004");
            CompteCheque ch5 = new CompteCheque("ch0005");

            CompteEpargne ep1 = new CompteEpargne("ep0001");
            CompteEpargne ep2 = new CompteEpargne("ep0002");
            CompteEpargne ep3 = new CompteEpargne("ep0003");
            CompteEpargne ep4 = new CompteEpargne("ep0004");
            CompteEpargne ep5 = new CompteEpargne("ep0005");

            CompteAdmin compteAd = new CompteAdmin();

            List<CompteClient> Listcompte = new List<CompteClient>();

            ch1.Nom = ep1.Nom = "Fatemeh1";
            ch2.Nom = ep2.Nom = "Xin_Wang";
            ch3.Nom = ep3.Nom = "Marcelle";
            ch4.Nom = ep4.Nom = "PierreLi";
            ch5.Nom = ep5.Nom = "PatrickR";

            ch1.Nip = ep1.Nip = "1234";
            ch2.Nip = ep2.Nip = "1998";
            ch3.Nip = ep3.Nip = "9874";
            ch4.Nip = ep4.Nip = "6541";
            ch5.Nip = ep5.Nip = "9856";

            ch1.Soldecompte = ep1.Soldecompte = 11500d;
            ch2.Soldecompte = ep2.Soldecompte = 1500d;
            ch3.Soldecompte = ep3.Soldecompte = 1500d;
            ch4.Soldecompte = ep4.Soldecompte = 1500d;
            ch5.Soldecompte = ep5.Soldecompte = 1500d;

            Listcompte.Add(ch1);
            Listcompte.Add(ch2);
            Listcompte.Add(ch3);
            Listcompte.Add(ch4);
            Listcompte.Add(ch5);
            Listcompte.Add(ep1);
            Listcompte.Add(ep2);
            Listcompte.Add(ep3);
            Listcompte.Add(ep4);
            Listcompte.Add(ep5);

            compteAd.Nom = "admin";
            compteAd.Nip = "123456";

            Guichet guichet = new Guichet(Listcompte, compteAd);
            guichet.ouvrirguichet(10000);
            //guichet.menuPrincipal();

        }
    }
}
