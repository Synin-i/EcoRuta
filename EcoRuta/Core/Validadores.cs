using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRutaCore
{
     public class Validadores
    {
        public static void Leer(string text, out int x)
        {
            Console.Write(text);
            while(!int.TryParse(Console.ReadLine(),out x))
            {
                Console.WriteLine("Caracter Invalido");
                Console.Write(text);
            }
        }
        public static void Leer(string text, out double x)
        {
            Console.Write(text);
            while (!double.TryParse(Console.ReadLine(), out x))
            {
                Console.WriteLine("Caracter Invalido");
                Console.Write(text);
            }
        }

        public static void Leer(string text, out bool x)
        {
            Console.Write(text);
            while (true)
            {
                string? entrada = Console.ReadLine()?.Trim().ToUpper();
                if (entrada == "S")
                {
                    x = true;
                    return;
                }
                if (entrada == "N")
                {
                    x = false;
                    return;
                }
                Console.Write(text);
            }
        }
        public static void Leer(string text, out string? x)
        {
            Console.Write(text);
            x = Console.ReadLine()??"";
        }
    } 
}
