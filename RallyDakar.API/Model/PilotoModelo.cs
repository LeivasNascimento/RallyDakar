namespace RallyDakar.API.Model
{
    public class PilotoModelo
    {
        /* A pasta Model é criada para que
        se possa ter uma filtragem do que se 
        deve mostrar das classes de entidades
        ex: n mostrar todos os campos da entidade
        na api. então irá ser utilizada a classeModelo
        para que possa representar a entidade para
        protegê-la.
        */

        public int Id { get; set; }
        public string Nome { get; set; }
        public int EquipeId { get; set; }

        //n entra comportamentos na classe de modelo 
        public PilotoModelo()
        {

        }
    }
}
