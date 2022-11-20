using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewRest.Model;
using NewRest.Services;

namespace NewRest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : Controller
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _ipersonPervice;
        List<Error> err = new List<Error>();
        public PersonController(ILogger<PersonController> logger, IPersonService ipersonService)
        {
            _logger = logger;
            _ipersonPervice = ipersonService;
        }

        /// <summary>Rota para encontrar o cadastro de uma pessoa pelo seu id.</summary>
        /// <remarks>
        /// ### Fluxo de Utilização da rota: ###
        ///     - Informar o id desejado para a busca.
        ///</remarks>
        /// <returns></returns>
        [HttpGet("pessoa")]
        public IActionResult GetPersonAll()
        {
            try
            {
                return Ok(_ipersonPervice.FindAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        /// <summary>Rota para retornar o cadastro de todas as pessoas da base.</summary>
        /// <remarks>
        /// ### Fluxo de Utilização da rota: ###
        ///     - Apenas executar o endpoint.
        ///</remarks>
        /// <returns></returns>
        [HttpGet("pessoa/{id}")]
        public IActionResult GetPerson(long id)
        {
            try
            {
                var personId = _ipersonPervice.FindByID(id);

                if (personId is null)
                {
                    return BadRequest("Ooops! Nenhuma pessoa encontrada...");
                }
                return Ok(personId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        /// <summary>Rota para cadastrar uma pessoa na base.</summary>
        /// <remarks>
        /// ### Fluxo de Utilização da rota: ###
        ///     - Informar os dados necessários para cadastro.
        ///</remarks>
        /// <returns></returns>
        [HttpPost("pessoa/cadastrar")]
        public IActionResult CreatePessoa([FromBody] Person person)
        {
            if (person is null)
            {
                return BadRequest("Ooops! Nenhum dado foi informado para cadastro...");
            }

            try
            {
                if (string.IsNullOrEmpty(person.FirstName))
                {
                    err.Add(new Error { code = 400, message = "O primeiro nome deve ser informado para cadastro!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }

            if (err.Count > 0)
            {
                return BadRequest(err.ToList());
            }
            return Created("Pessoa cadastrada com sucesso!", _ipersonPervice.Create(person));
        }

        /// <summary>Rota para atualizar uma pessoa na base.</summary>
        /// <remarks>
        /// ### Fluxo de Utilização da rota: ###
        ///     - Informar os desejados necessários para atualização.
        ///</remarks>
        /// <returns></returns>
        [HttpPut("pessoa/atualizar")]
        public IActionResult UpdatePessoa([FromBody] Person person)
        {
            if (person is null)
            {
                return BadRequest("Ooops! Ao menos algum dado deve ser informado para a atualização.");
            }

            try
            {
                if (person.Id < 0 || person.Id is 0)
                {
                    return BadRequest("Ooops! É preciso informar o id da pessoa para atualização...");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
            return Ok(_ipersonPervice.Update(person));
        }

        /// <summary>Rota para deletar uma pessoa na base.</summary>
        /// <remarks>
        /// ### Fluxo de Utilização da rota: ###
        ///     - Informar os desejados necessários para exclusão.
        ///</remarks>
        /// <returns></returns>
        [HttpDelete("pessoa/deletar/{id}")]
        public IActionResult DeletePessoa(long id)
        {
            try
            {
                if (id is 0)
                {
                    return BadRequest("Ooops! É necessário informado o identificador para exclusão...");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
            _ipersonPervice.Delete(id);
            return Ok($"Pessoa de identificador {id} excluída com sucesso!");
        }
    }
}