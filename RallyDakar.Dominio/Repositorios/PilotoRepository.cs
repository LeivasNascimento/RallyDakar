using RallyDakar.Dominio.DbContexto;
using RallyDakar.Dominio.Entidades;
using RallyDakar.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RallyDakar.Dominio.Repositorios
{
    public class PilotoRepository : IPilotoRepository
    {
        private readonly RallyDbContexto _rallyDbContexto;
        public PilotoRepository(RallyDbContexto rallyDbContexto)
        {
            _rallyDbContexto = rallyDbContexto;
        }

        public void Adicionar(Piloto piloto)
        {
            _rallyDbContexto.Pilotos.Add(piloto);
            _rallyDbContexto.SaveChanges();
        }

        public void Atualizar(Piloto piloto)
        {
            /*
            if (_rallyDbContexto.Pilotos.Contains(piloto))
            {
                _rallyDbContexto.Pilotos.Update(piloto);
                _rallyDbContexto.SaveChanges();
            }*/

            //se for PUT
            if(_rallyDbContexto.Entry(piloto).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                _rallyDbContexto.Attach(piloto); // o EF traqueia o piloto na coleção; até então estava 'solta'
                _rallyDbContexto.Entry(piloto).State = Microsoft.EntityFrameworkCore.EntityState.Modified; // considerando todos os campos; dá para fazer por campo individualmente
               
            }
            else //senão, é PATCH
            {
                _rallyDbContexto.Update(piloto);
            }

            _rallyDbContexto.SaveChanges();


        }

        public void Excluir(Piloto piloto)
        {
            // esse piloto do parametro precisa ser uma instancia obtida do banco e n uma instancia 
            // somente instanciada o campo id, senão n vai conseguir dá um attach + entry nessa instancia
            /*  Piloto pil = _rallyDbContexto.Pilotos.FirstOrDefault(x => x.Id.Equals(id));
              if (pil != null)
              {
                  _rallyDbContexto.Pilotos.Remove(pil);
                  _rallyDbContexto.SaveChanges();
              }
            */
            _rallyDbContexto.Attach(piloto);
            _rallyDbContexto.Entry(piloto).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
           // _rallyDbContexto.Pilotos.Remove(piloto);
            _rallyDbContexto.SaveChanges();

        }

        public bool Existe(int pilotoID)
        {
            return _rallyDbContexto.Pilotos.Any(x => x.Id.Equals(pilotoID));
        }

        public Piloto Obter(int pilotoId)
        {
            return _rallyDbContexto.Pilotos.FirstOrDefault(x => x.Id.Equals(pilotoId));
        }

        public IEnumerable<Piloto> ObterPorNome(string nome)
        {
            return _rallyDbContexto.Pilotos.Where(x => x.Nome.Contains(nome)).ToList();
        }

        public IEnumerable<Piloto> ObterTodos()
        {
            return _rallyDbContexto.Pilotos.ToList(); // o .net core já retorna em json na requisição, n precisa usar nenhum tipo de 'parse json' ou algo similar.
        }
    }
}
