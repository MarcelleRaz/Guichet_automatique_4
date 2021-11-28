using System;
using System.Collections.Generic;
using System.Text;

namespace Guichet
{
    abstract class CompteClient
    {
        private string nom;
        private string nip;
        private bool blocked = false;//Pour verouiller le compte du client
        protected string numerocompte;

        public string Nom { get => nom; set => nom = value; }
        public string Nip { get => nip; set => nip = value; }
        public bool Blocked { get => blocked; set => blocked = value; }

        public CompteClient()
        {
           
        }
    }
}
