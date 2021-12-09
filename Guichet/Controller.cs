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
            Guichet guichet = new Guichet();
            guichet.ouvrirguichet(10000);
        }
    }
}
