using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.IO;
using EcoRutaConsoleModels;
using EcoRutaCore;
using EcoRutaData;
using EcoRutaModels;
using System.Numerics;
using System.Linq.Expressions;

namespace EcoRutaData
{

    public class CrearPuntos
    {
        public void CreadordePuntos()
        {
            try
            {
                bool ArchivoNuevo = !File.Exists(Configuracion.RutaPuntos);
                PuntoRecoleccion[] puntos1 = new PuntoRecoleccion[1];
                Console.WriteLine("Creando Un Punto de Recoleccion...");
                for (int i = 0; i < puntos1.Length; i++)
                {
                    int id, frecuencia;
                    string zona;
                    double capacidadkg;
                    bool activo = false;
                    Validadores.Leer("ID: ", out id);
                    Validadores.Leer("Zona: ", out zona);
                    Validadores.Leer("Capacidad(kg): ", out capacidadkg);
                    Validadores.Leer("Frecuencia: ", out frecuencia);
                    Validadores.Leer("¿Punto Activo?(S/N)", out activo);
                    puntos1[i] = new PuntoRecoleccion(id, zona, capacidadkg, frecuencia, activo);
                }

                using (StreamWriter writer = new StreamWriter(Configuracion.RutaPuntos, true))
                {

                    if (ArchivoNuevo)
                    {
                        writer.WriteLine("ID,Zona,Capacidad (KG),Frecuencia,Activo");
                    }
                    for (int i = 0; i < puntos1.Length; i++)
                    {
                        writer.WriteLine($"{puntos1[i].Id},{puntos1[i].Zona},{puntos1[i].CapacidadKg},{puntos1[i].FrecuenciaSemanal},{puntos1[i].Activo}");
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Punto Creado Exitosamente...");
                    Console.ResetColor();

                }

            }
            catch (UnauthorizedAccessException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  [!] Error de Permisos: El sistema denegó el acceso para guardar el archivo.");
                Console.WriteLine("  Ejecute el programa como Administrador o cambie la ruta de la carpeta.");
                Console.ResetColor();
            }
            catch (DirectoryNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  [!] Error: La carpeta destino para guardar el punto no existe.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  [!] Error inesperado al guardar: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("Presione Cualquier Tecla Para Continuar...");
            Console.ReadKey();
        }
    }




    public class LeerPuntosdeRecoleccion
    {
        public void LeerArchivoCsv(bool soloActivos = false)
        {

            if (!File.Exists(Configuracion.RutaPuntos))
            {
                Console.WriteLine("No hay puntos registrados aún.");
                return;
            }

            using (StreamReader Lector = new StreamReader(Configuracion.RutaPuntos))
            {
                Lector.ReadLine();
                string? linea;
                Console.WriteLine($"{"ID",-6} | {"Zona",-15} | {"Capacidad(Kg)",-15} | {"Frecuencia",-12} | {"Estado",-8}");

                while ((linea = Lector.ReadLine()) != null)
                {
                    string[] datos = linea.Split(',');

                    if (datos.Length >= 5)
                    {
                        bool esActivo = datos[4].Trim().ToLower() == "true";
                        bool debeMostrarse;

                        if (!soloActivos)
                        {

                            debeMostrarse = true;
                        }
                        else if (soloActivos && esActivo)
                        {

                            debeMostrarse = true;
                        }
                        else
                        {

                            debeMostrarse = false;
                        }

                        if (debeMostrarse)
                        {
                            string id = $"[{datos[0]}]";
                            string zona = datos[1];
                            string capacidad = $"{datos[2]} Kg";
                            string frecuencia = $"{datos[3]} x/semana";
                            string estado = esActivo ? "Activo" : "Inactivo";

                            Console.WriteLine($"{id,-9}{zona,-18} | {capacidad,-18:F1} |{frecuencia,-15}|{estado,-11}");
                        }
                    }
                }
            }
        }
    }
    public class CreacionRuta
    {
        public void CrearRuta()
        {
            string ruta = @"C:\Users\limbe\source\repos\Programacion2\EcoRuta\EcoRuta\Data\output";


            Dictionary<int, double> puntosDisponibles = new Dictionary<int, double>();

            if (File.Exists(Configuracion.RutaPuntos))
            {
                string[] lineas = File.ReadAllLines(Configuracion.RutaPuntos);
                for (int i = 1; i < lineas.Length; i++)
                {
                    string[] datos = lineas[i].Split(',');
                    if (datos.Length >= 5 && int.TryParse(datos[0], out int id) && double.TryParse(datos[2], out double capacidad))
                    {
                        puntosDisponibles[id] = capacidad;
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: No existe el archivo de puntos. Cree puntos de recolección primero.");
                Console.ResetColor();
                Console.WriteLine("Presione cualquier tecla para volver...");
                Console.ReadKey();
                return;
            }

            HashSet<int> puntosYaUsados = new HashSet<int>();
            if (File.Exists(Configuracion.RutaRutas))
            {
                string[] lineasRutas = File.ReadAllLines(Configuracion.RutaRutas);
                for (int i = 1; i < lineasRutas.Length; i++)
                {
                    string[] datosRutas = lineasRutas[i].Split(',');
                    if (datosRutas.Length >= 4)
                    {

                        string[] idsAsignados = datosRutas[3].Split('|');
                        foreach (string idStr in idsAsignados)
                        {
                            if (int.TryParse(idStr, out int idUsado))
                            {
                                puntosYaUsados.Add(idUsado);
                            }
                        }
                    }
                }
            }
            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }

            Ruta[] rutas = new Ruta[1];
            bool ArchivoNuevo = !File.Exists(Configuracion.RutaRutas);
            Console.WriteLine("Creando Rutas...");
            for (int i = 0; i < rutas.Length; i++)
            {
                int numerodepuntos = 0;
                int id;
                string nombre;


                Console.WriteLine($"\n--- Ruta #{i + 1} ---");
                Validadores.Leer("ID Ruta: ", out id);
                Validadores.Leer("Nombre Ruta: ", out nombre);
                double umbralEcoAmigable = 40.0;
                double distanciaestimada = 0;
                bool distanciaValida = false;

                while (!distanciaValida)
                {
                    Validadores.Leer("Distancia(Km): ", out distanciaestimada);

                    if (distanciaestimada > umbralEcoAmigable)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\n  [!] ERROR: La ruta excede el umbral límite eco-amigable ({umbralEcoAmigable} Km).");
                        Console.WriteLine("  Por favor, reingrese una distancia permitida o contacte con el administrador.");
                        Console.ResetColor();
                    }
                    else
                    {
                        distanciaValida = true;
                    }
                }
                Validadores.Leer("Cantidad de Puntos a agregar a esta ruta: ", out numerodepuntos);

                int[] asignandopuntos = new int[numerodepuntos];
                double capacidadAcumulada = 0;


                for (int j = 0; j < asignandopuntos.Length; j++)
                {
                    bool puntoValido = false;
                    while (!puntoValido)
                    {
                        int iddepunto;
                        Validadores.Leer($"Ingrese ID de Punto {j + 1}: ", out iddepunto);


                        if (!puntosDisponibles.ContainsKey(iddepunto))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("  [!] Error: Ese ID de punto no existe. Intente con otro.");
                            Console.ResetColor();
                        }

                        else if (Array.IndexOf(asignandopuntos, iddepunto) != -1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("  [!] Error: Punto duplicado. Ya se ingresó este punto en esta ruta.");
                            Console.ResetColor();
                        }

                        else if (puntosYaUsados.Contains(iddepunto))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("  [!] Error: Este punto ya pertenece a otra ruta creada anteriormente.");
                            Console.ResetColor();
                        }
                        else
                        {
                            capacidadAcumulada += puntosDisponibles[iddepunto];

                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"  [+] Punto agregado correctamente. Capacidad total de la ruta: {capacidadAcumulada} Kg");
                            Console.ResetColor();

                            asignandopuntos[j] = iddepunto;
                            puntoValido = true;
                        }
                    }
                }

                rutas[i] = new Ruta(id, nombre, asignandopuntos, distanciaestimada);
            }
            using (StreamWriter writer = new StreamWriter(Configuracion.RutaRutas, true))
            {
                if (ArchivoNuevo)
                {
                    writer.WriteLine("Id,Nombre,DistanciaKm,PuntosIDs");
                }
                for (int i = 0; i < rutas.Length; i++)
                {
                    string puntosUnidos = string.Join("|", rutas[i].PuntosAsignados);
                    writer.WriteLine($"{rutas[i].Id},{rutas[i].Nombre},{rutas[i].DistanciaEstimadaKm},{puntosUnidos},");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ruta Creada Exitosamente...");
                Console.ResetColor();
            }
            Console.WriteLine("Presione Cualquier Tecla Para Continuar...");
            Console.ReadKey();

        }

    }

