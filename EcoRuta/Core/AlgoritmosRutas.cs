using EcoRutaData;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace EcoRutaCore
{
    public class AlgoritmosRutas
    {
        int[,] matrizCobertura = new int[5, 7];
        public int[,] MatrizZonasporDias()
        {
            
            string[] zonas = { "Sopocachi", "San Pedro", "Miraflores", "San Jorge", "Obrajes" };
            string[] dias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };

            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== REGISTRO DE COBERTURA ===\n");

                Console.Write($"{"",-15}");
                for (int j = 0; j < dias.Length; j++)
                {
                    Console.Write($"{dias[j],-12}");
                }
                Console.WriteLine();

                for (int i = 0; i < matrizCobertura.GetLength(0); i++)
                {
                    Console.Write($"{i + 1}. {zonas[i],-12}");

                    for (int j = 0; j < matrizCobertura.GetLength(1); j++)
                    {
                        if (matrizCobertura[i, j] == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"{matrizCobertura[i, j],-12}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write($"{matrizCobertura[i, j],-12}");
                        }
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("\n¿Desea registrar una cobertura?");
                Console.WriteLine("1. Sí, actualizar registro");
                Console.WriteLine("0. No, volver al menú principal");

                int opcion;
                Validadores.Leer("Seleccione una opción: ", out opcion);

                if (opcion == 0)
                {
                    continuar = false;
                }
                else if (opcion == 1)
                {
                    int idZona, idDia;

                    Console.WriteLine("\n--- Ingrese los datos (Use los números correspondientes) ---");

                    Validadores.Leer("Número de Zona (1-5): ", out idZona);
                    int indiceZona = idZona - 1;

                    Console.WriteLine("\nDías: 1.Lun | 2.Mar | 3.Mié | 4.Jue | 5.Vie | 6.Sáb | 7.Dom");
                    Validadores.Leer("Número de Día (1-7): ", out idDia);
                    int indiceDia = idDia - 1;

                    if (indiceZona >= 0 && indiceZona < 5 && indiceDia >= 0 && indiceDia < 7)
                    {
                        matrizCobertura[indiceZona, indiceDia] = 1;
                        Console.WriteLine("\n¡Cobertura registrada con éxito! Presione una tecla...");
                    }
                    else
                    {
                        Console.WriteLine("\nError: Zona o Día fuera de rango. Presione una tecla...");
                    }
                    Console.ReadKey();
                }
            }

            return matrizCobertura;
        }
        public int[,] ObtenerMatriz()
        {
            return matrizCobertura;
        }

    }

}

