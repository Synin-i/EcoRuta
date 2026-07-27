using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRutaModels
{
    public struct Ruta
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public int[] PuntosAsignados { get; private set; }
        public double DistanciaEstimadaKm { get; private set; }
        public DateTime FechaCreacion { get; private set; }

        public Ruta(int id, string nombre, int[] puntos, double distancia)
        {
            if (puntos == null || puntos.Length == 0) throw new ArgumentException("Ruta requiere puntos asignados.");
            Id = id; Nombre = nombre?.Trim() ?? "";
            PuntosAsignados = (int[])puntos.Clone();
            DistanciaEstimadaKm = distancia;
            FechaCreacion = DateTime.Now;
        }
    }

}