    public class LecturaRutas
    {
        public void LeerArchivoRutas()
        {

            try
            {
                if (!File.Exists(Configuracion.RutaRutas))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  [!] No hay rutas registradas aún. Cree una ruta primero.");
                    Console.ResetColor();
                    return;
                }

                using (StreamReader Lector = new StreamReader(Configuracion.RutaRutas))
                {
                    Lector.ReadLine();
                    string? linea;

                    Console.WriteLine($"{"ID",-6} | {"Nombre Ruta",-18} | {"Distancia",-12} | {"Puntos Asignados"}");
                    Console.WriteLine(new string('-', 75));

                    double umbralEcoAmigable = 40.0;

                    while ((linea = Lector.ReadLine()) != null)
                    {
                        string[] datos = linea.Split(',');

                        if (datos.Length >= 4)
                        {
                            string id = $"[{datos[0]}]";
                            string nombre = datos[1];
                            string distancia = $"{datos[2]} Km";

                            string puntos = datos[3].Replace("|", " - ");

                            if (double.TryParse(datos[2], out double distanciaCalculada))
                            {
                                if (distanciaCalculada > umbralEcoAmigable)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write($"{id,-9}{nombre,-20} | {distancia,-12} | {puntos}");
                                    Console.WriteLine("  >> [⚠ UMBRAL KM SUPERADO: Esta ruta requiere ser revisada y reestructurada]");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    Console.WriteLine($"{id,-9}{nombre,-20} | {distancia,-12} | {puntos}");
                                }
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  [!] Error: El archivo de rutas no existe. Cree una ruta primero.");
                Console.ResetColor();
            }
            catch (UnauthorizedAccessException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  [!] Error de Seguridad: No tienes permisos para leer este archivo.");
                Console.WriteLine("  Asegúrese de no tener el archivo abierto en Excel.");
                Console.ResetColor();
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  [!] Error Crítico: El archivo CSV está corrupto (contiene letras en la columna de distancia).");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  [!] Ocurrió un error inesperado: {ex.Message}");
                Console.ResetColor();
            }

        }
    }

