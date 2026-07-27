using EcoRutaCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRutaModels
{
    public class Reportes
    {
        public void GenerarReporteGeneral(int[,] matrizCobertura)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n  ==================================================");
            Console.WriteLine("             📊 REPORTE ESTADÍSTICO GENERAL 📊       ");
            Console.WriteLine("  ==================================================\n");
            Console.ResetColor();

            int totalPuntos = 0;
            int puntosActivos = 0;
            double capacidadTotal = 0;

            Dictionary<string, double> pesosPuntos = new Dictionary<string, double>();
            string rutaPuntos = Configuracion.RutaPuntos;

            if (File.Exists(rutaPuntos))
            {
                string[] lineas = File.ReadAllLines(rutaPuntos);
                for (int i = 1; i < lineas.Length; i++)
                {
                    string[] datos = lineas[i].Split(',');
                    if (datos.Length >= 5)
                    {
                        totalPuntos++;
                        if (double.TryParse(datos[2], out double cap))
                        {
                            capacidadTotal += cap;
                            pesosPuntos[datos[0]] = cap; 
                        }
                        if (bool.TryParse(datos[4], out bool activo) && activo) puntosActivos++;
                    }
                }
            }

 
            int totalRutas = 0;
            double distanciaTotal = 0;
            double totalKilosRecolectados = 0; 

            string rutaRutas = Configuracion.RutaRutas;

     
            double[] metricasPorRuta = new double[0];
            string[] nombresRutas = new string[0];

            if (File.Exists(rutaRutas))
            {
                string[] lineasRutas = File.ReadAllLines(rutaRutas);
                totalRutas = lineasRutas.Length - 1; 

                if (totalRutas > 0)
                {
                    metricasPorRuta = new double[totalRutas];
                    nombresRutas = new string[totalRutas];

                    for (int i = 1; i < lineasRutas.Length; i++)
                    {
                        string[] datos = lineasRutas[i].Split(',');
                        if (datos.Length >= 4)
                        {
                            nombresRutas[i - 1] = datos[1]; 
                            if (double.TryParse(datos[2], out double dist)) distanciaTotal += dist;

                            string[] puntosIds = datos[3].Split('|');
                            double kilosDeEstaRuta = 0;

                            foreach (string id in puntosIds)
                            {
                                if (pesosPuntos.ContainsKey(id))
                                {
                                    kilosDeEstaRuta += pesosPuntos[id];
                                }
                            }

                            metricasPorRuta[i - 1] = kilosDeEstaRuta;
                            totalKilosRecolectados += kilosDeEstaRuta;
                        }
                    }
                }
            }

            int zonasCubiertas = 0;
            int totalVisitas = 0;

            for (int i = 0; i < matrizCobertura.GetLength(0); i++)
            {
                bool zonaTieneVisita = false;

                for (int j = 0; j < matrizCobertura.GetLength(1); j++)
                {
                    if (matrizCobertura[i, j] == 1)
                    {
                        totalVisitas++;
                        zonaTieneVisita = true;
                    }
                }
                if (zonaTieneVisita) zonasCubiertas++;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  --- INFRAESTRUCTURA DE RECOLECCIÓN ---");
            Console.ResetColor();
            Console.WriteLine($"  ▶ Puntos Registrados       : {totalPuntos}");
            Console.WriteLine($"  ▶ Puntos Activos           : {puntosActivos}");
            Console.WriteLine($"  ▶ Capacidad Max. Ciudad    : {capacidadTotal} Kg\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  --- LOGÍSTICA DE LA SEMANA ---");
            Console.ResetColor();
            Console.WriteLine($"  ▶ Zonas Únicas Cubiertas   : {zonasCubiertas} de 5 zonas posibles");
            Console.WriteLine($"  ▶ Total de Visitas (Días)  : {totalVisitas} visitas realizadas\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  --- ESTADÍSTICAS DE RUTAS (MÉTRICAS) ---");
            Console.ResetColor();
            Console.WriteLine($"  ▶ Rutas Operativas         : {totalRutas}");
            Console.WriteLine($"  ▶ Distancia Total Estimada : {distanciaTotal} Km");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  ▶ Basura Total Recolectada : {totalKilosRecolectados} Kg\n");
            Console.ResetColor();

            if (totalRutas > 0)
            {
                Console.WriteLine("  Desglose de Kilos Recolectados por Ruta:");
                for (int i = 0; i < metricasPorRuta.Length; i++)
                {
                    Console.WriteLine($"    - Ruta '{nombresRutas[i]}' : {metricasPorRuta[i]} Kg");
                }
                Console.WriteLine();
            }


            Console.WriteLine();
            bool exportar;
            Validadores.Leer("  ¿Desea exportar este reporte a un archivo .txt? (S/N): ", out exportar);

            if (exportar)
            {
                string rutaTxt = Path.Combine(Configuracion.CarpetaBase, "Reporte_General.txt");

                using (StreamWriter writer = new StreamWriter(rutaTxt, false))
                {
                    writer.WriteLine("==================================================");
                    writer.WriteLine("             REPORTE ESTADISTICO GENERAL          ");
                    writer.WriteLine("==================================================");
                    writer.WriteLine($"Fecha de Emisión: {DateTime.Now}");
                    writer.WriteLine();
                    writer.WriteLine("--- INFRAESTRUCTURA DE RECOLECCION ---");
                    writer.WriteLine($"Puntos Registrados       : {totalPuntos}");
                    writer.WriteLine($"Puntos Activos           : {puntosActivos}");
                    writer.WriteLine($"Capacidad Max. Ciudad    : {capacidadTotal} Kg");
                    writer.WriteLine();
                    writer.WriteLine("--- LOGISTICA DE LA SEMANA ---");
                    writer.WriteLine($"Zonas Unicas Cubiertas   : {zonasCubiertas} de 5 zonas posibles");
                    writer.WriteLine($"Total de Visitas (Dias)  : {totalVisitas} visitas realizadas");
                    writer.WriteLine();
                    writer.WriteLine("--- ESTADISTICAS DE RUTAS ---");
                    writer.WriteLine($"Rutas Operativas         : {totalRutas}");
                    writer.WriteLine($"Distancia Total Estimada : {distanciaTotal} Km");
                    writer.WriteLine($"Basura Total Recolectada : {totalKilosRecolectados} Kg");
                    writer.WriteLine();

                    if (totalRutas > 0)
                    {
                        writer.WriteLine("Desglose de Kilos por Ruta:");
                        for (int i = 0; i < metricasPorRuta.Length; i++)
                        {
                            writer.WriteLine($"- Ruta '{nombresRutas[i]}' : {metricasPorRuta[i]} Kg");
                        }
                    }
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  [+] Reporte exportado exitosamente en: {rutaTxt}");
                Console.ResetColor();
            }

            Console.WriteLine("\n  Presione cualquier tecla para volver al menú...");
            Console.ReadKey();
        }
    }
}
