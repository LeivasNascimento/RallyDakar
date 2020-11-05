using System;

namespace RallyDakar.Dominio.Entidades
{
    public class Telemetria
    {
        public int Id { get; set; }
        public int EquipeId { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataServidor { get; set; }

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

        //campos abaixo n contém no exemplo final do curso
       
        public TimeSpan Hora { get; set; } // que vem do carro n do servidor
        public TimeSpan HoraServidor { get; set; } 
     
        

    }
}
