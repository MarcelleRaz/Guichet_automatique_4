using System;

namespace Guichet
{
    class Controller
    {
        static void Main(string[] args)
        {
            Guichet guichet = new Guichet();
            Administrateur admin = new Administrateur();
            admin.accesComptAdmin();
        }
        public void accesComptClient()
        {
            Console.WriteLine("******************************************************************************************************");
            Console.WriteLine("Bienvenue sur notre Guichet Automatique");
            Console.WriteLine("Veuillez saisir vos informations:");
            Console.WriteLine("Nom d'utilisateur:\n");
            Console.ReadLine();
            Console.WriteLine("Mot de passe:\n");
            Console.ReadLine();
        }
    }
}
