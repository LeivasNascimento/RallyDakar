using RallyDakar.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace RallyDakar.Dominio.Interfaces
{
    public interface IPilotoRepository
    {
        void Adicionar(Piloto piloto); 
        void Atualizar(Piloto piloto);

        void Excluir(Piloto piloto);

        IEnumerable<Piloto> ObterPorNome(string nome);

        IEnumerable<Piloto> ObterTodos();

        Piloto Obter(int pilotoId);
        bool Existe(int pilotoID);

    }
}
