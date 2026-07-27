using EcoRutaCore;
using EcoRutaData;
using EcoRutaModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using static EcoRutaData.LeerPuntosdeRecoleccion;

namespace EcoRutaConsoleUI
{
    public class MenuConsola
    {
        public void MostrarMenu()
        {
            bool flag = true;
            LeerPuntosdeRecoleccion cargarpuntos = new LeerPuntosdeRecoleccion();
            AlgoritmosRutas gestorCobertura = new AlgoritmosRutas(); 

            while (flag)
            {

                Console.Clear();

              
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n  ==================================================");
                Console.WriteLine("             🍃 ECO RUTA - MENÚ PRINCIPAL 🍃         ");
                Console.WriteLine("  ==================================================\n");
                Console.ResetColor();

              
                Console.WriteLine("    [ 1 ] Mostrar Puntos de Recolección");
                Console.WriteLine("    [ 2 ] Agregar Punto de Recolección");
                Console.WriteLine("    [ 3 ] Crear una Ruta");
                Console.WriteLine("    [ 4 ] Mostrar Rutas");
                Console.WriteLine("    [ 5 ] Mostrar Registro de Cobertura");
                Console.WriteLine("    [ 6 ] Generar Reporte");
                Console.WriteLine("    [ 7 ] Búsqueda y Filtros Especiales\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("    [ 0 ] Salir");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n  ==================================================");
                Console.ResetColor();

               
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine();
                int Opcion;
                Validadores.Leer("    ▶ Seleccione una opción: ", out Opcion);
                Console.ResetColor();

                Console.Clear(); // 

                switch (Opcion)
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n  --- LISTADO DE PUNTOS ---");
                        Console.ResetColor();
                        cargarpuntos.LeerArchivoCsv();

                     
                        Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n  --- NUEVO PUNTO DE RECOLECCIÓN ---");
                        Console.ResetColor();
                        CrearPuntos creando = new CrearPuntos();
                        creando.CreadordePuntos();
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n  --- PUNTOS DISPONIBLES ---");
                        Console.ResetColor();

                    
                        cargarpuntos.LeerArchivoCsv(true);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n  --- CREACIÓN DE RUTA ---");
                        Console.ResetColor();
                        CreacionRuta crearruta = new CreacionRuta();
                        crearruta.CrearRuta();
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n  --- LISTADO DE RUTAS CREADAS ---");
                        Console.ResetColor();

                        LecturaRutas lectorRutas = new LecturaRutas();
                        lectorRutas.LeerArchivoRutas();

                        Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
                        Console.ReadKey();
                        break;
                    case 5:
                        gestorCobertura.MatrizZonasporDias();
                        break;
                    case 6:
                        Reportes generador = new Reportes();
                        generador.GenerarReporteGeneral(gestorCobertura.ObtenerMatriz());   
                        Console.ReadKey();
                        break;
                    case 7:
                        BuscadorFiltros buscador = new BuscadorFiltros();
                        buscador.MostrarMenuFiltros(gestorCobertura.ObtenerMatriz());
                        break;
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n  ¡Gracias por usar Eco Ruta! Cerrando sistema...\n");
                        Console.ResetColor();
                        flag = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n  Error: Opción no válida. Intente nuevamente.");
                        Console.ResetColor();
                        Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}