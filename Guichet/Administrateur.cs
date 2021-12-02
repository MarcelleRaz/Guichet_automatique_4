using System;
using System.Collections.Generic;
using System.Text;

namespace Guichet
{
    public class Administrateur
    {
        private string nom;
        private string nip;
        public Administrateur()
        {

        }
        public string NomAdmin { get => nom; set => nom = value; }
        public string NipAdmin { get => nip; set => nip = value; }

        
    }
}
