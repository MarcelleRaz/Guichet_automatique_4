using System;
using System.Collections.Generic;
using System.Text;

namespace Guichet
{
    public class Utilisateur
    {
        private string nom;
        private string nip;
        private bool activation;
        private CompteCheque chequeactuel;
        private CompteEpargne epargneactuel;

        internal string Nom { get => nom; set => nom = value; }
        internal string Nip { get => nip; set => nip = value; }
        internal bool Activation { get => activation; set => activation = value; }
        internal CompteCheque Chequeactuel { get => chequeactuel; set => chequeactuel = value; }
        internal CompteEpargne Epargneactuel { get => epargneactuel; set => epargneactuel = value; }

        internal Utilisateur(string nom, string nip, CompteCheque cheque, CompteEpargne epargne, bool activate)
        {
            this.Nom = nom;
            this.Nip = nip;
            this.Chequeactuel = cheque;
            this.Epargneactuel = epargne;
            this.Activation = activate;
        }
    }
}
