using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace RallyDakar.Dominio.Entidades
{
    public class Temporada
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public virtual ICollection<Equipe> Equipes { get; set; }

        public Temporada()
        {
            this.Equipes = new List<Equipe>();
        }

        /* no momento deste comentário (código acima) a classe é anêmica (possui somente
        campos). Referências informam que a classe deve possuir mais
        que somente atributos. Devem possuir também comportamentos.
        Abaixo desse comentário serão adicionados os comportamentos.
         */
        public void AdicionarEquipe(Equipe equipe)
        {
            if(equipe != null)
            {
                if(equipe.Validado())
                {
                    this.Equipes.Add(equipe);
                }
            }
        }

        public Equipe ObterPorId(int id)
        {
            // return Equipes.Where(x => x.Id == id).ToList().FirstOrDefault();
            return Equipes.FirstOrDefault(x => x.Id.Equals(id));
        }

    }
}