    public class BuscadorFiltros
    {
        public void MostrarMenuFiltros(int[,] matrizCobertura)
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n  ==================================================");
                Console.WriteLine("             🔍 MÓDULO DE BÚSQUEDA Y FILTROS 🔍      ");
                Console.WriteLine("  ==================================================\n");
                Console.ResetColor();

                Console.WriteLine("    [ 1 ] Buscar Puntos de Recolección por Zona");
                Console.WriteLine("    [ 2 ] Filtrar Rutas por Estado (Eco-Eficiencia)");
                Console.WriteLine("    [ 3 ] Ver Zonas y Días Pendientes de Recolección\n");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("    [ 0 ] Volver al Menú Principal");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine();
                int opcion;
                Validadores.Leer("    ▶ Seleccione un filtro: ", out opcion);
                Console.ResetColor();

                Console.Clear();

                if (opcion == 1)
                {
                    FiltrarPuntosPorZona();
                }
                else if (opcion == 2)
                {
                    FiltrarRutasPorEstado();
                }
                else if (opcion == 3)
                {
                    MostrarDiasPendientes(matrizCobertura);
                }
                else if (opcion == 0)
                {
                    salir = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  [!] Opción no válida.");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }
        }

        private void FiltrarPuntosPorZona()
        {
            string rutaArchivo = Configuracion.RutaPuntos;

            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("  [!] No hay puntos registrados.");
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n  --- BÚSQUEDA DE PUNTOS POR ZONA ---");
            Console.ResetColor();

            string zonaBuscada;
            Validadores.Leer("  Ingrese el nombre de la Zona a buscar: ", out zonaBuscada);
            zonaBuscada = zonaBuscada.Trim().ToLower();

            Console.WriteLine($"\n{"ID",-6} | {"Zona",-15} | {"Capacidad",-12} | {"Estado"}");
            Console.WriteLine(new string('-', 55));

            int encontrados = 0;
            string[] lineas = File.ReadAllLines(rutaArchivo);

            for (int i = 1; i < lineas.Length; i++)
            {
                string[] datos = lineas[i].Split(',');
                if (datos.Length >= 5)
                {
                    string zonaActual = datos[1].Trim().ToLower();

                    if (zonaActual == zonaBuscada)
                    {
                        string estado = datos[4].Trim().ToLower() == "true" ? "Activo" : "Inactivo";
                        Console.WriteLine($"[{datos[0],-4}] | {datos[1],-15} | {datos[2] + " Kg",-12} | {estado}");
                        encontrados++;
                    }
                }
            }

            if (encontrados == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  No se encontraron puntos en la zona: '{zonaBuscada}'.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  Total encontrados: {encontrados} puntos.");
                Console.ResetColor();
            }
            Console.ReadKey();
        }

