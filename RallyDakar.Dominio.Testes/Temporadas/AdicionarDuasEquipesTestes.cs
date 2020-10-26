using Microsoft.VisualStudio.TestTools.UnitTesting;
using RallyDakar.Dominio.Entidades;
using System.Linq;

namespace RallyDakar.Dominio.Testes.Temporadas
{
    [TestClass]
    public class AdicionarDuasEquipesTestes
    {
        Temporada temporada;
        Equipe equipe1;
        Equipe equipe2;
        Equipe equipe3;

        [TestInitialize]
        public void Initialize()
        {
            temporada = new Temporada();
            temporada.Id = 1;
            temporada.Nome = "Temporada2020";

            this.equipe1 = new Equipe();
            this.equipe1.Id = 1;
            this.equipe1.Nome = "EquipeTeste1";

            this.equipe2 = new Equipe();
            this.equipe2.Id = 2;
            this.equipe2.Nome = "teste2";

            this.equipe3 = null;

            temporada.AdicionarEquipe(equipe1);
            temporada.AdicionarEquipe(equipe2);
            temporada.AdicionarEquipe(equipe3);
        }

        [TestMethod]
        public void DuasEquipesAdicionadasCorretamente()
        {
            Assert.IsTrue(temporada.Equipes.Count() == 2);
           
        }
    }
}
