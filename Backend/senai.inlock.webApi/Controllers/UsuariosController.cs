using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai.inlock.webApi_.Domains;
using senai.inlock.webApi_.Interfaces;
using senai.inlock.webApi_.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace senai.inlock.webApi_.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _UsuarioRepository { get; set; }

        public UsuariosController()
        {
            _UsuarioRepository = new UsuarioRepository();
        }

        [HttpPost("login")]

        public IActionResult Login(UsuarioDomain login)
        {
            UsuarioDomain usuarioBuscado = _UsuarioRepository.BuscarPorEmailSenha(login.email, login.senha);
            if (usuarioBuscado == null)
            {
                return NotFound("Email ou senha incorretos");
            }
            else
            {
                var minhasClaims = new[]
                {
                  new Claim(JwtRegisteredClaimNames.Email,usuarioBuscado.email),
                  new Claim(JwtRegisteredClaimNames.Jti,usuarioBuscado.idUsuario.ToString()),
                  new Claim(ClaimTypes.Role,usuarioBuscado.tipoUsuario.ToString())

                };
                //define a chave de acesso ao token
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("inlock-chave-autenticacao"));
                //define as credencias do token
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                // composicao do token
                   var meuToken = new JwtSecurityToken(
                    issuer: "inlock.webApi",  //emissor do token
                    audience: "inlock.webApi",  //destinatario do token
                    claims: minhasClaims,    //dados definidos acima 
                    expires: DateTime.Now.AddMinutes(30), //o tempo de expiração do token
                    signingCredentials: creds //credenciais do token
                    );

                return Ok(
                    new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(meuToken)
                    }
                        );
            }

        }

        [HttpGet]
        public IActionResult Get()
        {
            List<UsuarioDomain> listaUsuarios = _UsuarioRepository.ListarTodos();
            return Ok(listaUsuarios);
        }

    }
    }
