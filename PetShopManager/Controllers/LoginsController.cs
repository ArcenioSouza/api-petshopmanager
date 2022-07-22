using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PetShopManager.Data;
using PetShopManager.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PetShopManager.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LoginsController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        public LoginsController(ApplicationDbContext database)
        {
            _database = database;
        }

        [HttpPost]
        public IActionResult Login(Login credenciais)
        {
            try
            {
                var usuarioCliente = _database.Clientes.First(user => user.Email.Equals(credenciais.Email));
                var usuarioFuncionario = _database.Funcionarios.First(user => user.Email.Equals(credenciais.Email));

                if (usuarioCliente != null)
                {
                    var chaveDeSeguranca = "minhasenhasecreta";
                    var chaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveDeSeguranca));
                    var credenciaisDeAcesso = new SigningCredentials(chaveSimetrica, SecurityAlgorithms.HmacSha256Signature);

                    var claims = new List<Claim>();
                    claims.Add(new Claim("id", usuarioCliente.Id.ToString()));
                    claims.Add(new Claim("email", usuarioCliente.Email));
                    claims.Add(new Claim("senha", usuarioCliente.Senha));
                    claims.Add(new Claim(ClaimTypes.Role, "Cliente"));


                    var JWT = new JwtSecurityToken(
                        issuer: "PetShopManager.com",
                        expires: DateTime.Now.AddHours(24),
                        audience: "usuario_comum",
                        signingCredentials: credenciaisDeAcesso
                    );

                    return Ok(new { msg = "Logado com sucesso", token = new JwtSecurityTokenHandler().WriteToken(JWT) });

                }
                else if (usuarioFuncionario != null)
                {
                    var chaveDeSeguranca = "minhasenhasecreta";
                    var chaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveDeSeguranca));
                    var credenciaisDeAcesso = new SigningCredentials(chaveSimetrica, SecurityAlgorithms.HmacSha256Signature);

                    var claims = new List<Claim>();
                    claims.Add(new Claim("id", usuarioFuncionario.Id.ToString()));
                    claims.Add(new Claim("email", usuarioFuncionario.Email));
                    claims.Add(new Claim("senha", usuarioFuncionario.Senha));
                    claims.Add(new Claim(ClaimTypes.Role, "Funcionario"));

                    var JWT = new JwtSecurityToken(
                        issuer: "PetShopManager.com",
                        expires: DateTime.Now.AddHours(24),
                        audience: "comum",
                        signingCredentials: credenciaisDeAcesso
                    );

                    return Ok(new { msg = "Logado com sucesso", token = new JwtSecurityTokenHandler().WriteToken(JWT) });
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Usuário não autorizado");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}