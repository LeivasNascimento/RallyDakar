﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RallyDakar.API.Modelo
{
    public class TelemetriaModelo
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Equipe não identificada")]
        public int EquipeId { get; set; }

        [Required(ErrorMessage = "Data da equipe não foi recebia")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Hora da equipe não foi recebia")]
        public TimeSpan Hora { get; set; }

        public DateTime DataServidor { get; set; }
        public TimeSpan HoraServidor { get; set; }

        [Required(ErrorMessage = "Latitude não informada")]
        public decimal Latitude { get; set; }

        [Required(ErrorMessage = "Longitude não informada")]
        public decimal Longitude { get; set; }

        [Required(ErrorMessage = "Percentual Combustível não informado")]
        public decimal PercentualCombustivel { get; set; }

        [Required(ErrorMessage = "Velocidade não informada")]
        public double Velocidade { get; set; }

        [Required(ErrorMessage = "RPM não informado")]
        public double RPM { get; set; }
        
        public int TemperaturaExterna { get; set; }
        public int TemperaturaMotor { get; set; }
        public double AltitudeNivelMar { get; set; }

        public bool PedalAcelerador { get; set; }
        public bool PedalFreio { get; set; }
    }
}
