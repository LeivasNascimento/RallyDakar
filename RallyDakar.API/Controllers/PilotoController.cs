using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RallyDakar.API.Model;
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
        private readonly IPilotoRepository _pilotoRepository;
        private readonly IMapper _mapper;
        //serve para pegar uma classe A do tipo A e instancia uma classe B do tipo B com campos iguais
        private readonly ILogger<PilotoController> _logger;

        public PilotoController(IPilotoRepository pilotoRepository, IMapper mapper, ILogger<PilotoController> logger) // é o asp.net core que passa a 
        {
            _pilotoRepository = pilotoRepository;
            _mapper = mapper;
            _logger = logger;
            //serve para pegar uma classe A do tipo A e instancia uma classe B do tipo B com campos iguais

        }

        [HttpGet] //método
        public IActionResult ObterTodos() //tipo e recurso; .net core já retorna o tipo em json
        {
            try
            {
                /*var den = 0; TESTE FORÇANDO ERRO
                var numero = 1 / den; TESTE FORÇANDO ERRO
                */
                IEnumerable<Piloto> pilotos = _pilotoRepository.ObterTodos(); //dados associados
                if (!pilotos.Any())
                    return NotFound();

                //IEnumerable<PilotoModelo> pilotosModelo = _mapper.Map<PilotoModelo>(pilotos);

                else return Ok(pilotos);
            }
            catch (Exception ex)
            {
                //return BadRequest("Ocorreu um erro interno no sistema. Por favor entre em contato com suporte.");
                // registrar em log, seja em arquivo ou base de dados
                // _logger.Info(ex.ToString());
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte.");
            }
        }
         
        [HttpGet("{id}", Name = "Obter")]
        public IActionResult Obter(int id)
        {
            try
            {
                var retorno = _pilotoRepository.Obter(id);
                if(retorno == null)
                {
                    return NotFound();
                }else
                {
                    var retornoModel = _mapper.Map<PilotoModelo>(retorno);
                    return Ok(retornoModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Ocorreu um erro interno no sistema.Por favor entre em contato com suporte.");
            }
        }

        [HttpPost]
        public IActionResult AdicionarPiloto([FromBody] PilotoModelo pilotoModelo)
        {
            try
            {
               
                /*Piloto piloto = new Piloto();
                piloto.Id = pilotoModelo.Id;
                piloto.Nome = pilotoModelo.Nome;*/
                _logger.LogInformation("mapeando piloto modelo");

                var piloto = _mapper.Map<Piloto>(pilotoModelo);

                _logger.LogInformation($"verificando se o piloto id == {piloto.Id} existe na base de dados em memória");
                if (_pilotoRepository.Existe(piloto.Id))
                {
                    _logger.LogWarning($"piloto id = {piloto.Id} já existe.");
                    return StatusCode(409, "já existe um piloto com este identificador");
                }

                //[FromBody] - como o asp.net core vai receber os dados da requisição (nesse caso do corpo do json)
                // poderia ser de um formulário, etc
                _logger.LogInformation($"adicionando piloto id {piloto.Id}");
                _pilotoRepository.Adicionar(piloto);
                _logger.LogInformation("piloto inseriu com sucesso!");

                _logger.LogInformation("mapeando piloto inserido para piloto modelo");
                var pilotoModeloRetorno = _mapper.Map<PilotoModelo>(piloto);

                //return Ok("adicionou");
                _logger.LogInformation("retornando piloto modelo novo");
                return CreatedAtRoute("Obter", new { id = piloto.Id }, pilotoModeloRetorno);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString()); 
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte.");
            }
           
        }
        
        [HttpPut] //atualiza totalmente
        public IActionResult Atualizar([FromBody] PilotoModelo pilotoModelo)
        {
            //piloto: instância solta, n é gerenciada pelo entityFramework
            // se mandar atualizar, o EF n conhece essa instância (hash da instância)
            // 

            try
            {
                if(_pilotoRepository.Existe(pilotoModelo.Id))
                {
                    var piloto = _mapper.Map<Piloto>(pilotoModelo);

                    _pilotoRepository.Atualizar(piloto);
                    return NoContent();
                }
                    else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte.");
            }
        } 
        
        [HttpPatch("{id}")] //atualiza parcialmente
        public IActionResult AtualizarParcialmente(int id, [FromBody] JsonPatchDocument<PilotoModelo> patchPilotoModelo)
        { //JsonPatchDocument<Piloto>; ex: vem do postman, a parte fragmentada, ou seja, só alguns campos
            // n envia o objeto com todos os campos para serem atualizados, por isso usa o patch
            // o .net core já faz isso automaticamente, mas aqui está em modo de exemplo
            try
            {
                if (!_pilotoRepository.Existe(id))
                {
                    _logger.LogWarning($"pilotoId:{id} não existe");
                    return NotFound();
                }

                var piloto = _pilotoRepository.Obter(id); //tem o hash do registro do EF
                var pilotoModelo = _mapper.Map<PilotoModelo>(piloto);

                patchPilotoModelo.ApplyTo(pilotoModelo); //ApplyTo: pega as n alterações e aplica na instancia do objeto do EF

                piloto = _mapper.Map(pilotoModelo, piloto); //pega o piloto do EF e aplica as alterações aplicadas a pilotoModelo e joga no piloto
                _logger.LogInformation($"piloto id {id} atualizado com sucesso!");
                _pilotoRepository.Atualizar(piloto);

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte.");
            }
        }

        //[HttpDelete]
        //public IActionResult Deletar(Piloto piloto)
        //{
        //    _pilotoRepository.Excluir(piloto);
        //    return Ok("excluiu");
        //}
        
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                //o tratamento de existencia do registro pode ser feito aqui no controller
                if(!_pilotoRepository.Existe(id))
                    return NotFound();

                var piloto = _pilotoRepository.Obter(id);
                _pilotoRepository.Excluir(piloto);
                // return Ok("excluiu");
                _logger.LogInformation($"piloto { id } excluído com sucesso");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte.");
            }
        }

        [HttpOptions]
        public IActionResult ListarOperacoesPermitidas()
        {
            Response.Headers.Add("Allow", "GET, POST, PATCH, DELETE, PUT, OPTIONS");
            return NoContent();
        }
    }
}
