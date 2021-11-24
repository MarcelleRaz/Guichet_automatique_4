using System;

namespace Guichet
{
    class Controller
    {
        static void Main(string[] args)
        {
            Guichet guichet = new Guichet();
            Administrateur admin = new Administrateur();
            admin.menuAdmin();
        }
    }
}
