using Microsoft.VisualStudio.TestTools.UnitTesting;
using RallyDakar.Dominio.Entidades;

namespace RallyDakar.Dominio.Testes.Temporadas
{
    [TestClass]
    public class AdicionaTemporadaTodosCamposPreenchidos
    {
        public Temporada Temporada { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Temporada = new Temporada();
            Temporada.Id = 1;
            Temporada.Nome = "Teste 2020";
            Temporada.DataInicio = new System.DateTime();
            Temporada.DataFim = new System.DateTime().AddDays(30);
           
            Temporada.AdicionarEquipe(new Equipe { Id = 1, Nome = "Ferrari", CodigoIdentificador = "22" });
        }

        [TestMethod]
        public void AdicionarTemporadaSemCamposNulos()
        {
            bool temNulo = false;
            if(Temporada.DataFim == null || Temporada.DataInicio == null || Temporada.Equipes.Count == 0 
                || Temporada.Id == 0 || string.IsNullOrEmpty(Temporada.Nome))
            {
                temNulo = true;
            }

            Assert.IsFalse(temNulo);
        }
    }
}
