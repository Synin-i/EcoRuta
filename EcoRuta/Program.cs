using System;
using System.Linq;
using System.IO;
using EcoRutaData;
using EcoRutaConsoleUI;
using System.Text;
using EcoRutaCore;

namespace EcoRutaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Configuracion.AsegurarDirectorios();
            MenuConsola showmenu = new MenuConsola();
            showmenu.MostrarMenu();
        }

    }

}
