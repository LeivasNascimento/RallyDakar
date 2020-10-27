using Microsoft.AspNetCore.Mvc;
using RallyDakar.Dominio.Entidades;
using RallyDakar.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RallyDakar.API.Controllers
{
    [ApiController]
    [Route("api/pilotos")]
    public class PilotoController : ControllerBase
    {
        IPilotoRepository _pilotoRepository;
        public PilotoController(IPilotoRepository pilotoRepository) // é o asp.net core que passa a 
            // referência como argumento desse construtor
        {
            _pilotoRepository = pilotoRepository;
        }

        [HttpGet]
        public IActionResult ObterTodos()
        {
            return Ok(_pilotoRepository.ObterTodos());
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] Piloto piloto)
        {
            //[FromBody] - como o asp.net core vai receber os dados da requisição (nesse caso do corpo do json)
            // poderia ser de um formulário, etc
            _pilotoRepository.Adicionar(piloto);
            return Ok("adicionou");
        }


    }
}
