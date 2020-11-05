using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RallyDakar.Dominio.Entidades;
using System;

namespace RallyDakar.Dominio.DbContexto
{

    public class BaseDados
    {
        public BaseDados()
        {

        }

        // STATIC: TODAS AS INSTÂNCIAS DA CLASSE BaseDados TERÃO O MESMO VALOR PARA ESTE MÉTODO.
        // ESTA CLASSE É INSTANCIADA EM PROGRAM.CS, LOGO CADA INSTANCIA TERÁ O MESMO VALOR REFERENTE A ESTE MÉTODO

        /* A static method in C# is a method that keeps only one copy of the method at the Type level, not the object level.  
         * That means, all instances of the class share the same copy of the method and its data. The last updated value of the method 
           is shared among all objects of that Type. Static methods are called by using the class name, not the instance of the class. 
        */

        public static void CargaInicial(IServiceProvider serviceProvider)
        {
            // adiciona carga inicial na memória
            using(var context = new RallyDbContexto(serviceProvider.GetRequiredService<DbContextOptions<RallyDbContexto>>()))
            {
                var temporada = new Temporada();
                    temporada.Id = 1;
                    temporada.Nome = "Temporada20";
                    temporada.DataInicio = DateTime.Now;
                var equipe = new Equipe();
                    equipe.Id = 1;
                    equipe.Nome = "Ferrari";
                    equipe.CodigoIdentificador = "FER";

                var piloto = new Piloto();
                piloto.Id = 1;
                piloto.Nome = "schumacher";

                var piloto2 = new Piloto();
                piloto2.Id = 2;
                piloto2.Nome = "Rubens";

                equipe.AdicionarPiloto(piloto);
                equipe.AdicionarPiloto(piloto2);

                temporada.AdicionarEquipe(equipe);

                context.Temporadas.Add(temporada);
                context.SaveChanges(); //salva em memória 

            }
        }
        
    }
    
}
