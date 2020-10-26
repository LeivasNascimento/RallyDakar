using System.Collections.Generic;

namespace RallyDakar.Dominio.Entidades
{
    public class Equipe
    {
        public int Id { get; set; }
        public string CodigoIdentificador { get; set; }
        public string Nome { get; set; }

        public int TemporadaId { get; set; }
        public virtual Temporada Temporada { get; set; }
        //virtual: O ORM (como o EntityFw), carrega a entidade
        //Temporada por demanda. Tem que estar como 'virtual' para isto.
        //N fazer sempre pois a entidade carregada pode ser 'pesada'

        public ICollection<Piloto> Pilotos { get; set; }
    }
}
