using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRutaConsoleModels
{
    public struct PuntoRecoleccion
    {
        public int Id { get; private set; }
        public string Zona { get; private set; }
        public double CapacidadKg { get; private set; }
        public int FrecuenciaSemanal { get; private set; }
        public bool Activo { get; private set; }

        public PuntoRecoleccion(int id, string zona, double capacidad, int frecuencia, bool activo)
        {
            if (id <= 0 || capacidad < 0 || frecuencia < 1 || frecuencia > 7)
                throw new ArgumentException("Datos de punto inválidos.");
            Id = id; 
            Zona = zona?.Trim() ?? "";
            CapacidadKg = capacidad; 
            FrecuenciaSemanal = frecuencia; 
            Activo = activo;
        }
        public override string ToString() => $"[{Id}] {Zona} | {CapacidadKg:F1}kg | Frec: {FrecuenciaSemanal}x/semana | {(Activo ? "✅" : "❌")}";
    }
}