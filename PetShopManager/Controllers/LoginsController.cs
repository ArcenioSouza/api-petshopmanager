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
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Login(string email, string senha, bool isCliente)
        {
            try
            {
                if (isCliente)
                {
                    Cliente usuarioCliente = await _database.Clientes.FirstOrDefaultAsync(user => user.Email.Equals(email));
                    if (usuarioCliente is null) return NotFound("Usuário cliente não encontrado");

                    if (usuarioCliente.Senha != senha) return BadRequest("Senha inválida");

                    var chaveDeSeguranca = "minhasenhasecreta";
                    var chaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveDeSeguranca));
                    var credenciaisDeAcesso = new SigningCredentials(chaveSimetrica, SecurityAlgorithms.HmacSha256Signature);

                    var claims = new List<Claim>();
                    claims.Add(new Claim("id", usuarioCliente.Id.ToString()));
                    claims.Add(new Claim("email", usuarioCliente.Email));
                    claims.Add(new Claim(ClaimTypes.Role, "Cliente"));

                    var JWT = new JwtSecurityToken(
                        issuer: "PetShopManager.com",
                        expires: DateTime.Now.AddHours(24),
                        audience: "usuario_comum",
                        signingCredentials: credenciaisDeAcesso,
                        claims: claims
                    );

                    return Ok(new { msg = "Usuário Logado", token = new JwtSecurityTokenHandler().WriteToken(JWT) });

                }
                else
                {
                    Funcionario usuarioFuncionario = await _database.Funcionarios.FirstAsync(user => user.Email.Equals(email));
                    if (usuarioFuncionario is null) return NotFound("Usuário funcionario não encontrado");
                    if (usuarioFuncionario.Senha != senha) return BadRequest("Senha inválida");

                    var chaveDeSeguranca = "minhasenhasecreta";
                    var chaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveDeSeguranca));
                    var credenciaisDeAcesso = new SigningCredentials(chaveSimetrica, SecurityAlgorithms.HmacSha256Signature);

                    var claims = new List<Claim>();
                    claims.Add(new Claim("id", usuarioFuncionario.Id.ToString()));
                    claims.Add(new Claim("email", usuarioFuncionario.Email));
                    claims.Add(new Claim(ClaimTypes.Role, "Funcionario"));


                    var JWT = new JwtSecurityToken(
                        issuer: "PetShopManager.com",
                        expires: DateTime.Now.AddHours(24),
                        audience: "usuario_comum",
                        signingCredentials: credenciaisDeAcesso,
                        claims: claims
                    );

                    return Ok(new { msg = "Usuário Logado", token = new JwtSecurityTokenHandler().WriteToken(JWT) });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}