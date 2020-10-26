using System;
using System.Collections.Generic;

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
                if(!string.IsNullOrEmpty(equipe.Nome))
                {
                    this.Equipes.Add(equipe);
                }
            }
        }

    }
}
