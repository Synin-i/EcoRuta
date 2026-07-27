using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace EcoRutaCore
{
    public static class Configuracion
    {
        public const string CarpetaBase = @"C:\Users\limbe\source\repos\Programacion2\EcoRuta\EcoRuta\Data\output";
        public static readonly string RutaPuntos = Path.Combine(CarpetaBase, "PuntosDeRecolección.csv");
        public static readonly string RutaRutas = Path.Combine(CarpetaBase, "Rutas.csv");

        public static void AsegurarDirectorios()
        {
            if (!Directory.Exists(CarpetaBase))
            {
                Directory.CreateDirectory(CarpetaBase);
            }
        }
    }
}
