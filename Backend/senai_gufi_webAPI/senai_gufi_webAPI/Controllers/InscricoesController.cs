using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai_gufi_webAPI.Domains;
using senai_gufi_webAPI.Interfaces;
using senai_gufi_webAPI.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace senai_gufi_webAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class InscricoesController : ControllerBase
    {
        private IInscricaoRepository _inscricaoRepository { get; set; }

        public InscricoesController()
        {
            _inscricaoRepository = new InscricaoRepository();
        }

        [Authorize(Roles = "2")]
        [HttpGet("minhas")]
        public IActionResult ListarMinhas()
        {
            try
            {
                int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                return Ok(_inscricaoRepository.ListarMinhas(idUsuario));
            }
            catch (Exception error)
            {
                return BadRequest(new
                {
                    mensagem = "Não é possível mostrar as presenças se o usuário não estiver logado",
                    error
                });
            }
        }

        [Authorize(Roles = "2")]
        [HttpPost("inscrever/{idEvento}")]
        public IActionResult Inscrever(int idEvento)
        {
            try
            {
                Inscricao novaInscricao = new Inscricao()
                {
                    IdUsuario = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value),
                    IdEvento = idEvento,
                    IdSituacao = 1
                };

                _inscricaoRepository.Inscrever(novaInscricao);

                return StatusCode(201);
            }
            catch (Exception error)
            {
                return BadRequest(new
                {
                    mensagem = "Não é possível se inscrver em um evento se o usuário não estiver logado",
                    error
                });
            }

        }

        [Authorize(Roles = "1")]
        [HttpPatch("{idPresenca}")]
        public IActionResult AprovarOuRecusar(int idPresenca, Inscricao status)
        {
            try
            {
                _inscricaoRepository.AprovarOuRecusar(idPresenca, status.IdSituacao.ToString());

                return StatusCode(204);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
    }
}
