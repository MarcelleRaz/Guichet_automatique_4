using System;
using System.Collections.Generic;
using System.Text;

namespace Guichet
{
    public abstract class CompteClient
    {
        protected string numerocompte;
        public CompteClient()
        {

        }
        public string Numerocompte { get => numerocompte; set => numerocompte = value; }
    }
}