        private void FiltrarRutasPorEstado()
        {
            string rutaArchivo = Configuracion.RutaRutas;

            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("  [!] No hay rutas registradas.");
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n  --- FILTRO DE RUTAS POR ESTADO ---");
            Console.ResetColor();
            Console.WriteLine("  1. Rutas Óptimas (<= 40 Km)");
            Console.WriteLine("  2. Rutas que Requieren Revisión (> 40 Km)");

            int tipoEstado;
            Validadores.Leer("  Seleccione el estado a filtrar (1 o 2): ", out tipoEstado);

            if (tipoEstado != 1 && tipoEstado != 2)
            {
                Console.WriteLine("  [!] Opción inválida.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\n{"ID",-6} | {"Nombre",-15} | {"Distancia",-12}");
            Console.WriteLine(new string('-', 40));

            double umbral = 40.0;
            int encontrados = 0;
            string[] lineas = File.ReadAllLines(rutaArchivo);

            for (int i = 1; i < lineas.Length; i++)
            {
                string[] datos = lineas[i].Split(',');
                if (datos.Length >= 4 && double.TryParse(datos[2], out double dist))
                {
                    bool esOptima = dist <= umbral;
                    bool mostrar = false;

                    if (tipoEstado == 1 && esOptima)
                    {
                        mostrar = true;
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (tipoEstado == 2 && !esOptima)
                    {
                        mostrar = true;
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    if (mostrar)
                    {
                        Console.WriteLine($"[{datos[0],-4}] | {datos[1],-15} | {dist} Km");
                        encontrados++;
                        Console.ResetColor();
                    }
                }
            }

            Console.WriteLine($"\n  Total rutas en esta categoría: {encontrados}");
            Console.ReadKey();
        }

        private void MostrarDiasPendientes(int[,] matriz)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n  --- ZONAS Y DÍAS PENDIENTES DE RECOLECCIÓN ---");
            Console.ResetColor();
            Console.WriteLine("  (Días donde el servicio aún no ha pasado)\n");

            string[] zonas = { "Sopocachi", "San Pedro", "Miraflores", "San Jorge", "Obrajes" };
            string[] dias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };

            int zonasCompletas = 0;

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"  ▶ {zonas[i],-15}: ");
                Console.ResetColor();

                bool tienePendientes = false;

                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j] == 0)
                    {
                        Console.Write($"{dias[j]} | ");
                        tienePendientes = true;
                    }
                }

                if (!tienePendientes)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("¡Cobertura del 100% completada!");
                    Console.ResetColor();
                    zonasCompletas++;
                }

                Console.WriteLine();
            }

            if (zonasCompletas == zonas.Length)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n  ¡FELICIDADES! Toda la ciudad ha sido cubierta esta semana.");
                Console.ResetColor();
            }

            Console.WriteLine("\n  Presione cualquier tecla para volver...");
            Console.ReadKey();
        }
    }

}




