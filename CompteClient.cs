using System;
using System.Collections.Generic;
using System.Text;

namespace Guichet
{
    public abstract class CompteClient
    {
        private string nom;
        private string nip;
        private double soldecompte;
        private bool blocked = false;//Pour verouiller le compte du client
        private string numerocompte;

        public string Nom { get => nom; set => nom = value; }
        public string Nip { get => nip; set => nip = value; }
        public bool Blocked { get => blocked; set => blocked = value; }
        public double Soldecompte { get => soldecompte; set => soldecompte = value; }
        protected string Numerocompte { get => numerocompte; set => numerocompte = value; }

        public CompteClient()
        {
           
        }
    }
}
