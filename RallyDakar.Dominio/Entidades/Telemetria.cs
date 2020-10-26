﻿using System;

namespace RallyDakar.Dominio.Entidades
{
    public class Telemetria
    {
        public int Id { get; set; }
        public int TemporadaId { get; set; }
        public int PilotoId { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; } // que vem do carro n do servidor
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal PercentualCombustivel { get; set; }
        public double Velocidade { get; set; }
        public double RPM { get; set; }
        public int TemperaturaExterna { get; set; }
        public int TemperaturaMotor { get; set; }
        public double AltitudeNivelMar { get; set; }
        public bool PedalFreio { get; set; }
        public bool PedalAcelerador { get; set; }

    }
}