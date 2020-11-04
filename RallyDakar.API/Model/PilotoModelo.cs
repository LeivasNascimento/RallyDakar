using System.ComponentModel.DataAnnotations;

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

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é de preenchimento obrigatório")]
        [MinLength(5,ErrorMessage ="Nome deve ter no mínimo 5 caracteres")]
        [MaxLength(50,ErrorMessage ="Nome não pode ter mais que 50 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "SobreNome é de preenchimento obrigatório")]
        [MinLength(5,ErrorMessage = "SobreNome deve ter no mínimo 5 caracteres")]
        [MaxLength(50,ErrorMessage = "SobreNome não pode ter mais que 50 caracteres")]
        public string SobreNome { get; set; }

        public string NomeCompleto { 
            get { return $"{Nome} {SobreNome}"; }    
        }

        public int EquipeId { get; set; }

        //n entra comportamentos na classe de modelo 
        public PilotoModelo()
        {

        }
    }
}
