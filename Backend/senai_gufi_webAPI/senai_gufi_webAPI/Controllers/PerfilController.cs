using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai_gufi_webAPI.Interfaces;
using senai_gufi_webAPI.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace senai_gufi_webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public PerfilController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [Authorize(Roles = "2")]
        [HttpPost("imagem/bd")]
        public IActionResult postBD(IFormFile arquivo)
        {
            try
            {
                //Analisar o tamanho do arquivo
                if (arquivo.Length > 50000000) 
                {
                    return BadRequest(new { mensagem = "O tamanho máximo da imagem foi atingido" });
                }

                string extensao = arquivo.FileName.Split('.').Last();

                if (extensao != "png")
                {
                    return BadRequest(new { mensagem = "Apenas arquivos .png são aceitados" });
                }

                //recuperar id do usuario logado a partir do token
                int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                _usuarioRepository.SalvarPerfilBD(arquivo, idUsuario);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "2")]
        [HttpGet("imagem/bd")]
        public IActionResult getBD()
        {
            try
            {
                int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                string base64 =  _usuarioRepository.ConsultarPerfilBD(idUsuario);

                return Ok(base64); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "2")]
        [HttpPost("imagem/dir")]
        public IActionResult postDir(IFormFile arquivo)
        {
            try
            {
                //Analisar o tamanho do arquivo
                if (arquivo.Length > 500000000)
                {
                    return BadRequest(new { mensagem = "O tamanho máximo da imagem foi atingido" });
                }

                string extensao = arquivo.FileName.Split('.').Last();

                if (extensao != "png")
                {
                    return BadRequest(new { mensagem = "Apenas arquivos .png são aceitados" });
                }

                //recuperar id do usuario logado a partir do token
                int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                _usuarioRepository.SalvarPerfilDir(arquivo, idUsuario);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "2")]
        [HttpGet("imagem/dir")]
        public IActionResult getDir()
        {
            try
            {
                int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                string base64 = _usuarioRepository.ConsultarPerfilBD(idUsuario);

                return Ok(base64);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
